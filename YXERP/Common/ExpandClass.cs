using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CloudSalesEntity;

public static class ExpandClass
{
    /// <summary>
    /// 顶层菜单编码
    /// </summary>
    public const string MANAGE_TOP_CODE = "100000000";
    /// <summary>
    /// 默认菜单编码
    /// </summary>
    public const string MANAGE_DEFAULT_CODE = "101000000";


    /// <summary>
    /// 获取下级菜单
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="menuCode"></param>
    /// <returns></returns>
    public static List<Menu> GetChildMenuByCode(HttpContext httpContext, string menuCode)
    {
        if (httpContext.Session["ClientManager"] != null)
        {
            return ((CloudSalesEntity.C_Users)httpContext.Session["ClientManager"]).Menus.Where(m => m.PCode == menuCode).ToList();
        }
        else
        {
            return new List<Menu>();
        }
    }
    /// <summary>
    /// 加载二级菜单
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static List<Menu> GetControllers(HttpContext httpContext)
    {
        if (httpContext.Session["ClientManager"] != null && httpContext.Session["topMenuCode"] != null)
        {
            return ((CloudSalesEntity.C_Users)httpContext.Session["ClientManager"]).Menus.Where(m => m.PCode == httpContext.Session["topMenuCode"].ToString()).ToList();
        }
        else
        {
            return new List<Menu>();
        }
    }
    /// <summary>
    /// 获取二级菜单选中项
    /// </summary>
    /// <param name="html"></param>
    /// <param name="httpContext"></param>
    /// <param name="menuCode"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public static string GetTopMenuActive(this HtmlHelper html, HttpContext httpContext, string menuCode, string style)
    {
        if (httpContext.Session["topMenuCode"] != null && httpContext.Session["topMenuCode"].ToString() == menuCode)
        {
            return style;
        }
        else if (httpContext.Session["topMenuCode"] == null && menuCode == MANAGE_DEFAULT_CODE)
        {
            return style;
        }
        return "";
    }
}
