using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClearCacheWinForm.Caching;

namespace ClearCacheWinForm
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private Dictionary<string, string> allEnv = new Dictionary<string, string>();
        private Dictionary<string, List<string>> allGroup = new Dictionary<string, List<string>>();
        private List<string> filterPrefixs = GetSettingToList("filterPrefixs");
        private Thread thread;

        /// <summary>
        /// 清理分钟缓存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (chkKeyList.CheckedItems.Count == 0)
            {
                WriteLogToBox("请选择要清除的数据");
                return;
            }
            var cacheManager = GetCacheManager();
            foreach (var item in chkKeyList.CheckedItems)
            {
                cacheManager.Remove(item.ToString());
            }
            WriteLogToBox("移除数据：" + chkKeyList.CheckedItems.Count);
            IisResert();
            LoadShowKeys();
        }
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            //根据分组删除缓存
            LoadEnvs();
            LoadGroup();
            LoadShowKeys();
            this.panelSearch.Visible = GetSetting("showKeySearch") == "true";
            this.button1.Visible = GetSetting("showLoadData") == "true";
        }

        private void cmbGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                LoadShowKeys();
            });
        }

        private void cmbSelectEnv_SelectionChangeCommitted(object sender, EventArgs e)
        {

            Task.Run(() =>
            {
                LoadShowKeys();
            });
        }

        private void btnSelectSingle_Click(object sender, EventArgs e)
        {
            var key = txtSingleKey.Text;
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            Task.Run(() =>
            {
                List<Task> task = new List<Task>();
                Task.Run(() =>
                {
                    cmbGroup.Invoke(new Action(() =>
                    {
                        cmbGroup.SelectedIndex = 0;
                        var value = GetCacheManager(cmbSelectEnv.Text).Get(key);
                        if (!string.IsNullOrEmpty(value))
                        {
                            chkKeyList.Items.Clear();
                            chkKeyList.Items.Add(key, true);
                            WriteLogToBox(cmbSelectEnv.Text + "环境[" + key + "]的值为：\r\n" + value, false);
                        }
                        else
                        {
                            WriteLogToBox("未找到");
                        }
                    }));

                });
                Task.WaitAll(task.ToArray());
            });
        }

        private void btnIISResert_Click(object sender, EventArgs e)
        {
            IisResert();
        }

        /// <summary>
        /// 加载未排除的所有key值
        /// </summary>
        private void LoadShowKeys()
        {
            thread = new Thread(() =>
            {
                string cmbGroupText = "";
                string cmbEnvText = "";
                cmbGroup.Invoke(new Action(() =>
                {
                    cmbGroupText = cmbGroup.Text;
                }));
                cmbSelectEnv.Invoke(new Action(() =>
                {
                    cmbEnvText = cmbSelectEnv.Text;
                }));
                chkKeyList.Invoke(new Action(() => chkKeyList.Items.Clear()));
                var showKeys = new List<string>();
                if (cmbGroupText == "")
                {
                    WriteLogToBox("选择环境,模块后自动加载数据并选中，点击清理缓存即可");
                    return;
                }
                WriteLogToBox("开始加载(" + cmbEnvText + "_" + cmbGroupText + ")数据");
                if (cmbGroupText == "所有")
                {
                    var cacheManager = GetCacheManager(cmbEnvText);
                    showKeys = cacheManager.GetAllKeys();
                }
                else
                {
                    showKeys = allGroup[cmbGroupText].ToList();
                    var cacheManager = GetCacheManager(cmbEnvText);
                    showKeys.RemoveAll(e => cacheManager.IsSet(e) == false);
                }
                //排除项优先排除
                showKeys.RemoveAll(
                    e1 => filterPrefixs.Any(e2 => e1.IndexOf(e2, System.StringComparison.Ordinal) == 0));
                chkKeyList.Invoke(new Action(() =>
                {
                    foreach (var item in showKeys)
                    {
                        chkKeyList.Items.Add(item, true);
                    }
                }));
                WriteLogToBox("加载(" + cmbEnvText + "_" + cmbGroupText + ")数据完毕：" + showKeys.Count+"条");
            });
            thread.Start();
        }
        public ICacheManager GetCacheManager(string envKey = null)
        {
            envKey = envKey ?? cmbSelectEnv.Text;
            return new RedisCacheManager(new RedisConnectionWrapper(envKey + "Connection"));
        }
        /// <summary>
        /// 加载环境
        /// </summary>
        private void LoadEnvs()
        {
            allEnv.Add("test", GetSetting("testConnection"));
            allEnv.Add("pre", GetSetting("preConnection"));
            allEnv.Add("prod", GetSetting("prodConnection"));
            //环境
            foreach (var item in allEnv)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    cmbSelectEnv.Items.Add(item.Key);
                }
            }
            if (cmbSelectEnv.Items.Count > 0)
                cmbSelectEnv.SelectedIndex = 0;
        }
        /// <summary>
        /// 加载分组
        /// </summary>
        private void LoadGroup()
        {
            allGroup.Add("", new List<string>());
            allGroup.Add("所有", new List<string>());
            GetSettingToList("groupNames").ForEach(e =>
            {
                if (!string.IsNullOrEmpty(e) && GetSettingToList(e).Count > 0)
                {
                    allGroup.Add(e, GetSettingToList(e));
                }
            });
            //allGroup.Add("自定义",);
            //allGroup.Add("促销标", new List<string>() { "Prompts" });
            foreach (var item in allGroup)
            {
                cmbGroup.Items.Add(item.Key);
            }
            if (cmbGroup.Items.Count > 0)
                cmbGroup.SelectedIndex = 0;
        }

        public static List<string> GetSettingToList(string key)
        {
            return GetSetting(key).Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        public static string GetSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key] ?? string.Empty;
        }

        public void WriteLogToBox(string msg, bool isAppend = true)
        {
            txtLog.Invoke(new Action(() =>
            {
                if (isAppend == false)
                    txtLog.Clear();
                txtLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + msg);

                txtLog.AppendText(Environment.NewLine);

                txtLog.ScrollToCaret();
            }));
        }


        private void IisResert()
        {
            if (GetSetting("iisResert") == "false")
            {
                return;
            }
            var envVal = cmbSelectEnv.Text;
            Task.Run(() =>
            {

                Process proc = null;
                proc = new Process
                {
                    StartInfo =
                    {
                        CreateNoWindow = false,
                        UseShellExecute = true
                    }
                };
                try
                {
                    string targetDir = AppDomain.CurrentDomain.BaseDirectory + "bats\\";
                    var bats = GetSettingToList(envVal + "batname");
                    if (bats.Count == 0)
                        return;
                    WriteLogToBox("开始重启IIS");
                    foreach (var item in bats)
                    {
                        if (!File.Exists(targetDir + item))
                        {
                            WriteLogToBox("脚本不存在" + item);
                            continue;
                        }
                        proc.StartInfo.CreateNoWindow = false;
                        proc.StartInfo.WorkingDirectory = targetDir;
                        proc.StartInfo.FileName = item;
                        proc.Start();
                        //proc.WaitForExit();
                        WriteLogToBox("执行成功");
                    }
                    WriteLogToBox("重启IIS结束");
                }
                catch (Exception ex)
                {
                    WriteLogToBox(string.Format("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace));
                }
            });
        }
    }
}
