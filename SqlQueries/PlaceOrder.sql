USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[PlaceOrder]    Script Date: 26-07-2020 16:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[PlaceOrder]
@UserId int,
@CartId int
as
begin try
	if exists (select CartId, UserId from Cart where CartId = @CartId and UserId = @UserId and IsUsed = 'false' and IsDeleted = 'false')
	begin
		declare @Quantity int,
				@BookId int,
				@Price decimal

		select @Quantity = Quantity, @BookId = BookId from Cart where CartId = @CartId
		select @Price = Price from Books where BookId = @BookId

		if exists (select BooksAvailable from Books where BooksAvailable >= @Quantity and BookId = @BookId)
		begin
			insert into OrderDetails(CartId, UserId, OrderPlaced, TotalPrice, CreatedDate, ModifiedDate)
			values(@CartId, @UserId, 'true', @Quantity * @Price, getdate(), getdate())

			select OrderId, Cart.CartId, UserInformation.UserId, Books.BookId, Books.Title, Books.Author, Books.BookImage , UserInformation.Address, UserInformation.City,
					UserInformation.PhoneNumber, OrderDetails.TotalPrice, OrderDetails.OrderPlaced ,OrderDetails.CreatedDate, OrderDetails.ModifiedDate  
					from Cart inner join OrderDetails on Cart.CartId = OrderDetails.CartId inner join Books on Cart.CartId = @CartId and Books.BookId = @BookId 
					inner join UserInformation on OrderDetails.UserId = @UserId and UserInformation.UserId = @UserId

			update Cart 
			set IsUsed = 'true'
			where CartId = @CartId

			update Books
			set BooksAvailable = BooksAvailable - @Quantity
			where BookId = @BookId
		end
		else
		begin
			raiserror('Book Is out of stock', 11, 1)
		end

	end
	else
	begin
		raiserror('CartId Not Found', 11, 1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch
