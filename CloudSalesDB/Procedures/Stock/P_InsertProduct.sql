Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_InsertProduct')
BEGIN
	DROP  Procedure  P_InsertProduct
END

GO
/***********************************************************
过程名称： P_InsertProduct
功能描述： 添加产品
参数说明：	 
编写日期： 2015/6/8
程序作者： Allen
调试记录： exec P_InsertProduct 
************************************************************/
CREATE PROCEDURE [dbo].[P_InsertProduct]
@ProductCode nvarchar(200),
@ProductName nvarchar(200),
@GeneralName nvarchar(200),
@IsCombineProduct int,
@BrandID nvarchar(64),
@BigUnitID nvarchar(64),
@SmallUnitID nvarchar(64),
@BigSmallMultiple int,
@CategoryID nvarchar(64),
@Status int,
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
@ProductImg nvarchar(4000),
@Description text,
@ShapeCode nvarchar(50),
@CreateUserID nvarchar(64),
@ClientID nvarchar(64),
@ProductID nvarchar(64) output,
@Result int output--1：成功；0失败
AS

begin tran

declare @Err int,@PIDList nvarchar(max),@SaleAttr  nvarchar(max)
set @Err=0
set @ProductID=NEWID()

select @PIDList=PIDList,@SaleAttr=SaleAttr from Category where CategoryID=@CategoryID

IF(NOT EXISTS(SELECT 1 FROM [Products] WHERE [ProductCode]=@ProductCode and ClientID=@ClientID))--产品编号唯一，编号不存在时才能执行插入
BEGIN
		INSERT INTO [Products]([ProductID],[ProductCode],[ProductName],[GeneralName],[IsCombineProduct],[BrandID],[BigUnitID],[SmallUnitID],[BigSmallMultiple] ,
						[CategoryID],[CategoryIDList],[SaleAttr],[AttrList],[ValueList],[AttrValueList],[CommonPrice],[Price],[PV],[TaxRate],[Status],
						[OnlineTime],[UseType],[IsNew],[IsRecommend] ,[IsDiscount],[DiscountValue],[SaleCount],[Weight] ,[ProductImage],[EffectiveDays],
						[ShapeCode] ,[ProdiverID],[Description],[CreateUserID],[CreateTime] ,[UpdateTime],[OperateIP] ,[ClientID])
				 VALUES(@ProductID,@ProductCode,@ProductName,@GeneralName,@IsCombineProduct,@BrandID,@BigUnitID,@SmallUnitID,@BigSmallMultiple,
						@CategoryID,@PIDList,@SaleAttr,@AttrList,@ValueList,@AttrValueList,@CommonPrice,@Price,@Price,0,@Status,
						getdate(),0,@Isnew,@IsRecommend,1,@DiscountValue,0,@Weight,@ProductImg,@EffectiveDays,@ShapeCode,'',@Description,@CreateUserID,
						getdate(),getdate(),'',@ClientID);

						set @Err+=@@Error

		--不存在规格，插入默认子产品
		if not exists (select AutoID from CategoryAttr where CategoryID=@CategoryID and Type=2 and Status=1)
		begin
			INSERT INTO ProductDetail(ProductDetailID,[ProductID],DetailsCode,[UnitID] ,[SaleAttr],[AttrValue],[SaleAttrValue],[Price],[Status],
					Weight,ImgS,[ShapeCode] ,[Description],[CreateUserID],[CreateTime] ,[UpdateTime],[OperateIP] ,[ClientID])
				VALUES(NEWID(),@ProductID,'',@SmallUnitID,'','','',@Price,1,
					@Weight,'','','',@CreateUserID,getdate(),getdate(),'',@ClientID);
			set @Err+=@@Error
		end


		set @Result=1;
END
ELSE
BEGIN
	set @ProductID='';
	set @Result=0;
END

set @Err+=@@Error

if(@Err>0)
begin
	set @ProductID=''
	rollback tran
end 
else
begin
	commit tran
end