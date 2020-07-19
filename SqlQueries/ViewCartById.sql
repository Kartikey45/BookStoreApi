USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[ViewCartById]    Script Date: 20-07-2020 00:53:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ViewCartById]
@UserId int
as
begin
	if exists (select UserId from Cart where UserId = @UserId)
	begin
		select * from Books inner join Cart on Books.BookId = Cart.BookId and Cart.IsDeleted = 0
		where UserId = @UserId
	end
	else
	begin
		raiserror('Cart Is Empty',16,1)
	end
end