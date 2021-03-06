USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFromCartById]    Script Date: 26-07-2020 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DeleteFromCartById]
@UserId int,
@CartId int
as
begin try
	if exists (select CartId from Cart where CartId = @CartId)
	begin
		UPDATE Cart
		SET IsDeleted = 'true', DateModified = getdate()
		WHERE CartId = @CartId and UserId = @UserId
	end
	else
	begin
		raiserror('Invalid Cart Id',11,1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch
