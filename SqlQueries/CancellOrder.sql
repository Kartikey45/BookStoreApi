create procedure CancellOrder
@UserId int,
@OrderId int
as 
begin
	if exists (select OrderId from OrderDetails where OrderId = @OrderId and OrderPlaced = 'true')
	begin
		update OrderDetails
		set OrderPlaced = 'false' , ModifiedDate = getdate()
		where OrderId = @OrderId and UserId = @UserId
	end
	else 
	begin 
		raiserror('Order Id not found', 16 , 1)	
	end
end