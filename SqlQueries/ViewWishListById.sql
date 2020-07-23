USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[ViewWishListById]    Script Date: 23-07-2020 18:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ViewWishListById]
@UserId int
as 
begin
	if exists (select UserId from WishList where UserId = @UserId)
	begin
		select WishListId, Books.BookId, Title, Author, Price,Quantity, IsMoved, WishList.IsDeleted , DateCreatedWL, DateModifiedWL, Books.BookImage
		from Books inner join WishList on Books.BookId = WishList.BookId 
		where UserId = @UserId
	end
	else
	begin 
		raiserror('Wish List Is Empty',16,1)
	end
end