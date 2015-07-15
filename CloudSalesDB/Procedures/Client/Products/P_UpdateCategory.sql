Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_UpdateCategory')
BEGIN
	DROP  Procedure  P_UpdateCategory
END

GO
/***********************************************************
过程名称： P_UpdateCategory
功能描述： 编辑产品分类
参数说明：	 
编写日期： 2015/7/15
程序作者： Allen
调试记录： exec P_UpdateCategory 
************************************************************/
CREATE PROCEDURE [dbo].[P_UpdateCategory]
@CategoryID nvarchar(64),
@CategoryName nvarchar(200),
@AttrList nvarchar(4000),
@SaleAttr nvarchar(4000),
@Status int,
@Description nvarchar(4000),
@UserID nvarchar(64)
AS

begin tran

declare @Err int, @Layers int=0 ,@OldAttrList nvarchar(4000),@OldSaleAttr nvarchar(4000)
set @Err=0

select @Layers=Layers,@OldAttrList=AttrList,@OldSaleAttr=SaleAttr from C_Category where CategoryID=@CategoryID

Update C_Category set CategoryName=@CategoryName,Status=@Status,AttrList=@AttrList,SaleAttr=@SaleAttr,Description=@Description,UpdateTime=getdate() 
where CategoryID=@CategoryID
set @Err+=@@error

if(@Layers=3)
begin

	update a set Status=9,UpdateTime=getdate() from C_CategoryAttr a join C_ProductAttr p on a.AttrID=p.AttrID and p.CategoryID='' where a.CategoryID=@CategoryID and a.Status<>9

	set @AttrList=replace(@AttrList,',',''',''')
	update C_CategoryAttr set Status=9 where CategoryID=@CategoryID
	exec( 'insert into C_CategoryAttr(CategoryID,AttrID,Status,Type,CreateUserID) select '''+@CategoryID+''',AttrID,1,1,'''+@UserID+''' from C_ProductAttr where AttrID in('''+@AttrList+''')')
	set @Err+=@@error

	set @SaleAttr=replace(@SaleAttr,',',''',''')
	exec( 'insert into C_CategoryAttr(CategoryID,AttrID,Status,Type,CreateUserID) select '''+@CategoryID+''',AttrID,1,2,'''+@UserID+''' from C_ProductAttr where AttrID in('''+@SaleAttr+''')')
	set @Err+=@@error

end

if(@Err>0)
begin
	rollback tran
end 
else
begin
	commit tran
end