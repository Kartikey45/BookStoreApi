USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[ViewCartById]    Script Date: 26-07-2020 16:39:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ViewCartById]
@UserId int
as
begin try
	if exists (select UserId from Cart where UserId = @UserId)
	begin
		select CartId, Books.BookId, Title, Author, Price , Quantity,IsUsed, Cart.IsDeleted, DateCreated, DateModified, Books.BookImage
		from Books inner join Cart on Books.BookId = Cart.BookId 
		where UserId = @UserId
	end
	else
	begin
		raiserror('Cart Is Empty',11,1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch
