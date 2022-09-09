create table Cart(
CartId int Identity(1,1)Primary key,
BookId int FOREIGN KEY REFERENCES Book(BookId),
UserId int FOREIGN KEY REFERENCES Users(UserId),
Quantity int
)

select * from Cart

---------------------------------------------- Add to Cart ----------------------------------------------
Create procedure spAddToCart(
@BookId int,
@UserId int,  
@Quantity int
)  
As  
Begin try
insert into Cart (BookId,UserId,Quantity) values (@BookId,@UserId,@Quantity)
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH

---------------------------------------------- Get all cart ----------------------------------------------
create procedure spGetAllCart(
@UserId int
)  
As  
Begin try  
select Cart.CartId,Cart.BookId,Cart.Quantity,Cart.UserId,
Book.Description,Book.BookName,Book.AuthorName,Book.OriginalPrice,Book.DiscountPrice,Book.BookImg
from Cart inner join Book on Cart.BookId=Book.BookId
where Cart.UserId=@UserId

end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH

exec spGetAllCart '1'