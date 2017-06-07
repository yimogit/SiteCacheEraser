using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Administration;
using PreviewServerSite.Models;

namespace PreviewServerSite.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            List<SiteModel> sitesInfo = new List<SiteModel>();
            using (ServerManager _serverManager = new ServerManager())
            {
                sitesInfo = _serverManager.Sites.Select(e => new SiteModel(e)).ToList();
            }
            //重构↓
            return View(sitesInfo);
        }

        public ActionResult SiteOperation(string name, SiteOperat type)
        {
            try
            {
                using (ServerManager _serverManager = new ServerManager())
                {
                    var site = _serverManager.Sites[name];
                    if (site == null)
                    {
                        return Content("网站不存在");
                    }
                    switch (type)
                    {
                        case SiteOperat.启动:
                            ApplicationPool appPool1 = _serverManager.ApplicationPools[new SiteModel(site).应用程序池];
                            appPool1.Start();
                            break;
                        case SiteOperat.停止:
                            ApplicationPool appPool = _serverManager.ApplicationPools[new SiteModel(site).应用程序池];
                            appPool.Stop();
                            break;
                        case SiteOperat.删除:
                            site.Delete();
                            break;
                    }
                    _serverManager.CommitChanges();
                }
            }
            catch (Exception e)
            {
                return Content("error:" + e.Message);
            }
            return Content("操作完成");
        }
        public ActionResult IndexV1()
        {
            StringBuilder builder = new StringBuilder();
            using (ServerManager sm = new ServerManager())
            {
                builder.AppendFormat("应用程序池默认设置：");
                builder.AppendFormat("<br/>常规：");
                builder.AppendFormat("<br/>\t.NET Framework 版本：{0}", sm.ApplicationPoolDefaults.ManagedRuntimeVersion);
                builder.AppendFormat("<br/>\t队列长度：{0}", sm.ApplicationPoolDefaults.QueueLength);
                builder.AppendFormat("<br/>\t托管管道模式：{0}", sm.ApplicationPoolDefaults.ManagedPipelineMode.ToString());
                builder.AppendFormat("<br/>\t自动启动：{0}", sm.ApplicationPoolDefaults.AutoStart);

                builder.AppendFormat("<br/>CPU：");
                builder.AppendFormat("<br/>\t处理器关联掩码：{0}", sm.ApplicationPoolDefaults.Cpu.SmpProcessorAffinityMask);
                builder.AppendFormat("<br/>\t限制：{0}", sm.ApplicationPoolDefaults.Cpu.Limit);
                builder.AppendFormat("<br/>\t限制操作：{0}", sm.ApplicationPoolDefaults.Cpu.Action.ToString());
                builder.AppendFormat("<br/>\t限制间隔（分钟）：{0}", sm.ApplicationPoolDefaults.Cpu.ResetInterval.TotalMinutes);
                builder.AppendFormat("<br/>\t已启用处理器关联：{0}", sm.ApplicationPoolDefaults.Cpu.SmpAffinitized);

                builder.AppendFormat("<br/>回收：");
                builder.AppendFormat("<br/>\t发生配置更改时禁止回收：{0}",
                    sm.ApplicationPoolDefaults.Recycling.DisallowRotationOnConfigChange);
                builder.AppendFormat("<br/>\t固定时间间隔（分钟）：{0}",
                    sm.ApplicationPoolDefaults.Recycling.PeriodicRestart.Time.TotalMinutes);
                builder.AppendFormat("<br/>\t禁用重叠回收：{0}",
                    sm.ApplicationPoolDefaults.Recycling.DisallowOverlappingRotation);
                builder.AppendFormat("<br/>\t请求限制：{0}", sm.ApplicationPoolDefaults.Recycling.PeriodicRestart.Requests);
                builder.AppendFormat("<br/>\t虚拟内存限制（KB）：{0}",
                    sm.ApplicationPoolDefaults.Recycling.PeriodicRestart.Memory);
                builder.AppendFormat("<br/>\t专用内存限制（KB）：{0}",
                    sm.ApplicationPoolDefaults.Recycling.PeriodicRestart.PrivateMemory);
                builder.AppendFormat("<br/>\t特定时间：{0}",
                    sm.ApplicationPoolDefaults.Recycling.PeriodicRestart.Schedule.ToString());
                builder.AppendFormat("<br/>\t生成回收事件日志条目：{0}",
                    sm.ApplicationPoolDefaults.Recycling.LogEventOnRecycle.ToString());

                builder.AppendFormat("<br/>进程孤立：");
                builder.AppendFormat("<br/>\t可执行文件：{0}", sm.ApplicationPoolDefaults.Failure.OrphanActionExe);
                builder.AppendFormat("<br/>\t可执行文件参数：{0}", sm.ApplicationPoolDefaults.Failure.OrphanActionParams);
                builder.AppendFormat("<br/>\t已启用：{0}", sm.ApplicationPoolDefaults.Failure.OrphanWorkerProcess);

                builder.AppendFormat("<br/>进程模型：");
                builder.AppendFormat("<br/>\tPing 间隔（秒）：{0}",
                    sm.ApplicationPoolDefaults.ProcessModel.PingInterval.TotalSeconds);
                builder.AppendFormat("<br/>\tPing 最大响应时间（秒）：{0}",
                    sm.ApplicationPoolDefaults.ProcessModel.PingResponseTime.TotalSeconds);
                builder.AppendFormat("<br/>\t标识：{0}", sm.ApplicationPoolDefaults.ProcessModel.IdentityType);
                builder.AppendFormat("<br/>\t用户名：{0}", sm.ApplicationPoolDefaults.ProcessModel.UserName);
                builder.AppendFormat("<br/>\t密码：{0}", sm.ApplicationPoolDefaults.ProcessModel.Password);
                builder.AppendFormat("<br/>\t关闭时间限制（秒）：{0}",
                    sm.ApplicationPoolDefaults.ProcessModel.ShutdownTimeLimit.TotalSeconds);
                builder.AppendFormat("<br/>\t加载用户配置文件：{0}", sm.ApplicationPoolDefaults.ProcessModel.LoadUserProfile);
                builder.AppendFormat("<br/>\t启动时间限制（秒）：{0}",
                    sm.ApplicationPoolDefaults.ProcessModel.StartupTimeLimit.TotalSeconds);
                builder.AppendFormat("<br/>\t允许 Ping：{0}", sm.ApplicationPoolDefaults.ProcessModel.PingingEnabled);
                builder.AppendFormat("<br/>\t闲置超时（分钟）：{0}",
                    sm.ApplicationPoolDefaults.ProcessModel.IdleTimeout.TotalMinutes);
                builder.AppendFormat("<br/>\t最大工作进程数：{0}", sm.ApplicationPoolDefaults.ProcessModel.MaxProcesses);

                builder.AppendFormat("<br/>快速故障防护：");
                builder.AppendFormat("<br/>\t“服务不可用”响应类型：{0}",
                    sm.ApplicationPoolDefaults.Failure.LoadBalancerCapabilities.ToString());
                builder.AppendFormat("<br/>\t故障间隔（分钟）：{0}",
                    sm.ApplicationPoolDefaults.Failure.RapidFailProtectionInterval.TotalMinutes);
                builder.AppendFormat("<br/>\t关闭可执行文件：{0}", sm.ApplicationPoolDefaults.Failure.AutoShutdownExe);
                builder.AppendFormat("<br/>\t关闭可执行文件参数：{0}", sm.ApplicationPoolDefaults.Failure.AutoShutdownParams);
                builder.AppendFormat("<br/>\t已启用：{0}", sm.ApplicationPoolDefaults.Failure.RapidFailProtection);
                builder.AppendFormat("<br/>\t最大故障数：{0}",
                    sm.ApplicationPoolDefaults.Failure.RapidFailProtectionMaxCrashes);
                builder.AppendFormat("<br/>\t允许32位应用程序运行在64位 Windows 上：{0}",
                    sm.ApplicationPoolDefaults.Enable32BitAppOnWin64);

                builder.AppendFormat("<br/>网站默认设置：");
                builder.AppendFormat("<br/>常规：");
                builder.AppendFormat("<br/>\t物理路径凭据：UserName={0}, Password={1}", sm.VirtualDirectoryDefaults.UserName,
                    sm.VirtualDirectoryDefaults.Password);
                builder.AppendFormat("<br/>\t物理路径凭据登录类型：{0}", sm.VirtualDirectoryDefaults.LogonMethod.ToString());
                builder.AppendFormat("<br/>\t应用程序池：{0}", sm.ApplicationDefaults.ApplicationPoolName);
                builder.AppendFormat("<br/>\t自动启动：{0}", sm.SiteDefaults.ServerAutoStart);
                builder.AppendFormat("<br/>行为：");
                builder.AppendFormat("<br/>\t连接限制：");
                builder.AppendFormat("<br/>\t\t连接超时（秒）：{0}", sm.SiteDefaults.Limits.ConnectionTimeout.TotalSeconds);
                builder.AppendFormat("<br/>\t\t最大并发连接数：{0}", sm.SiteDefaults.Limits.MaxConnections);
                builder.AppendFormat("<br/>\t\t最大带宽（字节/秒）：{0}", sm.SiteDefaults.Limits.MaxBandwidth);
                builder.AppendFormat("<br/>\t失败请求跟踪：");
                builder.AppendFormat("<br/>\t\t跟踪文件的最大数量：{0}", sm.SiteDefaults.TraceFailedRequestsLogging.MaxLogFiles);
                builder.AppendFormat("<br/>\t\t目录：{0}", sm.SiteDefaults.TraceFailedRequestsLogging.Directory);
                builder.AppendFormat("<br/>\t\t已启用：{0}", sm.SiteDefaults.TraceFailedRequestsLogging.Enabled);
                builder.AppendFormat("<br/>\t已启用的协议：{0}", sm.ApplicationDefaults.EnabledProtocols);

                foreach (var s in sm.Sites) //遍历网站
                {
                    builder.AppendFormat("<td>模式名：{0}", s.Schema.Name);
                    builder.AppendFormat("<br/>编号：{0}", s.Id);
                    builder.AppendFormat("<br/>网站名称：{0}", s.Name);
                    builder.AppendFormat("<br/>物理路径：{0}", s.Applications["/"].VirtualDirectories["/"].PhysicalPath);
                    builder.AppendFormat("<br/>物理路径凭据：{0}", s.Methods.ToString());
                    builder.AppendFormat("<br/>应用程序池：{0}", s.Applications["/"].ApplicationPoolName);
                    builder.AppendFormat("<br/>已启用的协议：{0}", s.Applications["/"].EnabledProtocols);
                    builder.AppendFormat("<br/>自动启动：{0}", s.ServerAutoStart);

                    builder.AppendFormat("<br/>网站绑定：");
                    foreach (var tmp in s.Bindings)
                    {
                        builder.AppendFormat("<br/>.........类型：{0}", tmp.Protocol);
                        builder.AppendFormat("<br/>.........IP 地址：{0}", tmp.EndPoint.Address.ToString());
                        builder.AppendFormat("<br/>.........端口：{0}", tmp.EndPoint.Port.ToString());
                        builder.AppendFormat("<br/>.........主机名：{0}", tmp.Host);
                    }

                    builder.AppendFormat("<br/>连接限制：");
                    builder.AppendFormat("<br/>连接超时（秒）：{0}", s.Limits.ConnectionTimeout.TotalSeconds);
                    builder.AppendFormat("<br/>最大并发连接数：{0}", s.Limits.MaxConnections);
                    builder.AppendFormat("<br/>最大带宽（字节/秒）：{0}", s.Limits.MaxBandwidth);

                    builder.AppendFormat("<br/>失败请求跟踪：");
                    builder.AppendFormat("<br/>跟踪文件的最大数量：{0}", s.TraceFailedRequestsLogging.MaxLogFiles);
                    builder.AppendFormat("<br/>目录：{0}", s.TraceFailedRequestsLogging.Directory);
                    builder.AppendFormat("<br/>已启用：{0}", s.TraceFailedRequestsLogging.Enabled);

                    if (s.LogFile != null)
                    {
                        builder.AppendFormat("<br/>日志：");
                        builder.AppendFormat("<br/>启用日志服务：{0}", s.LogFile.Enabled);
                        builder.AppendFormat("<br/>格式：{0}", s.LogFile.LogFormat.ToString());
                        builder.AppendFormat("<br/>目录：{0}", s.LogFile.Directory);
                        builder.AppendFormat("<br/>文件包含字段：{0}", s.LogFile.LogExtFileFlags.ToString());
                        builder.AppendFormat("<br/>计划：{0}", s.LogFile.Period.ToString());
                        builder.AppendFormat("<br/>最大文件大小（字节）：{0}", s.LogFile.TruncateSize);
                        builder.AppendFormat("<br/>使用本地时间进行文件命名和滚动更新：{0}", s.LogFile.LocalTimeRollover);
                    }

                    builder.AppendFormat("<br/>应用程序 列表：");
                    foreach (var tmp in s.Applications)
                    {
                        if (tmp.Path != "/")
                        {
                            builder.AppendFormat("<br/>模式名：{0}", tmp.Schema.Name);
                            builder.AppendFormat("<br/>虚拟路径：{0}", tmp.Path);
                            builder.AppendFormat("<br/>物理路径：{0}", tmp.VirtualDirectories["/"].PhysicalPath);
                            builder.AppendFormat("<br/>物理路径凭据：{0}", tmp.Methods.ToString());
                            builder.AppendFormat("<br/>应用程序池：{0}", tmp.ApplicationPoolName);
                            builder.AppendFormat("<br/>已启用的协议：{0}", tmp.EnabledProtocols);
                        }
                        builder.AppendFormat("<br/>虚拟目录 列表：");
                        foreach (var tmp2 in tmp.VirtualDirectories)
                        {
                            if (tmp2.Path != "/")
                            {
                                builder.AppendFormat("<br/>\t模式名：{0}", tmp2.Schema.Name);
                                builder.AppendFormat("<br/>\t虚拟路径：{0}", tmp2.Path);
                                builder.AppendFormat("<br/>\t物理路径：{0}", tmp2.PhysicalPath);
                                builder.AppendFormat("<br/>\t物理路径凭据：{0}", tmp2.Methods.ToString());
                                builder.AppendFormat("<br/>\t物理路径凭据登录类型：{0}", tmp2.LogonMethod.ToString());
                            }
                        }
                    }
                }
            }
            return Content(builder.ToString());
        }

    }
}

