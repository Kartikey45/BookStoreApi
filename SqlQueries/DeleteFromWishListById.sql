USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFromWishListById]    Script Date: 26-07-2020 16:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DeleteFromWishListById]
@UserId int,
@WishListId int
as
begin try
	if exists (select WishListId from WishList where WishListId = @WishListId)
	begin
		UPDATE WishList
		SET IsDeleted = 'true', DateModifiedWL = getdate()
		WHERE WishListId = @WishListId and UserId = @UserId
	end
	else
	begin
		raiserror('Invalid WishList Id',11,1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch
