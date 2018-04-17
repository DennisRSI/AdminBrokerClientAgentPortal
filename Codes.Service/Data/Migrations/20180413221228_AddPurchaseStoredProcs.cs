using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codes.Service.Data.Migrations
{
    public partial class AddPurchaseStoredProcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region Sync Codes

            const string syncCodes = @"
CREATE PROCEDURE [dbo].[SyncCodes] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @MyCursor CURSOR;

DECLARE @UnusedT TABLE
(
	CampaignId INT,
	Code NVARCHAR(1000),
	CodeType NVARCHAR(50),
	CodeRangeId INT,
	CreationDate DATETIME,
	CreatorIP NVARCHAR(50),
	DeactivationDate DateTime NULL,
	DeactivationReason NVARCHAR(1000) NULL,
	IsActive BIT,
	BrokerId INT,
	OldCodeId INT
);

DECLARE @UsedT TABLE
(
	Address1 NVARCHAR(255) NULL,
	Address2 NVARCHAR(255) NULL,
	CampaignId INT,
	City NVARCHAR(100),
	Code NVARCHAR(1000),
	CodeRangeId INT,
	Cost DECIMAL(18,2),	
	Country NVARCHAR(100),
	CreationDate DATETIME,
	CreatorIP NVARCHAR(50),
	DeactivationDate DateTime NULL,
	DeactivationReason NVARCHAR(1000) NULL,
	Email NVARCHAR(100),
	EmailVerifiedDate DATETIME,
	EndDate DATETIME NULL,
	FirstName NVARCHAR(255),
	IsActive BIT,
	LastName NVARCHAR(255),
	MiddleName NVARCHAR(255) NULL,
	NumberOfUses INT,
	CodeCreatedDate DATETIME,
	PackageId INT,
	Paid DECIMAL(18,2),
	Password NVARCHAR(100),
	Phone1 NVARCHAR(50),
	Phone2 NVARCHAR(50),
	Points REAL,
	PostalCode NVARCHAR(50) NULL,
	RSIId INT,
	StartDate DATETIME NULL,
	State NVARCHAR(100),
	Username NVARCHAR(100),
	VerifyEmail BIT,
	CodeType NVARCHAR(50),
	OldCodeActivityId INT,
	BrokerId INT,
	OldCodeId INT
);

DECLARE @PendingT TABLE
(
	Address1 NVARCHAR(255) NULL,
	Address2 NVARCHAR(255) NULL,
	CampaignId INT,
	City NVARCHAR(100),
	Code NVARCHAR(1000),
	CodeRangeId INT,
	Cost DECIMAL(18,2),	
	Country NVARCHAR(100),
	CreationDate DATETIME,
	CreatorIP NVARCHAR(50),
	DeactivationDate DateTime NULL,
	DeactivationReason NVARCHAR(1000) NULL,
	Email NVARCHAR(100),
	EndDate DATETIME NULL,
	FirstName NVARCHAR(255),
	IsActive BIT,
	LastName NVARCHAR(255),
	MiddleName NVARCHAR(255) NULL,
	NumberOfUses INT,
	CodeCreatedDate DATETIME,
	PackageId INT,
	Paid DECIMAL(18,2),
	Password NVARCHAR(100),
	Phone1 NVARCHAR(50),
	Phone2 NVARCHAR(50),
	Points REAL,
	PostalCode NVARCHAR(50) NULL,
	StartDate DATETIME NULL,
	State NVARCHAR(100),
	Username NVARCHAR(100),
	VerifyEmail BIT,
	CodeType NVARCHAR(50),
	OldCodeActivityId INT,
	BrokerId INT,
	OldCodeId INT
);

DECLARE @CampaignId INT,
	@ClientId INT,
	@Code NVARCHAR(1000),
	@CodeType NVARCHAR(50),
	@CodeRangeId INT,
	@CreationDate DATETIME,
	@CreatorIP NVARCHAR(50),
	@DeactivationDate DATETIME,
	@DeactivationReason NVARCHAR(1000),
	@IsActive BIT,
	@LeftDiget AS INT,
	@OrigionalPadding INT,
	@NumberPart VARCHAR(256),
	@Pre NVARCHAR(100),
	@Post NVARCHAR(100),
	@BrokerId INT,
	@OldCodeId INT,
	@Address1 NVARCHAR(255),
	@Address2 NVARCHAR(255),
	@City NVARCHAR(100),
	@Country NVARCHAR(100),
	@Email NVARCHAR(100),
	@EmailVerifiedDate DATETIME,
	@EndDate DATETIME,
	@FirstName NVARCHAR(255),
	@LastName NVARCHAR(255),
	@MiddleName NVARCHAR(255),
	@NumberOfUses INT,
	@CodeCreationDate DATETIME,
	@PackageId INT,
	@Paid MONEY,
	@Password NVARCHAR(100),
	@Phone1 NVARCHAR(50),
	@Phone2 NVARCHAR(50),
	@Points REAL,
	@PostalCode NVARCHAR(50),
	@RSIId INT,
	@StartDate DATETIME,
	@State NVARCHAR(100),
	@Username NVARCHAR(100),
	@VerifyEmail BIT,
	@OldCodeActivityId INT,
	@ChargeAmount MONEY;
	
BEGIN
	SET @MyCursor = CURSOR FOR
	SELECT DISTINCT x.Code, x.CreationDate, x.CreatorIP, x.DeactivationDate, '', x.IsActive, x.IssuerReference, x.CodeId
	FROM Codes x 
	LEFT OUTER JOIN UsedCodes u on x.CodeId = u.OldCodeId
	LEFT OUTER JOIN UnusedCodes uu on x.CodeId = uu.OldCodeId
	LEFT OUTER JOIN PendingCodes p on x.CodeId = p.OldCodeId
	WHERE x.Issuer = 530 AND u.UsedCodeId IS NULL AND uu.UnusedCodeId IS NULL AND p.PendingCodeId IS NULL AND x.CodeId IN 
	(SELECT DISTINCT c.CodeId 
		FROM Codes c
		LEFT OUTER JOIN CodeActivities a on c.CodeId = a.CodeId
		WHERE c.Issuer = 530 AND (a.CodeId IS NULL OR 
			(SELECT COUNT(a1.ActivationCode) 
				FROM CodeActivities a1 
				WHERE a1.Issuer = 530 and a1.CodeId = c.CodeId  
				GROUP BY a1.CodeId) < NumberOfUses)
	);

	OPEN @MyCursor
	FETCH NEXT FROM @MyCursor
	INTO @Code, @CreationDate, @CreatorIP, @DeactivationDate, @DeactivationReason, @IsActive, @BrokerId, @OldCodeId
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @ChargeAmount = 0;
		set @Paid = 0;
		IF @BrokerId >= 3
		BEGIN
			SET @BrokerId = 4;
		END;
		SET @CodeType ='Virtual';
		SET @ClientId = NULL;

		-- SET @NumberPart = CAST(dbo.udf_GetNumericAsString(@Code) AS VARCHAR(256));
		SET @NumberPart = CAST(@Code AS VARCHAR(256));

		SET @LeftDiget = LEFT(@NumberPart, 1);
		IF @LeftDiget = 0
		BEGIN
			SET @OrigionalPadding = LEN(@NumberPart);
		END
		ELSE
		BEGIN
			SET @OrigionalPadding = 0;
		END;

		IF @LeftDiget = 0
		BEGIN
			SET @OrigionalPadding = LEN(@NumberPart);
		END
		ELSE
		BEGIN
			SET @OrigionalPadding = 0;
		END;

		SET @Pre = SUBSTRING(@Code, 1, CHARINDEX(@NumberPart, @Code) - 1);
		SET @Post = REPLACE(@Code, @Pre + @NumberPart, ''); 
		
		--Get Code Range Id------------------------------------------------------------------------------------------------------------------------------------
		SELECT @CodeRangeId = CodeRangeId
		FROM CodeRanges
		WHERE RSIOrganizationId = 530 AND PreAlphaCharacters = @Pre AND PostAlphaCharacters = @Post AND (@NumberPart BETWEEN StartNumber AND EndNumber);
		-------------------------------------------------------------------------------------------------------------------------------------------------------
		--Get Campaign Id--------------------------------------------------------------------------------------------------------------------------------------
		SELECT @CampaignId = CampaignId
		FROM CampaignCodeRanges
		WHERE CodeRangeId = @CodeRangeId;
		-------------------------------------------------------------------------------------------------------------------------------------------------------
		--Get Client Id----------------------------------------------------------------------------------------------------------------------------------------
		IF @CampaignId IS NOT NULL 
		BEGIN
			SELECT @ClientId = ClientId, @CodeType = CampaignType
			FROM Campaigns WHERE CampaignId = @CampaignId;
		END;
		-------------------------------------------------------------------------------------------------------------------------------------------------------

		INSERT INTO @UnusedT
				(CampaignId, Code, CodeType, CodeRangeId, CreationDate, CreatorIP, DeactivationDate, DeactivationReason, IsActive, BrokerId, OldCodeId )
			Values
				(@CampaignId, @Code, @CodeType, @CodeRangeId, @CreationDate, @CreatorIP, @DeactivationDate, @DeactivationReason, @IsActive, @BrokerId, @OldCodeId);
		
		FETCH NEXT FROM @MyCursor
		INTO @Code, @CreationDate, @CreatorIP, @DeactivationDate, @DeactivationReason, @IsActive, @BrokerId, @OldCodeId
	END;
	CLOSE @MyCursor ;
	DEALLOCATE @MyCursor;
	
	INSERT INTO UnusedCodes
		(CampaignId, Code, CodeRangeId, CreationDate, CreatorIP, DeactivationDate, DeactivationReason, IsActive, CodeType, BrokerId, OldCodeId)
		(SELECT CampaignId, Code, CodeRangeId, CreationDate, CreatorIP, DeactivationDate, DeactivationReason, IsActive, CodeType, BrokerId, OldCodeId
		FROM @UnusedT);

	SET @MyCursor = CURSOR FOR
	SELECT a.Address1, a.address2, a.city, a.ActivationCode, c.ChargeAmount, a.CountryCode, a.CreationDate as EmailSentDate, a.CreatorIP, a.DeactivationDate, a.Email, a.EmailVerifiedDate, c.EndDate
		, a.FirstName, a.IsActive, a.LastName, a.MiddleName, c.NumberOfUses, c.CreationDate, c.PackageId, 0, a.Password, a.Phone1, a.Phone2, c.HotelPoints, a.PostalCode, a.RSIId, c.StartDate
		, a.StateCode, a.Username, c.VerifyEmail, a.CodeActivityId, c.IssuerReference, a.DeactivationDate, c.CodeId
	FROM CodeActivities a
	INNER JOIN Codes c on a.CodeId = c.CodeId
	LEFT OUTER JOIN UsedCodes u on a.CodeActivityId = u.OldCodeActivityId
	LEFT OUTER JOIN PendingCodes p on a.CodeActivityId = p.OldCodeActivityId
	WHERE a.Issuer = 530 AND c.Issuer = 530 AND u.OldCodeActivityId IS NULL AND p.OldCodeActivityId IS NULL
	OPEN @MyCursor
	FETCH NEXT FROM @MyCursor
	INTO @Address1, @Address2, @City, @Code, @ChargeAmount, @Country, @CreationDate, @CreatorIP, @DeactivationDate, @Email, @EmailVerifiedDate, @EndDate, @FirstName, @IsActive, @LastName, @MiddleName, @NumberOfUses
		, @CodeCreationDate, @PackageId, @Paid, @Password, @Phone1, @Phone2, @Points, @PostalCode, @RSIId, @StartDate, @State, @Username, @VerifyEmail, @OldCodeActivityId, @BrokerId, @DeactivationDate, @OldCodeId;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @ChargeAmount = 0;
		set @Paid = 0;
		IF @BrokerId >= 3
		BEGIN
			SET @BrokerId = 4;
		END;
		SET @CodeType ='Virtual';
		SET @ClientId = NULL;
		SET @NumberPart = CAST(dbo.udf_GetNumericAsString(@Code) AS VARCHAR(256));
		SET @LeftDiget = LEFT(@NumberPart, 1);
		IF @LeftDiget = 0
		BEGIN
			SET @OrigionalPadding = LEN(@NumberPart);
		END
		ELSE
		BEGIN
			SET @OrigionalPadding = 0;
		END;

		IF @LeftDiget = 0
		BEGIN
			SET @OrigionalPadding = LEN(@NumberPart);
		END
		ELSE
		BEGIN
			SET @OrigionalPadding = 0;
		END;

		SET @Pre = SUBSTRING(@Code, 1, CHARINDEX(@NumberPart, @Code) - 1);
		SET @Post = REPLACE(@Code, @Pre + @NumberPart, ''); 
		
		--Get Code Range Id------------------------------------------------------------------------------------------------------------------------------------
		SELECT @CodeRangeId = CodeRangeId
		FROM CodeRanges
		WHERE RSIOrganizationId = 530 AND PreAlphaCharacters = @Pre AND PostAlphaCharacters = @Post AND (@NumberPart BETWEEN StartNumber AND EndNumber);
		-------------------------------------------------------------------------------------------------------------------------------------------------------
		--Get Campaign Id--------------------------------------------------------------------------------------------------------------------------------------
		SELECT @CampaignId = CampaignId
		FROM CampaignCodeRanges
		WHERE CodeRangeId = @CodeRangeId;
		-------------------------------------------------------------------------------------------------------------------------------------------------------
		--Get Client Id----------------------------------------------------------------------------------------------------------------------------------------
		IF @CampaignId IS NOT NULL 
		BEGIN
			SELECT @ClientId = ClientId, @CodeType = CampaignType
			FROM Campaigns WHERE CampaignId = @CampaignId;
		END;
		-------------------------------------------------------------------------------------------------------------------------------------------------------

		IF @RSIId IS NULL OR @RSIId < 1
		BEGIN
			INSERT INTO @PendingT
				(Address1, Address2, CampaignId, City, Code, CodeRangeId, Cost, Country, CreationDate, CreatorIP, DeactivationDate, Email, EndDate, FirstName, IsActive, LastName, MiddleName
					, NumberOfUses, CodeCreatedDate, PackageId, Paid, Password, Phone1, Phone2, Points, PostalCode, StartDate, State, Username, VerifyEmail, CodeType, OldCodeActivityId, BrokerId, DeactivationReason, OldCodeId)
			VALUES
				(@Address1, @Address2, @CampaignId, @City, @Code, @CodeRangeId, @ChargeAmount, @Country, @CreationDate, @CreatorIP, @DeactivationDate, @Email, @EndDate, @FirstName, @IsActive, @LastName, @MiddleName
					, @NumberOfUses, @CodeCreationDate, @PackageId, @Paid, @Password, @Phone1, @Phone2, @Points, @PostalCode, @StartDate, @State, @Username, @VerifyEmail, @CodeType, @OldCodeActivityId, @BrokerId, @DeactivationDate, @OldCodeId);
		END
		ELSE
		BEGIN
			INSERT INTO @UsedT
				(Address1, Address2, CampaignId, City, Code, CodeRangeId, Cost, Country, CreationDate, CreatorIP, DeactivationDate, Email, EmailVerifiedDate, EndDate, FirstName, IsActive, LastName, MiddleName
					, NumberOfUses, CodeCreatedDate, PackageId, Paid, Password, Phone1, Phone2, Points, PostalCode, RSIId, StartDate, State, Username, VerifyEmail, CodeType, OldCodeActivityId, BrokerId, DeactivationReason, OldCodeId)
			VALUES
				(@Address1, @Address2, @CampaignId, @City, @Code, @CodeRangeId, @ChargeAmount, @Country, @CreationDate, @CreatorIP, @DeactivationDate, @Email, @EmailVerifiedDate, @EndDate, @FirstName, @IsActive, @LastName, @MiddleName
					, @NumberOfUses, @CodeCreationDate, @PackageId, @Paid, @Password, @Phone1, @Phone2, @Points, @PostalCode, @RSIId, @StartDate, @State, @Username, @VerifyEmail, @CodeType, @OldCodeActivityId, @BrokerId, @DeactivationDate, @OldCodeId);
		END;

		FETCH NEXT FROM @MyCursor
		INTO @Address1, @Address2, @City, @Code, @ChargeAmount, @Country, @CreationDate, @CreatorIP, @DeactivationDate, @Email, @EmailVerifiedDate, @EndDate, @FirstName, @IsActive, @LastName, @MiddleName, @NumberOfUses
			, @CodeCreationDate, @PackageId, @Paid, @Password, @Phone1, @Phone2, @Points, @PostalCode, @RSIId, @StartDate, @State, @Username, @VerifyEmail, @OldCodeActivityId, @BrokerId, @DeactivationDate, @OldCodeId;
	END;
	CLOSE @MyCursor ;
	DEALLOCATE @MyCursor;
	--SELECT * FROM @UsedT;
	INSERT INTO [dbo].[UsedCodes]
		([Address1] ,[Address2], [CampaignId],[City],[Code],[CodeRangeId],[Cost],[Country],[EmailSentDate]
			   ,[CreatorIP]
			   ,[DeactivationDate]
			   ,[Email]
			   ,[CreationDate]
			   ,[EndDate]
			   ,[FirstName]
			   ,[IsActive]
			   ,[LastName]
			   ,[MiddleName]
			   ,[NumberOfUses]
			   ,[CodeCreatedDate]
			   ,[PackageId]
			   ,[Paid]
			   ,[Password]
			   ,[Phone1]
			   ,[Phone2]
			   ,[Points]
			   ,[PostalCode]
			   ,[RSIId]
			   ,[StartDate]
			   ,[State]
			   ,[Username]
			   ,[VerifyEmail]
			   ,[CodeType]
			   ,[OldCodeActivityId]
			   ,[BrokerId]
			   ,[DeactivationReason]
			   ,[OldCodeId])
			   (SELECT Address1, Address2, CampaignId, City, Code, CodeRangeId, Cost, Country, CreationDate, CreatorIP, DeactivationDate, Email, EmailVerifiedDate, EndDate, FirstName, IsActive, LastName, MiddleName
				, NumberofUses, CodeCreatedDate, PackageId, Paid, Password, Phone1, Phone2, Points, PostalCode, RSIId, StartDate, State, Username, VerifyEmail, CodeType, OldCodeActivityId, BrokerId, DeactivationDate
				, OldCodeId
				FROM @UsedT);

	INSERT INTO [dbo].[PendingCodes]
			   ([Address1]
			   ,[Address2]
			   ,[CampaignId]
			   ,[City]
			   ,[Code]
			   ,[CodeRangeId]
			   ,[Cost]
			   ,[Country]
			   ,[CreationDate]
			   ,[CreatorIP]
			   ,[DeactivationDate]
			   ,[Email]
			   ,[EndDate]
			   ,[FirstName]
			   ,[IsActive]
			   ,[LastName]
			   ,[MiddleName]
			   ,[NumberOfUses]
			   ,[CodeCreatedDate]
			   ,[PackageId]
			   ,[Paid]
			   ,[Password]
			   ,[Phone1]
			   ,[Phone2]
			   ,[Points]
			   ,[PostalCode]
			   ,[StartDate]
			   ,[State]
			   ,[Username]
			   ,[VerifyEmail]
			   ,[CodeType]
			   ,[OldCodeActivityId]
			   ,[BrokerId]
			   ,[DeactivationReason]
			   ,[OldCodeId])
			   (SELECT Address1, Address2, CampaignId, City, Code, CodeRangeId, Cost, Country, CreationDate, CreatorIP, DeactivationDate, Email, EndDate, FirstName, IsActive, LastName, MiddleName
				, NumberofUses, CodeCreatedDate, PackageId, Paid, Password, Phone1, Phone2, Points, PostalCode, StartDate, State, Username, VerifyEmail, CodeType, OldCodeActivityId, BrokerId, DeactivationDate
				, OldCodeId
				FROM @PendingT);
END;
END
            ";
            #endregion

            #region GenerateCodes

            const string generateCodes = @"
CREATE PROCEDURE [dbo].[GenerateCodes] 

	-- Add the parameters for the stored procedure here

	@LettersStart VARCHAR(2),

	@LettersEnd VARCHAR(2),

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

	@Message VARCHAR(1000) = '' OUTPUT

	

AS

BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from

	-- interfering with SELECT statements.

	SET NOCOUNT ON;

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

	DECLARE @TempMessage AS VARCHAR(MAX);

	DECLARE @CodeRangeId AS INT,

		@StartRange VARCHAR(500),

		@EndRange VARCHAR(500);

	SET @Message = '';



	-- SET @StartRange = @LettersStart + dbo.PaddString(CAST(@StartNumber AS VARCHAR(100)), @PaddingNumber, @PaddingValue) + @LettersEnd;
	-- SET @EndRange = @LettersStart + dbo.PaddString(CAST(@EndNumber AS VARCHAR(100)), @PaddingNumber, @PaddingValue) + @LettersEnd;

	SET @StartRange = @LettersStart + FORMAT(@StartNumber, '0000#') + @LettersEnd;
	SET @EndRange   = @LettersStart + FORMAT(@EndNumber, '0000#') + @LettersEnd;


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



		-- INSERT INTO [dbo].[CampaignCodeRanges]

        --    ([CampaignId]

        --    ,[CodeRangeId]

        --    ,[CreationDate]

         --   ,[CreatorIP]

         --   ,[DeactivationDate]

         --   ,[IsActive])

		-- VALUES

         --   (@CampaignId

         --   ,@CodeRangeId

         --   ,GETDATE()

         --   ,'127.0.0.1'

         --   ,NULL

         --   ,1);



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

           ,[VerifyEmail])

		SELECT * FROM @TempTable;



		EXEC dbo.SyncCodes;

		SET @Message = 'Success';

        -- Entity Framework requires data to be returned
        select *
        from Brokers
        where BrokerId = @BrokerId

	END;

	END;
            ";
            #endregion

            #region GenerateTempTableCodes

            const string generateTempTableCodes = @"

CREATE PROCEDURE [dbo].[GenerateTempTableCodes] 

	-- Add the parameters for the stored procedure here

	@LettersStart VARCHAR(2),

	@LettersEnd VARCHAR(2),

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

           (
            -- @LettersStart + [dbo].[PaddString] (CAST(@Start as VARCHAR(100)),@PaddingNumber,@PaddingValue) + @LettersEnd
            @LettersStart + FORMAT(@Start, '0000#') + @LettersEnd
           ,@ChargeAmount

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

            ";
            #endregion

            migrationBuilder.Sql(syncCodes);
            migrationBuilder.Sql(generateTempTableCodes);
            migrationBuilder.Sql(generateCodes);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[GenerateCodes]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[GenerateTempTableCodes]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[SyncCodes]");
        }
    }
}
