IF OBJECT_ID('ReportActivations', 'P') IS NOT NULL
  DROP PROCEDURE ReportActivations
GO

CREATE PROCEDURE [dbo].[ReportActivations] 
	-- Add the parameters for the stored procedure here
	@StartDate DateTime,
	@EndDate DateTime,
	@StartRowIndex INT,
	@NumberOfRows INT,
	@SortColumn VARCHAR(50) = 'DEFAULT',
	@SortDirection VARCHAR(4) = 'ASC',
	@BrokerId INT = NULL,
	@AgentId INT = NULL,
	@ClientId INT = NULL,
	@CampaignStatus VARCHAR(50) = NULL,
	@IsCardUsed  BIT = NULL,
	@TotalCount INT OUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @CardUsed AS CHAR(50);
	SET @CardUsed = CASE WHEN @IsCardUsed IS NULL THEN NULL
		WHEN @IsCardUsed = 1 THEN 'Y'
		WHEN @IsCardUsed = 0 THEN 'N' END;
	
	
    -- Insert statements for procedure here

	SET @TotalCount = (SELECT COUNT(*) FROM ReportActivationsVW
	WHERE ClientId = COALESCE(@ClientId, ClientId)
	AND BrokerId = COALESCE(@BrokerId, BrokerId)
	AND CardUsed = COALESCE(@CardUsed , CardUsed)
	AND Status = COALESCE(@CampaignStatus, Status));

	SELECT CardNumber, ActivationDate, Member, Denomination, CardType, CardUsed, Campaign, Status
	FROM ReportActivationsVW
	WHERE ClientId = COALESCE(@ClientId, ClientId)
	AND BrokerId = COALESCE(@BrokerId, BrokerId)
	AND CardUsed = COALESCE(@CardUsed, CardUsed)
	AND Status = COALESCE(@CampaignStatus, Status)
	AND (ActivationDate BETWEEN dbo.MinDate(@StartDate) AND dbo.MaxDate(@EndDate))
	ORDER BY 
		CASE WHEN @SortDirection = 'ASC' THEN
			CASE WHEN @SortColumn = 'CardNumber' THEN 1
				WHEN @SortColumn = 'Member' THEN 3
				WHEN @SortColumn = 'Denomination' THEN 4
				WHEN @SortColumn = 'CardType' THEN 5
				WHEN @SortColumn = 'CardUsed' THEN 6
				WHEN @SortColumn = 'Campaign' THEN 7
				WHEN @SortColumn = 'Status' THEN 8
				ELSE 2
			END
		END,
		CASE WHEN @SortDirection = 'DESC' THEN
			CASE WHEN @SortColumn = 'CardNumber' THEN 1 
				WHEN @SortColumn = 'Member' THEN 3
				WHEN @SortColumn = 'Denomination' THEN 4
				WHEN @SortColumn = 'CardType' THEN 5
				WHEN @SortColumn = 'CardUsed' THEN 6 
				WHEN @SortColumn = 'Campaign' THEN 7
				WHEN @SortColumn = 'Status' THEN 8
				ELSE 2
			END
		END DESC
	OFFSET @StartRowIndex ROWS
	FETCH NEXT @NumberOfRows ROWS ONLY;
END
