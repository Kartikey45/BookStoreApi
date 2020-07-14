USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[SortedBooksDetails]    Script Date: 15-07-2020 03:20:10 ******/
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

case when (@SortCol = 'Description' and @SortDir='Ascending')
then Description
end asc,
case when (@SortCol = 'Description' and @SortDir='Descending')
then Description
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
ModifiedDate
from Books
 )
Select *
from CTE_Books
END