using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Web.Administration;

namespace PreviewServerSite.Models
{
    public class SiteModel
    {
        public SiteModel(Site s)
        {
            this.编号 = s.Id.ToString();
            this.模式名 = s.Schema.Name;
            this.网站名称 = s.Name;
            this.物理路径 = s.Applications["/"].VirtualDirectories["/"].PhysicalPath;
            this.应用程序池 = s.Applications["/"].ApplicationPoolName;
            this.已启用的协议 = s.Applications["/"].EnabledProtocols;
            this.自动启动 = s.ServerAutoStart;
        }
        public string 编号 { get; set; }
        public string 模式名 { get; set; }
        public string 网站名称 { get; set; }
        public string 物理路径 { get; set; }
        public string 应用程序池 { get; set; }
        public string 已启用的协议 { get; set; }
        public bool 自动启动 { get; set; }
    }

    public enum SiteOperat
    {
        启动,
        停止,
        删除
    }
    //public static class SiteModelExt
    //{
    //    public static string ToBuilderString(this SiteModel model)
    //    {
    //        StringBuilder builder = new StringBuilder();
    //        builder.AppendFormat("<br/>编号：{0}", model.编号);
    //        builder.AppendFormat("<br/>模式名：{0}", model.模式名);
    //        builder.AppendFormat("<br/>网站名称：{0}", model.网站名称);
    //        builder.AppendFormat("<br/>物理路径：{0}", model.物理路径);
    //        builder.AppendFormat("<br/>应用程序池：{0}", model.应用程序池);
    //        builder.AppendFormat("<br/>已启用的协议：{0}", model.已启用的协议);
    //        builder.AppendFormat("<br/>自动启动：{0}", model.自动启动);
    //        return builder.ToString();
    //    }
    //}
}