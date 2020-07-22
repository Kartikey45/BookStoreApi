alter procedure ViewOrders
@UserId int
as 
begin
	if exists (select UserId from OrderDetails where UserId = @UserId)
	begin
	select OrderId, Cart.CartId, UserInformation.UserId, Books.BookId, Books.Title, Books.Author, UserInformation.Address, UserInformation.City,
				UserInformation.PhoneNumber, OrderDetails.TotalPrice, OrderDetails.OrderPlaced ,OrderDetails.CreatedDate, OrderDetails.ModifiedDate 
				from OrderDetails 
				join UserInformation on UserInformation.UserId = OrderDetails.UserId
				join Cart on OrderDetails.CartId = Cart.CartId 
				join Books on Cart.BookId = Books.BookId 
				where OrderDetails.UserId = @UserId
	end
	else
	begin
		raiserror('Order list is empty',16,1)
	end
end

