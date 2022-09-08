Create table Admin(
AdminId int Identity(1,1)Primary key,
Email varchar(50),
Password varchar(20)
)

insert into Admin(Email,Password) values('vinay@gmail.com','Vinay@123')

select * from Admin

---------------------------------------------- Login ----------------------------------------------
create procedure spAdminLogin(
@Email varchar(50),  
@Password varchar(20)
)
As  
Begin try  
select * from Admin where Email=@Email and Password=@Password;
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH 

exec spAdminLogin 'vinay@gmail.com', 'Vinay@123'