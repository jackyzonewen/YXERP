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
@CreateUserID nvarchar(64),
@ClientID nvarchar(64),
@ProductID nvarchar(64) output 
AS

begin tran

declare @Err int,@PIDList nvarchar(max),@SaleAttr  nvarchar(max)
set @Err=0
set @ProductID=NEWID()

select @PIDList=PIDList,@SaleAttr=SaleAttr from C_Category where CategoryID=@CategoryID


if(@Err>0)
begin
	set @ProductID=''
	rollback tran
end 
else
begin
	commit tran
end