Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_InsertCategory')
BEGIN
	DROP  Procedure  P_InsertCategory
END

GO
/***********************************************************
过程名称： P_InsertCategory
功能描述： 添加产品分类
参数说明：	 
编写日期： 2015/5/25
程序作者： Allen
调试记录： exec P_InsertCategory 
************************************************************/
CREATE PROCEDURE [dbo].[P_InsertCategory]
@CategoryCode nvarchar(200),
@CategoryName nvarchar(200),
@PID nvarchar(64),
@AttrList nvarchar(4000),
@SaleAttr nvarchar(4000),
@Description nvarchar(4000),
@CreateUserID nvarchar(64),
@ClientID nvarchar(64),
@CategoryID nvarchar(64) output 
AS

begin tran

set @CategoryID=NEWID()

declare @Err int 

set @Err=0


if(@Err>0)
begin
	set @CategoryID=''
	rollback tran
end 
else
begin
	commit tran
end