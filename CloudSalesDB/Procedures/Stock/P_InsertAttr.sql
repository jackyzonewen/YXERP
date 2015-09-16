Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_InsertAttr')
BEGIN
	DROP  Procedure  P_InsertAttr
END

GO
/***********************************************************
过程名称： P_InsertAttr
功能描述： 添加产品属性
参数说明：	 
编写日期： 2015/7/13
程序作者： Allen
调试记录： exec P_InsertAttr 
************************************************************/
CREATE PROCEDURE [dbo].[P_InsertAttr]
@AttrID nvarchar(64),
@AttrName nvarchar(200),
@Description nvarchar(4000),
@CategoryID nvarchar(64),
@Type int=1,
@CreateUserID nvarchar(64),
@ClientID nvarchar(64)
AS

begin tran

declare @Err int
set @Err=0

INSERT INTO ProductAttr([AttrID] ,[AttrName],[Description],CategoryID,[Status],CreateUserID,ClientID) 
					values(@AttrID ,@AttrName,@Description,@CategoryID,1,@CreateUserID,@ClientID)
set @Err+=@@error

if(@CategoryID is not null and @CategoryID<>'')
begin
	insert into CategoryAttr(CategoryID,AttrID,Status,[Type],CreateUserID)
	values(@CategoryID,@AttrID,1,@Type,@CreateUserID)
end

if(@Err>0)
begin
	rollback tran
end 
else
begin
	commit tran
end