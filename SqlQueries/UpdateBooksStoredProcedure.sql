USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdatBookDetails]    Script Date: 23-07-2020 15:24:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[UpdatBookDetails]
@BookId int,
@Title varchar(255),
@Description varchar(255),
@Author varchar(255),
@BooksAvailable int,
@Price decimal,
@ModifiedDate datetime
as
begin
if exists ( select BookId from Books where BookId = @BookId and IsDeleted = 'true')
begin
	raiserror('This bookId has already deleted, you cannot update it',16,1);
end
else if exists (select Title , BookId from Books where Title = @Title and BookId = @BookId)
begin
	update Books
	set Title = @Title,
		Description = @Description,
		Author = @Author,
		BooksAvailable = @BooksAvailable,
		Price = @Price,
		ModifiedDate = @ModifiedDate,
		IsDeleted = 'false'
	where BookId = @BookId

	select * from Books where BookId = @BookId
end
else if exists (select Title from Books where Title = @Title)
begin
	raiserror('This Book has already registered in book store',16,1)
end
else
begin
	update Books
	set Title = @Title,
		Description = @Description,
		Author = @Author,
		BooksAvailable = @BooksAvailable,
		Price = @Price,
		ModifiedDate = @ModifiedDate,
		IsDeleted = 'false'
	where BookId = @BookId

	select * from Books where BookId = @BookId
end
end
