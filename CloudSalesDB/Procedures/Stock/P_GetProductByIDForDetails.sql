Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetProductByIDForDetails')
BEGIN
	DROP  Procedure  P_GetProductByIDForDetails
END

GO
/***********************************************************
过程名称： P_GetProductByIDForDetails
功能描述： 获取产品详情（加入购物车页面）
参数说明：	 
编写日期： 2015/7/1
程序作者： Allen
调试记录： exec P_GetProductByIDForDetails 'CD867D63-B61D-47DE-9C63-0B1A56D68486'
************************************************************/
CREATE PROCEDURE [dbo].[P_GetProductByIDForDetails]
	@ProductID nvarchar(64)
AS

declare @BigUnit nvarchar(64),@Unit nvarchar(64),@CategoryID nvarchar(64)

select @BigUnit=BigUnitID,@Unit=SmallUnitID,@CategoryID=CategoryID from Products where ProductID=@ProductID

select * from Products where ProductID=@ProductID 

select * from ProductDetail where ProductID=@ProductID

select UnitID,UnitName from ProductUnit where UnitID=@BigUnit or UnitID=@Unit

select p.AttrID,p.AttrName,c.Type into #AttrTable from ProductAttr p join CategoryAttr c on p.AttrID=c.AttrID 
where c.Status=1 and c.CategoryID= @CategoryID and p.Status=1 order by p.AutoID
--属性
select * from #AttrTable
--属性值
select ValueID,ValueName,AttrID from AttrValue  where AttrID in (select AttrID from #AttrTable) and Status<>9
 

