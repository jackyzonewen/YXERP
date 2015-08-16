Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_UpdateProductDetail')
BEGIN
	DROP  Procedure  P_UpdateProductDetail
END

GO
/***********************************************************
过程名称： P_UpdateProductDetail
功能描述： 编辑子产品
参数说明：	 
编写日期： 2015/8/16
程序作者： Allen
调试记录： exec P_UpdateProductDetail 
************************************************************/
CREATE PROCEDURE [dbo].[P_UpdateProductDetail]
@DetailID nvarchar(64),
@ProductCode nvarchar(200),
@ProductID nvarchar(64),
@UnitID nvarchar(64),
@AttrList nvarchar(max),
@ValueList nvarchar(max),
@AttrValueList nvarchar(max),
@Price decimal(18,2),
@Weight decimal(18,2),
@Description text,
@ShapeCode nvarchar(50),
@Result int output--1：成功；0失败
AS

begin tran

declare @Err int
set @Err=0
set @Result=0

if exists(select AutoID from C_ProductDetail where ProductID=@ProductID and UnitID=@UnitID and [AttrValue]=@ValueList and ProductDetailID<>@DetailID)
begin
	rollback tran
	return
end

update C_ProductDetail set DetailsCode=@ProductCode ,[SaleAttr]=@AttrList,[AttrValue]=@ValueList,[SaleAttrValue]=@AttrValueList,[Price]=@Price,
					[Weight]=@Weight,[ShapeCode]=@ShapeCode ,[Description]=@Description ,[UpdateTime]=getdate()
where ProductDetailID=@DetailID

set @Err+=@@Error

if(@Err>0)
begin
	
	rollback tran
end 
else
begin
	set @Result=1
	commit tran
end