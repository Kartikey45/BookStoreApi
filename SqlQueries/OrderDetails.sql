create table OrderDetails
(
	OrderId int IDENTITY(1,1) PRIMARY KEY,
	CartId int,
	UserId int,
	OrderPlaced bit,
	TotalPrice decimal,
	CreatedDate datetime,
	ModifiedDate datetime
);

select * from OrderDetails