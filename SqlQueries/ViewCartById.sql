create procedure ViewCartById
@UserId int
as
begin
	select * from Books inner join Cart on Books.BookId = Cart.BookId
	where UserId = @UserId
end