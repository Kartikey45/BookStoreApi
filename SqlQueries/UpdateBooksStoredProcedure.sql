USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdatBookDetails]    Script Date: 15-07-2020 20:50:22 ******/
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
begin
	update Books
	set Title = @Title,
		Description = @Description,
		Author = @Author,
		BooksAvailable = @BooksAvailable,
		Price = @Price,
		ModifiedDate = @ModifiedDate
	where BookId = @BookId
	
end
	select * from Books where BookId = @BookId
end
