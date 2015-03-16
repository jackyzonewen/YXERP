using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


public static class ExpandClass
{
    public static string GetActiveMenu(this HtmlHelper html, string action, string param, string style)
    {
        return action.ToLower() == param.ToLower() ? style : "";
    }
}
