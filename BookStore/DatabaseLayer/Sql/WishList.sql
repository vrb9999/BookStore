Create table WishList
(
	WishListId int identity(1,1) primary key,
	UserId int not null foreign key references Users(UserId),
	BookId int not null foreign key references Book(BookId)
);

Select * from WishList

---------------------------------------------- Add to WishList ----------------------------------------------
alter procedure spAddToWishList
(
@UserId int,
@BookId int
)
as
begin try
insert into WishList values (@UserId,@BookId);
end try
Begin catch
SELECT
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH


exec spAddToWishList 1,3

---------------------------------------------- Get Wishlist ----------------------------------------------
create procedure spGetWishList(
@UserId int
)
As
Begin try
select 
w.WishListId,b.BookId,b.BookName,b.AuthorName,b.Description,b.OriginalPrice,b.DiscountPrice,b.BookImg
from WishList w INNER JOIN Book b ON w.BookId = b.BookId where UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spGetWishList '1'

---------------------------------------------- Delete Wishlist ----------------------------------------------
create procedure spDeleteWishList(
@WishListId int,
@UserId int
)
As
Begin try
delete from WishList where WishListId = @WishListId AND UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH