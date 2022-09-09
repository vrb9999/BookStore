Create Table Cart
(
CartId int identity(1,1) primary key,
UserId int not null foreign key (UserId) references Users(UserId),
BookId int not null foreign key (BookId) references Book(BookId),
Book_Quantity int
);

select * from Cart

---------------------------------------------- Add to Cart ----------------------------------------------
Create procedure spAddToCart
( 
  @Book_Quantity int,
  @UserId int,
  @BookId int
)
As
Begin
	insert into cart(Book_Quantity,UserId,BookId)
	values ( @Book_Quantity,@UserId, @BookId);
End;

---------------------------------------------- Get Book from Cart ----------------------------------------------
alter procedure spGetAllBookFromCart
(
@UserId int
)
AS
Begin
	select
		CartId,
		c.BookId,
		UserId,
		c.Book_Quantity,
		BookName,
		BookImg,
		AuthorName,
		DiscountPrice,
		OriginalPrice
		from Cart c
		join Book b
		on c.BookId = b.BookId
		where UserId = @UserId;
End;

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
begin
update Cart set BookId=@BookId,
				UserId=@UserId,
				Book_Quantity=@BookQuantity
				where CartId=@CartId;
end

---------------------------------------------- Delete Cart ----------------------------------------------
Create Procedure spDeleteCart
(
@CartId int
)
As
Begin
	Delete from Cart where CartId = @CartId;
End;