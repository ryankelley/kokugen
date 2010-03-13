#region Using Directives

using System.Configuration;
using FubuMVC.Core.Util;
using HtmlTags;
using Kokugen.Core.Domain;

#endregion

namespace Kokugen.Core.Config
{
    public static class ConfigExtensions
    {
        public static SiteConfiguration FromAppSetting(this SiteConfiguration config, string appSettingName)
        {
            var json = ConfigurationManager.AppSettings[appSettingName];
            var dto = JsonUtil.Get<SiteConfigDTO>(json);
            dto.ToSiteConfiguration(config);
            return config;
        }
    }
}