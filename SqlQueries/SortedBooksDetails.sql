USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[SortedBooksDetails]    Script Date: 23-07-2020 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SortedBooksDetails]
@SortCol varchar(255),
@SortDir varchar(255)
AS
BEGIN

    With CTE_Books as
(Select ROW_NUMBER() over
(order by
 
case when (@SortCol = 'Title' and @SortDir='Ascending')
then Title
end asc,
case when (@SortCol = 'Title' and @SortDir='Descending')
then Title
end desc,

case when (@SortCol = 'Price' and @SortDir='Ascending')
then Price
end asc,
case when (@SortCol = 'Price' and @SortDir='Descending')
then Price
end desc,

case when (@SortCol = 'Author' and @SortDir='Ascending')
then Author
end asc,
case when (@SortCol = 'Author' and @SortDir='Descending')
then Author
end desc
)
as RowNum,
COUNT(*) over() as TotalCount,
BookId,
Title,
Description,
Author,
BooksAvailable,
Price,
CreatedDate,
ModifiedDate,
IsDeleted,
BookImage
from Books
where IsDeleted = 'false'
 )
Select *
from CTE_Books
END