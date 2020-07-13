create procedure DeleteBookById
@BookId int
as
begin
	delete from Books
	where BookId = @BookId
end