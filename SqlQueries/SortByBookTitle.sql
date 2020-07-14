create procedure SortByBookTitle
as
begin
	select * from Books 
	order by Title
end
