create table WishList
(
	WishListId int IDENTITY(1,1) PRIMARY KEY,
	UserId int,
	BookId int,
	Quantity int,
	IsMoved bit,
	IsDeleted bit,
	DateCreatedWL datetime,
	DateModifiedWL datetime,
	FOREIGN KEY (UserId) REFERENCES UserInformation(UserId),
	FOREIGN KEY (BookId) REFERENCES Books(BookId)
)