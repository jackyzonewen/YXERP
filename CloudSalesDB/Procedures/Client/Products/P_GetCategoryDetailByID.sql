Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetCategoryDetailByID')
BEGIN
	DROP  Procedure  P_GetCategoryDetailByID
END

GO
/***********************************************************
过程名称： P_GetCategoryDetailByID
功能描述： 获取产品分类详情
参数说明：	 
编写日期： 2015/6/1
程序作者： Allen
调试记录： exec P_GetCategoryDetailByID '40AE522A-B104-47D4-BAC7-658BE0FFC8B6'
************************************************************/
CREATE PROCEDURE [dbo].[P_GetCategoryDetailByID]
	@CategoryID nvarchar(64)
AS

declare @SaleAttr nvarchar(max),@AttrList nvarchar(max)

select * into #Category from C_Category where CategoryID=@CategoryID

select @SaleAttr=SaleAttr,@AttrList=AttrList from #Category

set @SaleAttr=replace(@SaleAttr,',',''',''')
set @AttrList=replace(@AttrList,',',''',''')

create table #AttrTable(AttrID nvarchar(64))

exec('insert into #AttrTable select AttrID from C_ProductAttr where AttrID in('''+@SaleAttr+''') or AttrID in('''+@AttrList+''')')
select * from #Category
select * from C_ProductAttr where AttrID in (select AttrID from #AttrTable)
select * from C_AttrValue  where AttrID in (select AttrID from #AttrTable) and Status<>9

drop table #Category
drop table #AttrTable
 

