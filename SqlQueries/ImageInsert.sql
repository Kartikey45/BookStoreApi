create Procedure ImageInsert
@BookImage varchar(255),
@BookId int
as
begin
	If exists (select BookId from Books where BookId = @BookId)
	begin
		update Books
		set BookImage = @BookImage, ModifiedDate = getdate()
		where BookId = @BookId

		select * from Books where BookId = @BookId
	end
	else
	begin
		raiserror('BookId not found',16,1)
	end
end
