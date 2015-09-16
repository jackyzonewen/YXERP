Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_InsertClient')
BEGIN
	DROP  Procedure  P_InsertClient
END

GO
/***********************************************************
过程名称： P_InsertClient
功能描述： 添加客户端
参数说明：	 
编写日期： 2015/4/10
程序作者： Allen
调试记录： exec P_InsertClient 
************************************************************/
CREATE PROCEDURE [dbo].[P_InsertClient]
@ClientiD nvarchar(64),
@CompanyName nvarchar(200),
@MobilePhone nvarchar(64),
@Industry nvarchar(64),
@CityCode nvarchar(10),
@Address nvarchar(200),
@Description nvarchar(200),
@ContactName nvarchar(50),
@LoginName nvarchar(200),
@LoginPWD nvarchar(64),
@Modules nvarchar(4000),
@CreateUserID nvarchar(64),
@Result int output --0：失败，1：成功，2 账号已存在
AS

begin tran

set @Result=0

declare @Err int ,@DepartID nvarchar(64),@RoleID nvarchar(64),@UserID nvarchar(64)

select @Err=0,@DepartID=NEWID(),@RoleID=NEWID(),@UserID=NEWID()

set @Modules='''' + REPLACE(@Modules,',',''',''') + ''''

--客户端
insert into Clients(ClientID,CompanyName,ContactName,MobilePhone,Status,Industry,CityCode,Address,Description,CreateUserID) 
				values(@ClientiD,@CompanyName,@ContactName,@MobilePhone,1,@Industry,@CityCode,@Address,@Description,@CreateUserID)

set @Err+=@@error
--客户端模块
exec('insert into ClientModules(ClientID,ModulesID,CreateUserID) select '''+@ClientiD+''',ModulesID,'''+@CreateUserID+''' from Modules where ModulesID in('+@Modules+')')
set @Err+=@@error

--部门
insert into Department(DepartID,Name,Status,CreateUserID,ClientID) values (@DepartID,'系统管理',1,@UserID,@ClientID)
set @Err+=@@error

--角色
insert into Role(RoleID,Name,Status,IsDefault,CreateUserID,ClientID) values (@RoleID,'管理员',1,1,@UserID,@ClientID)

set @Err+=@@error
--管理员账号已存在
if exists(select UserID from Users where LoginName=@LoginName)
begin
	set @Result=2
	rollback tran
	return
end
insert into Users(UserID,LoginName,LoginPWD,Name,MobilePhone,Allocation,Status,IsDefault,CreateUserID,ClientID)
             values(@UserID,@LoginName,@LoginPWD,@ContactName,@MobilePhone,1,1,1,@UserID,@ClientID)

--管理员部门
insert into UserDepart(UserID,DepartID,CreateUserID,ClientID) values(@UserID,@DepartID,@UserID,@ClientID)  
set @Err+=@@error
   
--管理员角色
insert into UserRole(UserID,RoleID,CreateUserID,ClientID) values(@UserID,@RoleID,@UserID,@ClientID) 
set @Err+=@@error

if(@Err>0)
begin
	set @Result=0
	rollback tran
end 
else
begin
	set @Result=1
	commit tran
end