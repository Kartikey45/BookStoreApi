USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[ViewOrders]    Script Date: 25-07-2020 11:03:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ViewOrders]
@UserId int
as 
begin
	if exists (select UserId from OrderDetails where UserId = @UserId)
	begin
	select OrderId, Cart.CartId, UserInformation.UserId, Books.BookId, Books.Title, Books.Author, Books.BookImage,
				UserInformation.PhoneNumber, OrderDetails.TotalPrice, OrderDetails.OrderPlaced ,OrderDetails.CreatedDate, OrderDetails.ModifiedDate,
				OrderDetails.Address, OrderDetails.City, OrderDetails.PinCode
				from OrderDetails 
				join UserInformation on UserInformation.UserId = OrderDetails.UserId
				join Cart on OrderDetails.CartId = Cart.CartId 
				join Books on Cart.BookId = Books.BookId 
				where OrderDetails.UserId = 14
	end
	else
	begin
		raiserror('Order list is empty',16,1)
	end
end

