USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[ViewAllBooks]    Script Date: 20-07-2020 23:07:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ViewAllBooks]
as
begin
	select * from Books where IsDeleted = 'false';
end