USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[BookSearch]    Script Date: 15-07-2020 00:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[BookSearch]
@search varchar(255)
as
begin
	select * from Books
	where Title  like '%' +@search+ '%' or Description  like '%' +@search+ '%' or Author like '%' +@search+ '%'
end