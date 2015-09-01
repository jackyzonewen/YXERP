Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetAttrsByCategoryID')
BEGIN
	DROP  Procedure  P_GetAttrsByCategoryID
END

GO
/***********************************************************
过程名称： P_GetAttrsByCategoryID
功能描述： 获取分类属性列表
参数说明：	 
编写日期： 2015/7/14
程序作者： Allen
调试记录： exec P_GetAttrsByCategoryID 
************************************************************/
CREATE PROCEDURE [dbo].[P_GetAttrsByCategoryID]
	@CategoryID nvarchar(64)='',
	@ClientID nvarchar(64)
AS
if(@CategoryID='')
begin
	select AttrID,AttrName,Description from C_ProductAttr where ClientID=@ClientID and CategoryID=@CategoryID and Status<>9
end
else
begin
	select c.AttrID,AttrName,Description,c.Type,c.CategoryID from C_ProductAttr p join C_CategoryAttr  c on p.AttrID=c.AttrID
	where c.CategoryID=@CategoryID and p.Status<>9 and c.Status<>9
end
 

