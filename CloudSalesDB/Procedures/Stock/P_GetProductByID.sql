Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetProductByID')
BEGIN
	DROP  Procedure  P_GetProductByID
END

GO
/***********************************************************
过程名称： P_GetProductByID
功能描述： 获取产品详情
参数说明：	 
编写日期： 2015/7/1
程序作者： Allen
调试记录： exec P_GetProductByID 'CD867D63-B61D-47DE-9C63-0B1A56D68486'
************************************************************/
CREATE PROCEDURE [dbo].[P_GetProductByID]
	@ProductID nvarchar(64)
AS

declare @BigUnit nvarchar(64),@Unit nvarchar(64)

select @BigUnit=BigUnitID,@Unit=SmallUnitID from Products where ProductID=@ProductID

select * from Products where ProductID=@ProductID 

select * from ProductDetail where ProductID=@ProductID

select UnitID,UnitName from ProductUnit where UnitID=@BigUnit or UnitID=@Unit
 

