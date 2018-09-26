IF OBJECT_ID('GenerateCodes', 'P') IS NOT NULL
  DROP PROCEDURE GenerateCodes
GO

CREATE PROCEDURE [dbo].[GenerateCodes] 
	-- Add the parameters for the stored procedure here
	@LettersStart VARCHAR(50) = '',
	@LettersEnd VARCHAR(50) = '',
	@IncrementBy INT = 3,
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
	@Message VARCHAR(1000) OUTPUT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @TempTable as Table
	(
		Code NVARCHAR(500),
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
		VerifyEmail BIT,
		CampaignId INT
	);

	DECLARE @TempMessage AS VARCHAR(MAX);
	DECLARE @CodeRangeId AS INT,
		@StartRange VARCHAR(500),
		@EndRange VARCHAR(500);
	SET @Message = '';

	IF @LettersStart IS NULL 
	BEGIN
		SET @LettersStart = '';
	END;

	IF @LettersEnd IS NULL 
	BEGIN
		SET @LettersEnd = '';
	END;

	SET @StartRange = @LettersStart + dbo.PaddString(CAST(@StartNumber AS VARCHAR(100)), @PaddingNumber, @PaddingValue) + @LettersEnd;
	SET @EndRange = @LettersStart + dbo.PaddString(CAST(@EndNumber AS VARCHAR(100)), @PaddingNumber, @PaddingValue) + @LettersEnd;

	INSERT INTO @TempTable
	EXEC [GenerateTempTableCodes]
		@LettersStart,
		@LettersEnd,
		@IncrementBy,
		@BrokerId,
		@ClientId,
		@CampaignId,
		@CodeType,
		@Issuer,
		@PackageId,
		@PaddingNumber,
		@PaddingValue,
		@StartNumber,
		@EndNumber,
		@HotelPoints,
		@CondoRewards,
		@NumberOfUses,
		@ChargeAmount,
		@StartDate,
		@EndDate,
		@VerifyEmail,
		@Message = @Message OUTPUT;

	IF @Message = 'Success'
	BEGIN
		INSERT INTO [dbo].[CodeRanges]
           ([BrokerId]
           ,[CodeType]
           ,[Cost]
           ,[CreationDate]
           ,[CreatorIP]
           ,[DeactivationDate]
           ,[EndNumber]
           ,[IsActive]
           ,[NumberOfUses]
           ,[PostAlphaCharacters]
           ,[PreAlphaCharacters]
           ,[RSIOrganizationId]
           ,[StartNumber]
           ,[IncrementByNumber]
           ,[DeactivationReason]
           ,[Padding]
           ,[Points])
		VALUES
           (@BrokerId
           ,@CodeType
           ,@ChargeAmount
           ,GETDATE()
           ,'127.0.0.1'
           ,NULL
           ,@EndNumber
           ,1
           ,@NumberOfUses
           ,@LettersEnd
           ,@LettersStart
           ,@Issuer
           ,@StartNumber
           ,@IncrementBy
           ,''
           ,@PaddingNumber
           ,@HotelPoints);

		SET @CodeRangeId = SCOPE_IDENTITY();
		--PRINT 'CodeRangeId: ' + CAST(@CodeRangeId AS VARCHAR(500)) + '  CampaignId: ' + CAST(@CampaignId as VARCHAR(500));
		INSERT INTO [dbo].[CampaignCodeRanges]
           ([CampaignId]
           ,[CodeRangeId]
           ,[CreationDate]
           ,[CreatorIP]
           ,[DeactivationDate]
           ,[IsActive])
		VALUES
           (@CampaignId
           ,@CodeRangeId
           ,GETDATE()
           ,'127.0.0.1'
           ,NULL
           ,1);

		INSERT INTO [dbo].[Codes]
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
           ,[VerifyEmail]
		   ,[CampaignId])
		SELECT * FROM @TempTable;

		EXEC dbo.SyncCodes;
		SET @Message = 'Success';
	END;

	-- Required for Entity Framework
    select top 1 *
    from Codes

	END;