USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteBookById]    Script Date: 26-07-2020 16:27:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DeleteBookById]
@BookId int
as
begin try
	if exists (select BookId from Books where BookId = @BookId)
	begin
		update Books
		set IsDeleted = 1 ,ModifiedDate = getdate(), BooksAvailable = 0
		where BookId = @BookId
	end
	else
	begin
		raiserror('BookId not Found',11,1);
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch

