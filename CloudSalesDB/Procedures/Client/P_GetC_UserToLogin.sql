Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetC_UserToLogin')
BEGIN
	DROP  Procedure  P_GetC_UserToLogin
END

GO
/***********************************************************
过程名称： P_GetC_UserToLogin
功能描述： 验证管理员登录并返回信息
参数说明：	 
编写日期： 2015/4/22
程序作者： Allen
调试记录： exec P_GetC_UserToLogin 'Admin','ADA9D527563353B415575BD5BAAE0469'
************************************************************/
CREATE PROCEDURE [dbo].[P_GetC_UserToLogin]
@LoginName nvarchar(200),
@LoginPWD nvarchar(64)
AS

declare @C_UserID nvarchar(64)

select @C_UserID = UserID from C_Users where LoginName=@LoginName and LoginPWD=@LoginPWD

if(@C_UserID is not null)
begin
    select RoleID into #Roles from C_UserRole where UserID=@C_UserID

	--会员信息
	select * from C_Users where UserID=@C_UserID

	--部门信息
	select d.* from C_UserDepart u join C_Department d on u.DepartID=d.DepartID  where u.UserID=@C_UserID 

	--角色信息
	select d.* from C_UserRole u join C_Role d on u.RoleID=d.RoleID  where u.UserID=@C_UserID

	--权限信息
	select p.PermissionID,p.Name,p.MenuCode,p.Action from C_RolePermission r 
	join Permission p on r.PermissionID=p.PermissionID 
	where r.RoleID in (select RoleID from #Roles)
	group by p.PermissionID,p.Name,p.MenuCode,p.Action
end

 

