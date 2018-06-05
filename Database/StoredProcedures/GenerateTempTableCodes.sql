IF OBJECT_ID('GenerateTempTableCodes', 'P') IS NOT NULL
  DROP PROCEDURE GenerateTempTableCodes
GO

CREATE PROCEDURE [dbo].[GenerateTempTableCodes] 
	-- Add the parameters for the stored procedure here
	@LettersStart VARCHAR(50),
	@LettersEnd VARCHAR(50),
	@IncrementBy INT = 1,
	@BrokerId INT,
	@ClientId INT,
	@CampaignId INT,
	@CodeType VARCHAR(50) = 'Virtual',
	@Issuer VARCHAR(100), 
	@PackageId INT = 0,
	@PaddingNumber tinyint = 5,
	@PaddingValue CHAR = '0',
	@StartNumber INT = 0, 
	@EndNumber INT = 50,
	@HotelPoints INT = 1000,
	@CondoRewards INT = 0,
	@NumberOfUses INT = 1,
	@ChargeAmount MONEY = 0,
	@StartDate DATETIME = NULL,
	@EndDate DATETIME = NULL,
	@VerifyEmail BIT = 1,
	@Message VARCHAR(MAX) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @MyCursor CURSOR,
		@Code as VARCHAR(500);
	SET @Message = '';
	DECLARE @CodeCount INT;
	SET @CodeCount = 0;

	DECLARE @TempTable as Table
		(Code NVARCHAR(500),
		ChargeAmount DECIMAL(18,2),
		CreationDate DATETIME,
		EndDate DATETIME NULL,
		IsActive BIT,
		Issuer NVARCHAR(50),
		NumberOfUses INT,
		StartDate DATETIME NULL,
		IssuerReference NVARCHAR(100) NULL,
		CondoRewards INT,
		CreatorIP NVARCHAR(50),
		HotelPoints INT,
		PackageId INT,
		VerifyEmail BIT);

	DECLARE @AlreadyInCodes as Table
		(Code NVARCHAR(500));
	
	IF @EndNumber < @StartNumber
	BEGIN
		SET @Message = 'Error: End number has to be greater than start number';
	END
	ELSE
		BEGIN
		DECLARE @start INT;
		DECLARE @end INT;

		SELECT @start = @StartNumber, @end = @EndNumber;

		WHILE @start <= @end
		BEGIN
			INSERT INTO @TempTable
           ([Code]
           ,[ChargeAmount]
           ,[CreationDate]
           ,[EndDate]
           ,[IsActive]
           ,[Issuer]
           ,[NumberOfUses]
           ,[StartDate]
           ,[IssuerReference]
           ,[CondoRewards]
           ,[CreatorIP]
           ,[HotelPoints]
           ,[PackageId]
           ,[VerifyEmail])
     VALUES
           (@LettersStart + [dbo].[PaddString] (CAST(@Start as VARCHAR(100)),@PaddingNumber,@PaddingValue) + @LettersEnd
           , @ChargeAmount
           ,GETDATE()
           ,@EndDate
           ,1
           ,@Issuer
           ,@NumberOfUses
           ,@StartDate
           ,@BrokerId
           ,@CondoRewards
           ,'127.0.0.1'
           ,@HotelPoints
           ,@PackageId
           ,@VerifyEmail);
		SET @start = @start + @IncrementBy;
		END;
	
	INSERT INTO @AlreadyInCodes
	SELECT t.Code FROM @TempTable t INNER JOIN Codes c on t.Code = c.Code and c.Issuer = @Issuer;

	SET @CodeCount = (SELECT COUNT(*) FROM @AlreadyInCodes);

	IF @CodeCount > 0
	BEGIN
		SET @MyCursor = CURSOR FOR
		SELECT Code FROm @AlreadyInCodes
		OPEN @MyCursor
		FETCH NEXT FROM @MyCursor
		INTO @Code
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF LEN(@Message) > 0
			BEGIN
				SET @Message = @Message + ',';
			END
			ELSE
			BEGIN
				SET @Message = 'Error: Dups -> ';
			END;

			SET @Message = @Message + @Code;

			FETCH NEXT FROM @MyCursor
			INTO @Code
		END;
		CLOSE @MyCursor ;
		DEALLOCATE @MyCursor;
		END
		ELSE
		BEGIN
			SET @Message = 'Success';
		END;
	END;
	SELECT * FROM @TempTable;
END
