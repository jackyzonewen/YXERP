Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetFilterProducts')
BEGIN
	DROP  Procedure  P_GetFilterProducts
END

GO
/***********************************************************
过程名称： P_GetFilterProducts
功能描述： 获取产品列表
参数说明：	 
编写日期： 2015/9/5
程序作者： Allen
调试记录：declare @totalCount int ,@pageCount int 
		  exec P_GetFilterProducts 
		  @CategoryID='',
		  @keyWords='',
		  @orderColumn=' pd.Price ',
		  @isAsc=0,
		  @pageSize=20,
		  @pageIndex=1,
		  @totalCount =@totalCount,
		  @pageCount =@pageCount,
		  @ClientID='d583bf9e-1243-44fe-ac5c-6fbc118aae36'
************************************************************/
CREATE PROCEDURE [dbo].[P_GetFilterProducts]
	@CategoryID nvarchar(64),
	@BeginPrice nvarchar(20)='',
	@EndPrice nvarchar(20)='',
	@Where nvarchar(4000)='',
	@keyWords nvarchar(4000),
	@orderColumn nvarchar(500)='',
	@isAsc int=0,
	@pageSize int,
	@pageIndex int,
	@totalCount int output ,
	@pageCount int output,
	@ClientID nvarchar(64)
AS
	declare @tableName nvarchar(4000),
	@columns nvarchar(4000),
	@condition nvarchar(4000),
	@key nvarchar(100)
	

	set @tableName='C_Products P join C_Brand B on P.BrandID=B.BrandID 
					join C_ProductDetail pd on p.ProductID=pd.ProductID and pd.Status<>9 '
	set @columns='P.ProductID,P.ProductName,p.CommonPrice,isnull(pd.price,p.price) price,B.Name BrandName,
				  ISNULL(pd.ImgS,p.ProductImage) ProductImage,ISNULL(pd.SaleCount,p.SaleCount) SaleCount,pd.ProductDetailID,pd.AttrValue '
	set @key='pd.AutoID'
	set @condition=' P.ClientID='''+@ClientID+''' and P.Status<>9 '

	if(@CategoryID<>'' and @CategoryID<> '-1')
	begin
		set @condition +=' and P.CategoryIDList like ''%'+@CategoryID+'%'''
	end

	if(@BeginPrice<>'')
	begin
		set @condition +=' and pd.Price>='+@BeginPrice
	end

	if(@EndPrice<>'')
	begin
		set @condition +=' and pd.Price<='+@EndPrice
	end

	if(@keyWords <> '')
	begin
		set @condition +=' and (ProductName like ''%'+@keyWords+'%'' or  ProductCode like ''%'+@keyWords+'%'' or  GeneralName like ''%'+@keyWords+'%'') '
	end

	set @condition += @Where

	declare @total int,@page int
	exec P_GetPagerData @tableName,@columns,@condition,@key,@orderColumn,@pageSize,@pageIndex,@total out,@page out,@isAsc 
	select @totalCount=@total,@pageCount =@page
 

