create table Books
(
	BookId int IDENTITY(1,1) PRIMARY KEY,
	BookImage image,
	Title varchar(255),
	Description varchar(255),
	Author varchar(255),
	BooksAvailable int,
	Price decimal,
	CreatedDate datetime,
	ModifiedDate datetime
);

select * from Books;