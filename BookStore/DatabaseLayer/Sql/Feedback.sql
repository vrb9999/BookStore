create table FeedBack(
FeedbackId int Identity(1,1)primary key,
Comments varchar(max),
AvgRating int,
BookId int not null Foreign Key references Book(BookId),
UserId int not null Foreign key references Users(UserId)
)

select * from Feedback

---------------------------------------------- Add Feedback ----------------------------------------------
create  procedure spAddFeedback
(
@AvgRating int,
@Comments varchar(max),
@BookId int,
@UserId int
)
as
Declare @AverageRating int;
BEGIN TRY
	if(not exists(select * from Feedback where BookId=@BookId and UserId=@UserId))
	BEGIN
		insert into Feedback(AvgRating,Comments,BookId,UserId) values(@AvgRating,@Comments,@BookId,@UserId)
		set @AverageRating = (select AVG(AvgRating) from Book where BookId = @BookId);
		Update Book set AvgRating = @AverageRating, RatingCount = (RatingCount+1) where BookId = @BookId;
	END
END TRY
BEGIN CATCH
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

Exec spAddFeedback 4.0,'Good',2,1

---------------------------------------------- Get Feedback ----------------------------------------------
create proc spGetFeedback(
@BookId int
)
As
BEGIN TRY
	select f.FeedbackId,f.Comments,f.BookId,f.AvgRating,f.UserId,u.FullName
	from Feedback f inner join Users u on f.UserId = u.UserId where BookId = @BookId
END TRY
BEGIN CATCH
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spGetFeedback '2'