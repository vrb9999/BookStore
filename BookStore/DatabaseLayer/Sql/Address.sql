---------------------------------------------- Address Type ----------------------------------------------
create table AddressType
(
	TypeId int identity(1,1) primary key,
	AddressType varchar(max) not null
);

insert into AddressType values('Home'),('Office'),('Others');

select * from AddressType

---------------------------------------------- Address table ----------------------------------------------

create table AddressTable
(
AddressId int identity(1,1) primary key,
Address varchar(max) not null,
City varchar(100) not null,
State varchar(100) not null,
TypeId int not null foreign key (TypeId) references AddressType(TypeId),
UserId int not null foreign key (UserId) references Users(UserId)
)

select * from AddressTable

---------------------------------------------- Add Address ----------------------------------------------
create procedure spAddAddress
(
	@Address varchar(max),
    @City varchar(100),
    @State varchar(100),
    @TypeId int,
	@UserId int
)
As
Begin try
	Insert into AddressTable (Address, City, State, TypeId, UserId)	values(@Address, @City, @State, @TypeId, @UserId);
End try
Begin catch
SELECT
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH

Exec spAddAddress 'Swastik Nagar','Bagalkot','Karnataka',2,1


---------------------------------------------- Get All Address ----------------------------------------------
create procedure spGetAllAddress(
@UserId int
)
As
Begin try
select 
a.AddressId,a.TypeId,a.Address,a.City,a.State,u.UserId,u.FullName,u.Mobile
from AddressTable a INNER JOIN Users u ON a.UserId = u.UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spGetAllAddress '1'


---------------------------------------------- Get Address By Id ----------------------------------------------
create procedure spGetAddressById(
@AddressId int,
@UserId int
)
As
Begin try
select 
a.AddressId,a.TypeId,a.Address,a.City,a.State,u.UserId,u.FullName,u.Mobile
from AddressTable a INNER JOIN Users u ON a.UserId = u.UserId where AddressId=@AddressId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spGetAddressById '2','1'


---------------------------------------------- Update Address ----------------------------------------------
create procedure spUpdateAddress(
@AddressId int, 
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int,
@UserId int
)
As
Begin try
update AddressTable set TypeId=@TypeId,Address=@Address,City=@City,State=@State where UserId=@UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


---------------------------------------------- Delete Address ----------------------------------------------
create procedure spDeleteAddress(
@AddressId int,
@UserId int
)
As
Begin try
delete from AddressTable where AddressId = @AddressId AND UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH