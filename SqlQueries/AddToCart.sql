USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[AddToCart]    Script Date: 20-07-2020 20:10:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[AddToCart]
@UserId int,
@BookId int,
@Quantity int
as
begin
	
			if exists (select BookId from Books where BookId = @BookId and BooksAvailable > 0)
			begin
				if exists (select BookId , UserId from Cart where BookId = @BookId and UserId = @UserId and IsUsed = 'false' and IsDeleted = 'false')
				begin
					if exists (select BookId from Books where BookId = @BookId and BooksAvailable >= @Quantity)
					begin
						update cart
						set Quantity = @Quantity , DateModified = getdate()
						where BookId = @BookId and UserId = @UserId and IsUsed = 'false' and IsDeleted = 'false'
						select * from Books inner join Cart on Books.BookId = Cart.BookId
						where UserId = @UserId 
					end
					else
					begin
						raiserror('This much quantity of this book is not available right now',16,1)
					end
				end
				else
				begin
					if exists (select BookId from Books where BookId = @BookId and BooksAvailable >= @Quantity)
					begin
						insert into Cart(UserId,BookId, IsUsed, IsDeleted, Quantity, DateCreated, DateModified)
						values(@UserId,@BookId,'false','false',@Quantity,getdate(), getdate());
						select * from Books inner join Cart on Books.BookId = Cart.BookId
						where UserId = @UserId
					end
					else
					begin
						raiserror('This much quantity of this book is not available right now',16,1)
					end
				end
			end 
			else
			begin
				raiserror('Book is Out of Stock',16,1)
			end
	
end
