IF OBJECT_ID('SyncCodes', 'P') IS NOT NULL
  DROP PROCEDURE SyncCodes
GO

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
		if @CodeRangeId IS NOT NULL 
		BEGIN
		INSERT INTO @UnusedT
				(CampaignId, Code, CodeType, CodeRangeId, CreationDate, CreatorIP, DeactivationDate, DeactivationReason, IsActive, BrokerId, OldCodeId )
			Values
				(@CampaignId, @Code, @CodeType, @CodeRangeId, @CreationDate, @CreatorIP, @DeactivationDate, @DeactivationReason, @IsActive, @BrokerId, @OldCodeId);
		END;
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
