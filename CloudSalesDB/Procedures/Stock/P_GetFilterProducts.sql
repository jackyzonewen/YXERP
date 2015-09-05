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
调试记录：declare @totalCount int,@pageCount int 
		  exec P_GetFilterProducts '','',1,20,1,@totalCount output,@pageCount output,'d583bf9e-1243-44fe-ac5c-6fbc118aae36'
************************************************************/
CREATE PROCEDURE [dbo].[P_GetFilterProducts]
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
					left join C_ProductDetail pd on p.ProductID=pd.ProductID and pd.Status<>9 '
	set @columns='P.ProductID,P.ProductName,p.CommonPrice,isnull(pd.price,p.price) price,B.Name BrandName,
				  ISNULL(pd.ImgS,p.ProductImage) ProductImage,ISNULL(pd.SaleCount,p.SaleCount) SaleCount,pd.ProductDetailID,pd.AttrValue '
	set @key='P.AutoID'
	set @condition=' P.ClientID='''+@ClientID+''' and P.Status<>9 '
	if(@keyWords <> '')
	begin
		set @condition +=' and (ProductName like ''%'+@keyWords+'%'' or  ProductCode like ''%'+@keyWords+'%'' or  GeneralName like ''%'+@keyWords+'%'') '
	end

	declare @total int,@page int
	exec P_GetPagerData @tableName,@columns,@condition,@key,@orderColumn,@pageSize,@pageIndex,@total out,@page out,@isAsc 
	select @totalCount=@total,@pageCount =@page
 

