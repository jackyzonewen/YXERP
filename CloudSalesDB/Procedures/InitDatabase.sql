Use [CloudSales1.0]
GO
Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'InitDatabase')
BEGIN
	DROP  Procedure  InitDatabase
END

GO
/***********************************************************
过程名称： InitDatabase
功能描述： 初始化数据库
参数说明：	 
编写日期： 2015/4/10
程序作者： Allen
调试记录： exec InitDatabase 
************************************************************/
CREATE PROCEDURE [dbo].[InitDatabase]
AS
