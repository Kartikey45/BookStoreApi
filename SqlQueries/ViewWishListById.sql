USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[ViewWishListById]    Script Date: 26-07-2020 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ViewWishListById]
@UserId int
as 
begin try
	if exists (select UserId from WishList where UserId = @UserId)
	begin
		select WishListId, Books.BookId, Title, Author, Price,Quantity, IsMoved, WishList.IsDeleted , DateCreatedWL, DateModifiedWL, Books.BookImage
		from Books inner join WishList on Books.BookId = WishList.BookId 
		where UserId = @UserId
	end
	else
	begin 
		raiserror('Wish List Is Empty',11,1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch
