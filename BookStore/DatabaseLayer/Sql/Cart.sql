Create Table Cart
(
CartId int identity(1,1) primary key,
UserId int not null foreign key (UserId) references Users(UserId),
BookId int not null foreign key (BookId) references Book(BookId),
Book_Quantity int
);

select * from Cart

---------------------------------------------- Add to Cart ----------------------------------------------
create procedure spAddToCart
( 
  @Book_Quantity int,
  @UserId int,
  @BookId int
)
As
Begin try
	insert into cart(Book_Quantity,UserId,BookId) values (@Book_Quantity,@UserId, @BookId);
End try
Begin catch
SELECT
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;

---------------------------------------------- Get Book from Cart ----------------------------------------------
create procedure spGetAllBookFromCart
(
@UserId int
)
AS
Begin try
	select
		CartId,
		c.BookId,
		UserId,
		c.Book_Quantity,
		BookName,
		BookImg,
		AuthorName,
		Description,
		DiscountPrice,
		OriginalPrice
		from Cart c
		join Book b
		on c.BookId = b.BookId
		where UserId = @UserId;
End try
Begin catch
SELECT
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;

exec spGetAllBookFromCart '1'

---------------------------------------------- Update Cart ----------------------------------------------
create procedure spUpdateCart
(
	@BookQuantity int,
	@BookId int,
	@UserId int,
	@CartId int
)
as
begin try
update Cart set BookId=@BookId,
				UserId=@UserId,
				Book_Quantity=@BookQuantity
				where CartId=@CartId;
end try
Begin catch
SELECT
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH

---------------------------------------------- Delete Cart ----------------------------------------------
create Procedure spDeleteCart
(
@CartId int
)
As
Begin try
	Delete from Cart where CartId = @CartId;
End try
Begin catch
SELECT
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH;

---------------------------------------------- GetCartById ----------------------------------------------
alter procedure spGetCartById(
@CartId int
)
As
Begin try
select 
c.CartId,b.BookId,b.BookName,b.AuthorName,b.Description,c.Book_Quantity,b.OriginalPrice,b.DiscountPrice,b.BookImg
from Cart c INNER JOIN Book b ON c.BookId = b.BookId where CartId = @CartId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH
