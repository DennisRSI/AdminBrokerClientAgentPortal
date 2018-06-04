CREATE FUNCTION [dbo].[udf_GetNumericAsString] 
(
	-- Add the parameters for the function here
	@strAlphaNumeric VARCHAR(256)
)
RETURNS VARCHAR(256)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result VARCHAR(256);

	-- Add the T-SQL statements to compute the return value here
	DECLARE @intAlpha INT;
	SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric);
	BEGIN
		WHILE @intAlpha > 0
		BEGIN
			SET @strAlphaNumeric = STUFF(@strAlphaNumeric, @intAlpha, 1, '' );
			SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric );
		END;
	END;
	RETURN ISNULL(@strAlphaNumeric,0);

END
