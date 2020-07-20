create procedure ViewWishListById
@UserId int
as 
begin
	if exists (select UserId from WishList where UserId = @UserId)
	begin
		select * from Books inner join WishList on Books.BookId = WishList.BookId 
		where UserId = @UserId
	end
	else
	begin 
		raiserror('Wish List Is Empty',16,1)
	end
end