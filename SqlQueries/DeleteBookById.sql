USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteBookById]    Script Date: 20-07-2020 02:27:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DeleteBookById]
@BookId int
as
begin
	if exists (select BookId from Books where BookId = @BookId)
	begin
		update Books
		set IsDeleted = 1 ,ModifiedDate = getdate(), BooksAvailable = 0
		where BookId = @BookId
	end
	else
	begin
		raiserror('BookId not Found',16,1);
	end
end
