USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[AddToCart]    Script Date: 21-07-2020 11:47:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter procedure [dbo].[WishListToCart]
@UserId int,
@WishListId int
as
begin
	if exists (select WishListId , UserId from WishList where WishListId = @WishListId and UserId = @UserId and IsMoved = 'false' and IsDeleted = 'false')
	begin
		declare @Quantity int ,
				@BookId int
				
		select @Quantity = Quantity , @BookId = BookId from WishList where WishListId = @WishListId
		begin
			exec AddToCart @UserId, @BookId, @Quantity
			update WishList 
			set IsMoved = 'True'
			where WishListId = @WishListId
		end
	end
	else
	begin
		raiserror('WishListId not Found',16, 1)
	end
end
