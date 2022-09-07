---------------------------------------------- Add User ----------------------------------------------
Create table Users(
UserId int identity(1,1) primary key,
FullName varchar(50),
Email varchar(50) unique,
Password varchar(20),
Mobile Bigint
)
insert into Users(FullName,Email,Password,Mobile) values('Vinay Biradar','vinay@gmail.com','Vinay@123','8784657437')

select* from Users

Create procedure spAddUser(
@FullName varchar(50),   
@Email varchar(50),  
@Password varchar(20),  
@Mobile BigInt  
)  
As
Begin try
insert into Users(FullName,Email,Password,Mobile) values(@FullName,@Email,@Password,@Mobile)
end try 
Begin catch
SELECT
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH

---------------------------------------------- Login User ----------------------------------------------
Create procedure spLoginUser(
@Email varchar(50),
@Password varchar(20)
)
As
Begin try
select * from Users where Email=@Email and Password = @Password
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spLoginUser 'vinay@gmail.com' ,'Vinay@123'

---------------------------------------------- Forgot password ----------------------------------------------
Create procedure spForgetPasswordUser(
@Email varchar(50)
)
As
Begin try
select * from Users where Email=@Email 
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spForgetPasswordUser 'vinay@gmail.com'