create table Cart
(
	CartId int IDENTITY(1,1) PRIMARY KEY,
	UserId int,
	BookId int
);

select * from Cart;