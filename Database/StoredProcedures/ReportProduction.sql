IF OBJECT_ID('ReportProduction', 'P') IS NOT NULL
  DROP PROCEDURE ReportProduction
GO

CREATE PROCEDURE [dbo].[ReportProduction] 
	-- Add the parameters for the stored procedure here
	@BookingStartDate DateTime = NULL,
	@BookingEndDate DateTime = NULL,
	@CheckOutStartDate DateTime = NULL,
	@CheckOutEndDate DateTime = NULL,
	@StartRowIndex INT,
	@NumberOfRows INT,
	@SortColumn VARCHAR(50) = 'DEFAULT',
	@SortDirection VARCHAR(4) = 'ASC',
	@BrokerId INT = NULL,
	@AgentId INT = NULL,
	@ClientId INT = NULL,
	@Search VARCHAR(100) = NULL,
	@TotalCount INT OUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @Search IS NULL
	BEGIN
	SET @TotalCount = (SELECT COUNT(*) 
	FROM dbo.ReportProductionVW
	WHERE BrokerId = COALESCE(@BrokerId, BrokerId)
	AND ClientId = COALESCE(@ClientId, ClientId)
	AND (BookingDate BETWEEN dbo.MinDate((COALESCE(@BookingStartDate, BookingDate))) AND dbo.MaxDate((COALESCE(@BookingEndDate, BookingDate))))
	AND (CheckOutDate  BETWEEN dbo.MinDate((COALESCE(@CheckOutStartDate, CheckOutDate))) AND dbo.MaxDate((COALESCE(@CheckOutEndDate, CheckOutDate)))));

	SELECT ConfirmationNumber, CardNumber, Member, Guest, BookingDate, CheckInDate, CheckOutDate, Canceled, InternetPrice, YouPayPrice, MemberSavings, PointBalance as PointsBalance
	FROM dbo.ReportProductionVW
	WHERE BrokerId = COALESCE(@BrokerId, BrokerId)
	AND ClientId = COALESCE(@ClientId, ClientId)
	AND (BookingDate BETWEEN dbo.MinDate((COALESCE(@BookingStartDate, BookingDate))) AND dbo.MaxDate((COALESCE(@BookingEndDate, BookingDate))))
	AND (CheckOutDate  BETWEEN dbo.MinDate((COALESCE(@CheckOutStartDate, CheckOutDate))) AND dbo.MaxDate((COALESCE(@CheckOutEndDate, CheckOutDate))))
	ORDER BY
		CASE WHEN @SortDirection = 'ASC' THEN
			CASE WHEN @SortColumn = 'ConfirmationNumber' THEN 1
				WHEN @SortColumn = 'CardNumber' THEN 2
				WHEN @SortColumn = 'Member' THEN 3
				WHEN @SortColumn = 'Guest' THEN 4
				WHEN @SortColumn = 'CheckInDate' THEN 6
				WHEN @SortColumn = 'CheckOutDate' THEN 7
				WHEN @SortColumn = 'Canceled' THEN 8
				WHEN @SortColumn = 'InternetPrice' THEN 9
				WHEN @SortColumn = 'YouPayPrice' THEN 10
				WHEN @SortColumn = 'MemberSavings' THEN 11
				WHEN @SortColumn = 'PointsBalance' THEN 12
				ELSE 5
			END
		END,
		CASE WHEN @SortDirection = 'DESC' THEN
			CASE WHEN @SortColumn = 'ConfirmationNumber' THEN 1
				WHEN @SortColumn = 'CardNumber' THEN 2
				WHEN @SortColumn = 'Member' THEN 3
				WHEN @SortColumn = 'Guest' THEN 4
				WHEN @SortColumn = 'CheckInDate' THEN 6
				WHEN @SortColumn = 'CheckOutDate' THEN 7
				WHEN @SortColumn = 'Canceled' THEN 8
				WHEN @SortColumn = 'InternetPrice' THEN 9
				WHEN @SortColumn = 'YouPayPrice' THEN 10
				WHEN @SortColumn = 'MemberSavings' THEN 11
				WHEN @SortColumn = 'PointsBalance' THEN 12
				ELSE 5
			END 
		END DESC
		OFFSET @StartRowIndex ROWS
		FETCH NEXT @NumberOfRows ROWS ONLY;
	END
	ELSE
	BEGIN
		SET @TotalCount = (SELECT COUNT(*) 
	FROM dbo.ReportProductionVW
	WHERE BrokerId = COALESCE(@BrokerId, BrokerId)
	AND ClientId = COALESCE(@ClientId, ClientId)
	AND (BookingDate BETWEEN dbo.MinDate((COALESCE(@BookingStartDate, BookingDate))) AND dbo.MaxDate((COALESCE(@BookingEndDate, BookingDate))))
	AND (CheckOutDate  BETWEEN dbo.MinDate((COALESCE(@CheckOutStartDate, CheckOutDate))) AND dbo.MaxDate((COALESCE(@CheckOutEndDate, CheckOutDate))))
	AND (ConfirmationNumber LIKE '%' + @Search + '%' OR CardNumber LIKE '%' + @Search + '%' OR Member LIKE '%' + @Search + '%' OR Guest LIKE '%' + @Search + '%' OR BookingDate LIKE '%' + @Search + '%' OR 
		CheckInDate LIKE '%' + @Search + '%' OR CheckOutDate LIKE '%' + @Search + '%' OR Canceled LIKE '%' + @Search + '%' OR InternetPrice LIKE '%' + @Search + '%' OR YouPayPrice LIKE '%' + @Search + '%' OR 
		MemberSavings LIKE '%' + @Search + '%' OR PointBalance LIKE '%' + @Search + '%')
	);

	SELECT ConfirmationNumber, CardNumber, Member, Guest, BookingDate, CheckInDate, CheckOutDate, Canceled, InternetPrice, YouPayPrice, MemberSavings, PointBalance as PointsBalance
	FROM dbo.ReportProductionVW
	WHERE BrokerId = COALESCE(@BrokerId, BrokerId)
	AND ClientId = COALESCE(@ClientId, ClientId)
	AND (BookingDate BETWEEN dbo.MinDate((COALESCE(@BookingStartDate, BookingDate))) AND dbo.MaxDate((COALESCE(@BookingEndDate, BookingDate))))
	AND (CheckOutDate  BETWEEN dbo.MinDate((COALESCE(@CheckOutStartDate, CheckOutDate))) AND dbo.MaxDate((COALESCE(@CheckOutEndDate, CheckOutDate))))
	AND (ConfirmationNumber LIKE '%' + @Search + '%' OR CardNumber LIKE '%' + @Search + '%' OR Member LIKE '%' + @Search + '%' OR Guest LIKE '%' + @Search + '%' OR BookingDate LIKE '%' + @Search + '%' OR 
		CheckInDate LIKE '%' + @Search + '%' OR CheckOutDate LIKE '%' + @Search + '%' OR Canceled LIKE '%' + @Search + '%' OR InternetPrice LIKE '%' + @Search + '%' OR YouPayPrice LIKE '%' + @Search + '%' OR 
		MemberSavings LIKE '%' + @Search + '%' OR PointBalance LIKE '%' + @Search + '%')
	ORDER BY
		CASE WHEN @SortDirection = 'ASC' THEN
			CASE WHEN @SortColumn = 'ConfirmationNumber' THEN 1
				WHEN @SortColumn = 'CardNumber' THEN 2
				WHEN @SortColumn = 'Member' THEN 3
				WHEN @SortColumn = 'Guest' THEN 4
				WHEN @SortColumn = 'CheckInDate' THEN 6
				WHEN @SortColumn = 'CheckOutDate' THEN 7
				WHEN @SortColumn = 'Canceled' THEN 8
				WHEN @SortColumn = 'InternetPrice' THEN 9
				WHEN @SortColumn = 'YouPayPrice' THEN 10
				WHEN @SortColumn = 'MemberSavings' THEN 11
				WHEN @SortColumn = 'PointsBalance' THEN 12
				ELSE 5
			END
		END,
		CASE WHEN @SortDirection = 'DESC' THEN
			CASE WHEN @SortColumn = 'ConfirmationNumber' THEN 1
				WHEN @SortColumn = 'CardNumber' THEN 2
				WHEN @SortColumn = 'Member' THEN 3
				WHEN @SortColumn = 'Guest' THEN 4
				WHEN @SortColumn = 'CheckInDate' THEN 6
				WHEN @SortColumn = 'CheckOutDate' THEN 7
				WHEN @SortColumn = 'Canceled' THEN 8
				WHEN @SortColumn = 'InternetPrice' THEN 9
				WHEN @SortColumn = 'YouPayPrice' THEN 10
				WHEN @SortColumn = 'MemberSavings' THEN 11
				WHEN @SortColumn = 'PointsBalance' THEN 12
				ELSE 5
			END 
		END DESC
		OFFSET @StartRowIndex ROWS
		FETCH NEXT @NumberOfRows ROWS ONLY;
	END;

END
