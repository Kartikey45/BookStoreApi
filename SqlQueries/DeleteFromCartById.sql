USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFromCartById]    Script Date: 20-07-2020 00:10:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DeleteFromCartById]
@UserId int,
@CartId int
as
begin
	if exists (select CartId from Cart where CartId = @CartId)
	begin
		UPDATE Cart
		SET IsDeleted = 1, DateModified = getdate()
		WHERE CartId = @CartId and UserId = @UserId
	end
	else
	begin
		raiserror('Invalid Cart Id',16,1)
	end
end