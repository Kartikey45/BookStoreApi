USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[InsertBooksInBookStore]    Script Date: 20-07-2020 21:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[InsertBooksInBookStore]
@Title varchar(255),
@Description varchar(255),
@Author varchar(255),
@BooksAvailable int,
@Price decimal,
@CreatedDate datetime
as 
begin
if exists (select Title from Books where Title = @Title and BooksAvailable = 0 and IsDeleted = 'true')
begin
	update Books
	set BooksAvailable = @BooksAvailable , ModifiedDate = getdate(), IsDeleted = 'false'
	where Title = @Title and BooksAvailable = 0 and IsDeleted = 'true'
	select * from Books where Title = @Title;
end
else
begin
	insert into Books(Title, Description, Author, BooksAvailable, Price, CreatedDate,ModifiedDate,IsDeleted)
	values(@Title, @Description, @Author, @BooksAvailable, @Price, @CreatedDate, getdate(), 'false');
	select * from Books where Title = @Title;
end
end