Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetProductAttrList')
BEGIN
	DROP  Procedure  P_GetProductAttrList
END

GO
/***********************************************************
过程名称： P_GetProductAttrList
功能描述： 获取产品属性列表
参数说明：	 
编写日期： 2015/5/19
程序作者： Allen
调试记录： exec P_GetProductAttrList 
************************************************************/
CREATE PROCEDURE [dbo].[P_GetProductAttrList]
	@keyWords nvarchar(4000),
	@pageSize int,
	@pageIndex int,
	@totalCount int output ,
	@pageCount int output,
	@ClientID nvarchar(64)
AS
	declare @tableName nvarchar(4000),
	@columns nvarchar(4000),
	@condition nvarchar(4000),
	@key nvarchar(100),
	@orderColumn nvarchar(4000),
	@isAsc int

	create table #Attrs(AttrID nvarchar(64),AttrName nvarchar(200),Description nvarchar(4000))
	
	select @tableName=' C_ProductAttr ',@columns='AttrID,AttrName,Description',@key='AutoID',@orderColumn='',@isAsc=0
	set @condition=' ClientID='''+@ClientID+''' and Status<>9 '
	if(@keyWords <> '')
	begin
		set @condition +=' and AttrName like ''%'+@keyWords+'%'' or  Description like ''%'+@keyWords+'%'''
	end

	declare @total int,@page int
	insert into #Attrs exec P_GetPagerData @tableName,@columns,@condition,@key,@orderColumn,@pageSize,@pageIndex,@total out,@page out,@isAsc
	
	select * from #Attrs
	select * from C_AttrValue where AttrID in (select AttrID from #Attrs) and Status<>9
	
	select @totalCount=@total,@pageCount =@page
 

