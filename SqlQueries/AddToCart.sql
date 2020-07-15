create procedure AddToCart
@UserId int,
@BookId int
as
begin
	insert into Cart(UserId,BookId)
	values(@UserId,@BookId);

	select * from Books inner join Cart on Books.BookId = Cart.BookId
	where UserId = @UserId
end 

