Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetProductList')
BEGIN
	DROP  Procedure  P_GetProductList
END

GO
/***********************************************************
过程名称： P_GetProductList
功能描述： 获取产品列表
参数说明：	 
编写日期： 2015/6/29
程序作者： Allen
调试记录：declare @totalCount int,@pageCount int exec P_GetProductList '',20,1,@totalCount output,@pageCount output,'8ee5129d-1b93-481d-88d5-d33775dfc602'
************************************************************/
CREATE PROCEDURE [dbo].[P_GetProductList]
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

	select @tableName='C_Products P join C_Brand B on P.BrandID=B.BrandID join C_Category C on P.CategoryID=C.CategoryID',@columns='P.*,B.Name BrandName,C.CategoryName ',@key='P.AutoID',@orderColumn='P.CreateTime desc',@isAsc=0
	set @condition=' P.ClientID='''+@ClientID+''' and P.Status<>9 '
	if(@keyWords <> '')
	begin
		set @condition +=' and (ProductName like ''%'+@keyWords+'%'' or  ProductCode like ''%'+@keyWords+'%'' or  GeneralName like ''%'+@keyWords+'%'') '
	end

	declare @total int,@page int
	exec P_GetPagerData @tableName,@columns,@condition,@key,@orderColumn,@pageSize,@pageIndex,@total out,@page out,@isAsc 
	select @totalCount=@total,@pageCount =@page
 

