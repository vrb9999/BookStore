Create table Book(
BookId int identity(1,1) primary key,
BookName varchar(max) not null,
AuthorName varchar(max) not null,
Description varchar(max) not null,
Quantity int not null,
OriginalPrice int not null,
DiscountPrice int not null,
AvgRating int,
RatingCount int,
BookImg varchar(max)
)

select * from Book

---------------------------------------------- Add Book ----------------------------------------------
Create procedure spAddBook(
@BookName varchar(max),
@AuthorName varchar(max),
@Description varchar(max),
@Quantity int,
@OriginalPrice int,
@DiscountPrice int,
@AvgRating int,
@RatingCount int,
@BookImg varchar(max) 
)  
As
Begin try
insert into Book(BookName,AuthorName,Description,Quantity,OriginalPrice,DiscountPrice,AvgRating,RatingCount,BookImg) values(@BookName,@AuthorName,@Description,@Quantity,@OriginalPrice,@DiscountPrice,@AvgRating,@RatingCount,@BookImg)
end try 
Begin catch
SELECT
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH

---------------------------------------------- Get All Books ----------------------------------------------
create procedure spGetAllBooks
As
Begin try
select * from Book
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spGetAllBooks

---------------------------------------------- Get Book by Id ----------------------------------------------
create procedure spGetBookById(
@BookId int
)
As
Begin try
select * from Book where BookId=@BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

---------------------------------------------- Update Book ----------------------------------------------
create procedure spUpdateBook(
@BookId int,
@BookName varchar(max),
@AuthorName varchar(max),
@Description varchar(max),
@Quantity int,
@OriginalPrice int,
@DiscountPrice int,
@AvgRating int,
@RatingCount int,
@BookImg varchar(max) 
)  
As
Begin try
update Book set BookName=@BookName,AuthorName=@AuthorName,Description=@Description,Quantity=@Quantity,OriginalPrice=@OriginalPrice,DiscountPrice=@DiscountPrice,AvgRating=@AvgRating,RatingCount=@RatingCount,BookImg=@BookImg where BookId=@BookId
select * from Book where BookId=@BookId
end try 
Begin catch
SELECT
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH

---------------------------------------------- Delete Book ----------------------------------------------
create procedure spDeleteBook(
@BookId int
)
As
Begin try
delete from Book where BookId=@BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH