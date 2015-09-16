Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_AddCategoryAttr')
BEGIN
	DROP  Procedure  P_AddCategoryAttr
END

GO
/***********************************************************
过程名称： P_AddCategoryAttr
功能描述： 添加分类属性
参数说明：	 
编写日期： 2015/8/3
程序作者： Allen
调试记录： exec P_AddCategoryAttr 
************************************************************/
CREATE PROCEDURE [dbo].[P_AddCategoryAttr]
@AttrID nvarchar(64),
@CategoryID nvarchar(64),
@Type int=1,
@CreateUserID nvarchar(64)
AS
begin tran

declare @Err int
set @Err=0


if not exists(select AutoID from CategoryAttr where CategoryID=@CategoryID and AttrID=@AttrID and [Type]=@Type and Status=1)
begin
	insert into CategoryAttr(CategoryID,AttrID,Status,[Type],CreateUserID)
	values(@CategoryID,@AttrID,1,@Type,@CreateUserID)
end

set @Err+=@@error

if(@Err>0)
begin
	rollback tran
end 
else
begin
	commit tran
end