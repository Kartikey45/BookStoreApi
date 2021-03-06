USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[CancellOrder]    Script Date: 26-07-2020 16:25:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[CancellOrder]
@UserId int,
@OrderId int
as 
begin try
	if exists (select OrderId from OrderDetails where OrderId = @OrderId and OrderPlaced = 'true')
	begin
		update OrderDetails
		set OrderPlaced = 'false' , ModifiedDate = getdate()
		where OrderId = @OrderId and UserId = @UserId
	end
	else 
	begin 
		raiserror('Order Id not found', 11 , 1)	
	end
end try
begin catch
			DECLARE @Message varchar(MAX) = ERROR_MESSAGE()
			raiserror(@Message,11,1)
end catch
