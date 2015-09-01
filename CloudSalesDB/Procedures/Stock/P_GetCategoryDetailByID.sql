Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetCategoryDetailByID')
BEGIN
	DROP  Procedure  P_GetCategoryDetailByID
END

GO
/***********************************************************
过程名称： P_GetCategoryDetailByID
功能描述： 获取产品分类详情
参数说明：	 
编写日期： 2015/6/1
程序作者： Allen
调试记录： exec P_GetCategoryDetailByID '05678785-6286-46FC-AB5C-C65280EF7B49'
************************************************************/
CREATE PROCEDURE [dbo].[P_GetCategoryDetailByID]
	@CategoryID nvarchar(64)
AS

select * from C_Category where CategoryID=@CategoryID

select p.*,c.Type into #AttrTable from C_ProductAttr p join C_CategoryAttr c on p.AttrID=c.AttrID 
where c.Status=1 and c.CategoryID= @CategoryID and p.Status=1 order by p.AutoID
--属性
select * from #AttrTable
--属性值
select * from C_AttrValue  where AttrID in (select AttrID from #AttrTable) and Status<>9


 

