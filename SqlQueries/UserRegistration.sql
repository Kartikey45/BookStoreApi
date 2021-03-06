USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UserRegistration]    Script Date: 16-07-2020 11:29:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[UserRegistration]
@FirstName varchar(255),
@LastName varchar(255),
@Email varchar(255),
@Password varchar(255),
@Address varchar(255),
@City varchar(255),
@PhoneNumber varchar(255),
@CreatedDate datetime
as
begin
if not exists (select Email from UserInformation where Email = @Email)
begin
	insert into UserInformation(FirstName,LastName,UserRole,Email,Password,Address,City,PhoneNumber, CreatedDate)
	values (@FirstName, @LastName , 'Customer' , @Email , @Password , @Address , @City , @PhoneNumber , @CreatedDate);

	select * from UserInformation where Email = @Email
end
end