USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteBookById]    Script Date: 15-07-2020 21:47:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DeleteBookById]
@BookId int
as
begin
	delete from Books
	where BookId = @BookId
end
