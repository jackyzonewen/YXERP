using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSalesTool
{
    public class GlobalSession
    {
        private static CacheSession _sesson;
        public static CacheSession Sesson
        {
            get 
            {
                if (_sesson == null)
                {
                    _sesson = new CacheSession();
                }
                return _sesson;
            }
        }
    }

    public class CacheSession
    {
        /// <summary>
        /// Cookies替换session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                return null;
            }
        }
    }
}
