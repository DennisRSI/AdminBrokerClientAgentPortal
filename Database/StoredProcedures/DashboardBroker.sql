IF OBJECT_ID('DashboardBroker', 'P') IS NOT NULL
  DROP PROCEDURE DashboardBroker
GO

CREATE PROCEDURE [dbo].[DashboardBroker] 
	-- Add the parameters for the stored procedure here
	@BrokerId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Tbl TABLE(RSIId INT, CardType VARCHAR(50), IsVerified BIT);
	DECLARE @PhysicalCodes INT;
	DECLARE @VirtualCodes INT;
	DECLARE @PhysicalCodesActivated INT;
	DECLARE @VirtualCodesActivated INT;
	DECLARE @PhysicalCodeWaitingForEmailVerification INT;
	DECLARE @VirtualCodesWaitingForEmailVerification INT;
	DECLARE @MemberSavings MONEY;
	DECLARE @CommissionsPaid MONEY;
	DECLARE @CommissionsOwed MONEY;
	DECLARE @CommissionsTotal MONEY;

	SET @MemberSavings = 0;
	SET @CommissionsPaid = 0;
	SET @CommissionsOwed = 0;
	SET @CommissionsTotal = 0;

	EXEC SyncCodes;

	SET @PhysicalCodes = (SELECT COUNT(*) FROM UnusedCodes U WHERE U.BrokerId = @BrokerId AND U.CodeType = 'Physical');
	SET @VirtualCodes = (SELECT COUNT(*) FROM UnusedCodes U WHERE U.BrokerId = @BrokerId AND U.CodeType = 'Virtual');

	
	INSERT INTO @Tbl
	SELECT A.RSIId,U.CodeType, CASE WHEN A.EmailVerifiedDate IS NULL THEN 0 ELSE 1 END
	FROM CodeActivities A INNER JOIN UnusedCodes U on A.CodeId = U.OldCodeId 
	WHERE U.BrokerId = @BrokerId AND A.RSIId > 0;

	SET @PhysicalCodesActivated = (SELECT COUNT(*) FROM CodeActivations A INNER JOIN @Tbl T on A.RSIId = T.RSIId WHERE T.IsVerified = 1 AND T.CardType = 'Physical');
	SET @VirtualCodesActivated = (SELECT COUNT(*) FROM CodeActivations A INNER JOIN @Tbl T on A.RSIId = T.RSIId WHERE T.IsVerified = 1 AND T.CardType = 'Virtual');

	SET @PhysicalCodeWaitingForEmailVerification = (SELECT COUNT(*) FROM CodeActivations A INNER JOIN @Tbl T on A.RSIId = T.RSIId WHERE T.IsVerified = 0 AND T.CardType = 'Physical');
	SET @VirtualCodesWaitingForEmailVerification = (SELECT COUNT(*) FROM CodeActivations A INNER JOIN @Tbl T on A.RSIId = T.RSIId WHERE T.IsVerified = 0 AND T.CardType = 'Virtual');

	SET @MemberSavings = (SELECT SUM(B.PointsUsed) FROM RSIBookings.dbo.Bookings B INNER JOIN @Tbl T on B.RSIId = T.RSIId);
	SET @CommissionsTotal = (SELECT SUM(B.ClubCommissionDue) FROM RSIBookings.dbo.Bookings B INNER JOIN @Tbl T on B.RSIId = T.RSIId);
	--SET @CommissionsPaid = (SELECT SUM(B.) FROM RSIBookings.dbo.Bookings B INNER JOIN @Tbl T on B.RSIId = T.RSIId);

	SET @CommissionsOwed = @CommissionsTotal - @CommissionsPaid;

	SELECT @PhysicalCodes AS PhysicalTotal, @VirtualCodes AS VirtualTotal, @PhysicalCodesActivated AS PhysicalActivated, @VirtualCodesActivated AS VirtualActivated
		, @PhysicalCodeWaitingForEmailVerification AS PhysicalWaiting, @VirtualCodesWaitingForEmailVerification AS VirtualWaiting, @MemberSavings AS MemberSavings, @CommissionsTotal AS CommissionsTotal
		, @CommissionsOwed AS CommissionsOwed, @CommissionsPaid AS CommissionsPaid;
END