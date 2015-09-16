Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetDepotSeats')
BEGIN
	DROP  Procedure  P_GetDepotSeats
END

GO
/***********************************************************
过程名称： P_GetDepotSeats
功能描述： 获取货位列表
参数说明：	 
编写日期： 2015/7/25
程序作者： Allen
调试记录： exec P_GetDepotSeats '',20,1,0,0,'e64d3ec0-864a-4c9e-95f9-770808bfd63f',''
************************************************************/
CREATE PROCEDURE [dbo].[P_GetDepotSeats]
	@keyWords nvarchar(4000),
	@pageSize int,
	@pageIndex int,
	@totalCount int output ,
	@pageCount int output,
	@ClientID nvarchar(64),
	@WareID  nvarchar(64)
AS
	declare @tableName nvarchar(4000),
	@columns nvarchar(4000),
	@condition nvarchar(4000),
	@key nvarchar(100),
	@orderColumn nvarchar(4000),
	@isAsc int

	select @tableName='DepotSeat d join WareHouse w on d.WareID=w.WareID',@columns='d.*,w.Name as WareName',@key='d.AutoID',@orderColumn='d.CreateTime desc',@isAsc=0
	set @condition=' w.ClientID='''+@ClientID+''' and d.Status<>9 and  w.Status<>9 '
	if(@keyWords <> '')
	begin
		set @condition +=' and (d.Name like ''%'+@keyWords+'%'' or  DepotCode like ''%'+@keyWords+'%'') '
	end

	if(@WareID<>'' and @WareID<>'-1')
	begin
		set @condition+=' and d.wareid='''+@WareID+''''
	end

	declare @total int,@page int
	exec P_GetPagerData @tableName,@columns,@condition,@key,@orderColumn,@pageSize,@pageIndex,@total out,@page out,@isAsc 
	select @totalCount=@total,@pageCount =@page
 

