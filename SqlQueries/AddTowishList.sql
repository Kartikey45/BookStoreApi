USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[AddTowishList]    Script Date: 26-07-2020 16:20:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[AddTowishList]
@UserId int,
@BookId int,
@Quantity int
as
begin try
		if exists (select BookId from Books where BookId = @BookId and BooksAvailable > 0)
		begin 
			if exists (select BookId , UserId from WishList where BookId = @BookId and UserId = @UserId and IsMoved = 'false' and IsDeleted = 'false')
			begin
				declare @count int
				select @count = @Quantity + Quantity  from WishList where BookId = @BookId and UserId = @UserId
				if exists (select BookId from Books where BookId = @BookId and BooksAvailable >= @count)
					begin
						update WishList
						set Quantity = @count , DateModifiedWL = getdate()
						where BookId = @BookId and UserId = @UserId and IsMoved = 'false' and IsDeleted = 'false'
						select * from Books inner join WishList on Books.BookId = WishList.BookId
						where UserId = @UserId 
					end
				else
					begin
						raiserror('This much quantity of this book is not available right now',11,1)
					end
			end
			else
			begin
				if exists (select BookId from Books where BookId = @BookId and BooksAvailable >= @Quantity)
					begin
						insert into WishList(UserId,BookId, Quantity, IsMoved, IsDeleted, DateCreatedWL, DateModifiedWL)
						values(@UserId,@BookId,@Quantity,'false','false',getdate(), getdate());
						select * from Books inner join WishList on Books.BookId = WishList.BookId
						where UserId = @UserId
					end
				else
					begin
						raiserror('This much quantity of this book is not available right now',11,1)
					end
			end
		end
		else
		begin
			raiserror('Book is Out of Stock',11,1)
		end
end try
begin catch
		DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
		raiserror(@Message,11,1)
end catch


