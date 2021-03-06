USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[ImageInsert]    Script Date: 26-07-2020 16:32:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[ImageInsert]
@BookImage varchar(255),
@BookId int
as
begin try
	If exists (select BookId from Books where BookId = @BookId)
	begin
		update Books
		set BookImage = @BookImage, ModifiedDate = getdate()
		where BookId = @BookId

		select * from Books where BookId = @BookId
	end
	else
	begin
		raiserror('BookId not found',11,1)
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch

