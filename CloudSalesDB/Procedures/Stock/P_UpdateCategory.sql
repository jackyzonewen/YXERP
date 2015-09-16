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

declare @Err int, @Layers int=0 
set @Err=0

Update Category set CategoryName=@CategoryName,Status=@Status,AttrList=@AttrList,SaleAttr=@SaleAttr,Description=@Description,UpdateTime=getdate() 
where CategoryID=@CategoryID
set @Err+=@@error



if(@Err>0)
begin
	rollback tran
end 
else
begin
	commit tran
end