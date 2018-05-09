USE [RSICodeGenerators]
GO

IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CreateCodes') AND type IN ( N'P', N'PC' ) ) 
    drop procedure [dbo].[CreateCodes]
go

CREATE PROCEDURE [dbo].[CreateCodes]
	@BrokerId INT,
    @PurchaseId INT,
    @NumberOfCodes INT,
    @NumberOfUses INT,
    @CodeType VARCHAR(8),
	@CodePrefix VARCHAR(5),
	@CodeSuffix VARCHAR(5),
	@IncrementBy INT
AS
BEGIN
	
	DECLARE @start INT = 0;
	DECLARE @end INT;

    -- Need clarification on how these are used
	DECLARE @StartDate DATETIME = NULL
	DECLARE @EndDate DATETIME = NULL
	DECLARE @ChargeAmount MONEY = 0
	DECLARE @Issuer VARCHAR(100) = '?'
	DECLARE @PackageId INT = 0
	DECLARE @HotelPoints INT = 1000
	DECLARE @CondoRewards INT = 0
	DECLARE @VerifyEmail BIT = 1

    -- This query is slow: Add columns with separate code components so this can perform better?
    select @start = MAX(CAST(REPLACE(REPLACE(Code, @CodePrefix, ''), @CodeSuffix, '') as INT))
    from Codes
    where Code like @CodePrefix + '%' + @CodeSuffix

	if @start is null
	begin
	    select @start = 1
	end

    select @start = @start + @IncrementBy
    select @end = (@NumberOfCodes - 1) * @IncrementBy + @start

    WHILE @start <= @end
    BEGIN

        INSERT INTO Codes
            (
                 [Code]
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
                ,[PurchaseId]
            )
        VALUES
            (
                 @CodePrefix + FORMAT(@start, '0000#') + @CodeSuffix
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
                ,@VerifyEmail
                ,@PurchaseId
            );

        SET @start = @start + @IncrementBy;

    END;

    -- Required for Entity Framework
    select *
    from Brokers
    where BrokerId = @BrokerId

end