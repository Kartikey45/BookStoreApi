create procedure DeleteFromCartById
@UserId int,
@CartId int
as
begin
	delete from Cart
	where UserId = @UserId and CartId = @CartId
end