CREATE FUNCTION [dbo].[PaddString] 
(
	@ItemToPad varchar(255),
	@TotalLength tinyint,
	@Padding char
)
RETURNS varchar(255)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result varchar(255)

	-- Add the T-SQL statements to compute the return value here
	if len(@ItemToPad) > @TotalLength OR len(@ItemToPad) = @TotalLength
	begin
		set @Result = @ItemToPad
	end
	else
	begin
		set @Result = left( replicate( @Padding, @TotalLength ), @TotalLength - len( @ItemToPad ) ) + cast( @ItemToPad as varchar(255) )
	end
	-- Return the result of the function
	RETURN @Result

END