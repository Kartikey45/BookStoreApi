USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFromWishListById]    Script Date: 20-07-2020 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DeleteFromWishListById]
@UserId int,
@WishListId int
as
begin
	if exists (select WishListId from WishList where WishListId = @WishListId)
	begin
		UPDATE WishList
		SET IsDeleted = 'true', DateModifiedWL = getdate()
		WHERE WishListId = @WishListId and UserId = @UserId
	end
	else
	begin
		raiserror('Invalid WishList Id',16,1)
	end
end