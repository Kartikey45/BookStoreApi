
create procedure [dbo].[InsertBooksInBookStore]
@Title varchar(255),
@Description varchar(255),
@Author varchar(255),
@BooksAvailable int,
@Price decimal,
@CreatedDate datetime
as 
begin
if not exists (select Title from Books where Title = @Title)
begin
	insert into Books(Title, Description, Author, BooksAvailable, Price, CreatedDate)
	values(@Title, @Description, @Author, @BooksAvailable, @Price, @CreatedDate);
end
end
