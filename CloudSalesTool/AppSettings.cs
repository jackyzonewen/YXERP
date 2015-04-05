using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSalesTool
{

    public enum AppSettingsWEB
    {
        Manage = 1,
        Client = 2
    }

    /// <summary>
    /// 获取config AppSettings配置信息
    /// </summary>
    public class AppSettings
    {
        public static Dictionary<string, string> _appSettings = new Dictionary<string, string>();
        /// <summary>
        /// 获取网站AppSettings配置信息
        /// </summary>
        /// <param name="web"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[AppSettingsWEB web, string key]
        {
            get
            {
                string webKey = web.ToString() + key;
                if (_appSettings.ContainsKey(webKey))
                {
                    return _appSettings[webKey];
                }
                else
                {
                    string value = System.Configuration.ConfigurationManager.AppSettings[key];
                    if (string.IsNullOrEmpty(value))
                    {
                        return null;
                    }
                    else
                    {
                        _appSettings.Add(webKey, value);
                        return value;
                    }
                }
            }
        }
    }
}
