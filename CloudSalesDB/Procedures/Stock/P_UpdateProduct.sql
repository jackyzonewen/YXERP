Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_UpdateProduct')
BEGIN
	DROP  Procedure  P_UpdateProduct
END

GO
/***********************************************************
过程名称： P_UpdateProduct
功能描述： 编辑产品
参数说明：	 
编写日期： 2015/7/2
程序作者： Allen
调试记录： exec P_UpdateProduct 
************************************************************/
CREATE PROCEDURE [dbo].[P_UpdateProduct]
@ProductID nvarchar(64),
@ProductCode nvarchar(200),
@ProductName nvarchar(200),
@GeneralName nvarchar(200),
@IsCombineProduct int,
@BrandID nvarchar(64),
@BigUnitID nvarchar(64),
@SmallUnitID nvarchar(64),
@BigSmallMultiple int,
@Status int,
@CategoryID nvarchar(64),
@AttrList nvarchar(max),
@ValueList nvarchar(max),
@AttrValueList nvarchar(max),
@CommonPrice decimal(18,2),
@Price decimal(18,2),
@Weight decimal(18,2),
@Isnew int,
@IsRecommend int,
@EffectiveDays int,
@DiscountValue decimal(5,4),
@Description text,
@ShapeCode nvarchar(50),
@CreateUserID nvarchar(64),
@ClientID nvarchar(64)
AS

begin tran

declare @Err int,@PIDList nvarchar(max),@SaleAttr  nvarchar(max),@BUnitID nvarchar(64),@MUnitID nvarchar(64)
set @Err=0

select @PIDList=PIDList,@SaleAttr=SaleAttr from C_Category where CategoryID=@CategoryID

select @BUnitID=[BigUnitID],@MUnitID=[SmallUnitID] from [C_Products] where ProductID=@ProductID

Update [C_Products] set [ProductName]=@ProductName,[GeneralName]=@GeneralName,[IsCombineProduct]=@IsCombineProduct,[BrandID]=@BrandID,
						[BigUnitID]=@BigUnitID,[SmallUnitID]=@SmallUnitID,[BigSmallMultiple]=@BigSmallMultiple ,
						[CategoryIDList]=@PIDList,[SaleAttr]=@SaleAttr,[AttrList]=@AttrList,[ValueList]=@ValueList,[AttrValueList]=@AttrValueList,
						[CommonPrice]=@CommonPrice,[Price]=@Price,[PV]=0,[Status]=@Status,
						[IsNew]=@Isnew,[IsRecommend]=@IsRecommend ,[DiscountValue]=@DiscountValue,[Weight]=@Weight ,[EffectiveDays]=@EffectiveDays,
						[ShapeCode]=@ShapeCode ,[Description]=@Description ,[UpdateTime]=getdate()
where ProductID=@ProductID


if(@BUnitID<>@BigUnitID and @BUnitID<>@MUnitID)
begin
	update C_ProductDetail set UnitID=@BigUnitID where ProductID=@ProductID and UnitID=@BUnitID
	set @Err+=@@Error
end
if(@MUnitID<>@SmallUnitID)
begin
	update C_ProductDetail set UnitID=@SmallUnitID where ProductID=@ProductID and UnitID=@MUnitID
	set @Err+=@@Error
end


set @Err+=@@Error

if(@Err>0)
begin
	rollback tran
end 
else
begin
	commit tran
end