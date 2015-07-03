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
调试记录： exec P_GetProductByID '1BCB1EB3-3088-4D71-8ABE-52127B08277C'
************************************************************/
CREATE PROCEDURE [dbo].[P_GetProductByID]
	@ProductID nvarchar(64)
AS

select * from C_Products where ProductID=@ProductID 

select * from C_ProductDetail where ProductID=@ProductID
 

