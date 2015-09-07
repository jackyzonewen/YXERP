using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CloudSalesEnum;

namespace YXERP.Models
{
    [Serializable]
    public class FilterProduct
    {
        public string CategoryID { get; set; }

        public string BeginPrice { get; set; }

        public string EndPrice { get; set; }

        public string Keywords { get; set; }

        public string OrderBy { get; set; }

        public bool IsAsc { get; set; }

        public int PageIndex { get; set; }

        public List<FilterAttr> Attrs { get; set; }

    }
    [Serializable]
    public class FilterAttr
    {
        public string AttrID { get; set; }
        public string ValueID { get; set; }
        public EnumAttrType Type { get; set; }
    }
}