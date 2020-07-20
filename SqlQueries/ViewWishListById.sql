USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[ViewWishListById]    Script Date: 20-07-2020 19:50:34 ******/
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
		select WishListId, Books.BookId, Title, Author, Price,Quantity, IsMoved, WishList.IsDeleted , DateCreatedWL, DateModifiedWL
		from Books inner join WishList on Books.BookId = WishList.BookId 
		where UserId = @UserId
	end
	else
	begin 
		raiserror('Wish List Is Empty',16,1)
	end
end