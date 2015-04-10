Use [CloudSales1.0]
GO
Use [CloudSales1.0]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'P_GetPagerData')
BEGIN
	DROP  Procedure  P_GetPagerData
END

GO
/***********************************************************
过程名称： P_GetPagerData
功能描述： 数据分页
参数说明： @tableName nvarchar(4000),     //表明
		   @columns nvarchar(4000),       //查询字段
		   @condition nvarchar(4000),     //条件
		   @key nvarchar(100),            //主键（排序）
		   @orderColumn nvarchar(4000),   //排序字段
		   @pageSize int,                 //分页数量
		   @pageIndex int,                //页码
		   @totalCount int output ,       //记录总数
		   @pageCount int output,         //总页码
		   @isAsc int                     //是否升序 1升序 0 倒序	 
编写日期： 2015/4/10
程序作者： Allen
调试记录： exec P_GetPagerData 
************************************************************/
CREATE PROCEDURE [dbo].[P_GetPagerData]
	@tableName nvarchar(4000),
	@columns nvarchar(4000),
	@condition nvarchar(4000),
	@key nvarchar(100),
	@orderColumn nvarchar(4000),
	@pageSize int,
	@pageIndex int,
	@totalCount int output ,
	@pageCount int output,
	@isAsc int
AS
declare @orderby nvarchar(20)
if(@isAsc=0)
begin
	set @orderby='desc'
end
else
begin
	set @orderby='asc'
end
declare @CommandSQL nvarchar(4000)
set @CommandSQL= 'select @totalCount=count(0) from '+@tableName+' where '+@condition
exec sp_executesql @CommandSQL,N'@totalCount int output',@totalCount output
set @pageCount=CEILING(@totalCount * 1.0/@pageSize)

if(@pageIndex=0 or @pageIndex=1)
begin 
	if @orderColumn!=''
	begin
		set	@orderColumn=@orderColumn+','
	end
	set @CommandSQL='select top '+str(@pageSize)+' '+@columns+' from '+@tableName+' where '+@condition+' order by '+@orderColumn+@key+' '+@orderby
end
else
begin
	if(@pageIndex>@pageCount)
	begin
		set @pageIndex=@pageCount
	end
	if @orderColumn!=''
	begin
		set	@orderColumn=@orderColumn+','
	end
	set @CommandSQL='select * from (select row_number() over( order by '+@orderColumn+@key+' '+@orderby+') as rowid , '+@columns+' from '+@tableName+' where '+@condition+'  ) as dt where rowid between '+str((@pageIndex-1) * @pageSize + 1)+' and '+str(@pageIndex* @pageSize)
end
exec (@CommandSQL)
