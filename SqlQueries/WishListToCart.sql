USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[WishListToCart]    Script Date: 26-07-2020 16:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[WishListToCart]
@UserId int,
@WishListId int
as
begin try
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
		raiserror('WishListId not Found',11, 1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch

