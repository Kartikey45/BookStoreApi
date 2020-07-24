alter procedure PlaceOrderNew
@UserId int,
@CartId int,
@Address varchar(255),
@City varchar(255),
@PinCode int
as
begin
	if exists (select CartId, UserId from Cart where CartId = @CartId and UserId = @UserId and IsUsed = 'false' and IsDeleted = 'false')
	begin
		declare @Quantity int,
				@BookId int,
				@Price decimal

		select @Quantity = Quantity, @BookId = BookId from Cart where CartId = @CartId
		select @Price = Price from Books where BookId = @BookId

		if exists (select BooksAvailable from Books where BooksAvailable >= @Quantity and BookId = @BookId)
		begin
			insert into OrderDetails(CartId, UserId, OrderPlaced, TotalPrice, CreatedDate, ModifiedDate, Address , City, PinCode)
			values(@CartId, @UserId, 'true', @Quantity * @Price, getdate(), getdate(), @Address, @City, @PinCode)

			select OrderId, Cart.CartId, Books.BookId, Books.Title, Books.Author, Books.BookImage , OrderDetails.UserId ,OrderDetails.Address, OrderDetails.City, OrderDetails.PinCode,
				    OrderDetails.TotalPrice, OrderDetails.OrderPlaced ,OrderDetails.CreatedDate, OrderDetails.ModifiedDate  
					from Cart inner join OrderDetails on Cart.CartId = OrderDetails.CartId inner join Books on Cart.CartId = @CartId and Books.BookId = @BookId
					and OrderDetails.UserId = @UserId
					

			update Cart 
			set IsUsed = 'true'
			where CartId = @CartId

			update Books
			set BooksAvailable = BooksAvailable - @Quantity
			where BookId = @BookId
		end
		else
		begin
			raiserror('Book Is out of stock', 16, 1)
		end
	end
	else
	begin
		raiserror('CartId Not Found', 16, 1)
	end
end