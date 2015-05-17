Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetModulesMenu')
BEGIN
	DROP  Procedure  P_GetModulesMenu
END

GO
/***********************************************************
过程名称： P_GetModulesMenu
功能描述： 模块-菜单列表
参数说明：      
编写日期： 2015/5/4
程序作者： Allen
调试记录： exec P_GetModulesMenu 
************************************************************/
CREATE PROCEDURE [dbo].[P_GetModulesMenu]
AS
select * from M_Modules 

select mm.ModulesID,m.* from M_ModulesMenu mm join Menu m on mm.MenuCode=m.MenuCode where m.IsHide=0 and m.Type=1 order by m.Sort
