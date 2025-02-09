USE CourseWork;
GO
select * from users;

ALTER PROCEDURE LogInToSystem (
    @Login NVARCHAR(255),
    @Password_in NVARCHAR(255),
	@Success_out INT OUTPUT,
	@PersonID INT OUTPUT
)
AS
BEGIN
    DECLARE @RoleID INT = 0;
    DECLARE @Correct_Password NVARCHAR(30);
    DECLARE @BlockEndDate DATETIME;
    DECLARE @Block_count INT;
    DECLARE @SearchPersonID INT;
	SET @PersonID = 0;

    BEGIN TRY
        IF @Login IS NULL OR @Login = ''
        BEGIN
            SET @Success_out = 0;
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Users WHERE Login = @Login)
        BEGIN
            SELECT @RoleID = RoleID, @Correct_Password = Password, @SearchPersonID = UserID 
            FROM Users 
            WHERE Login = @Login;

            SELECT @Block_count = COUNT(*) FROM UserBlocks WHERE UserID = @SearchPersonID;

            IF (@Block_count > 0) 
            BEGIN
                SELECT @BlockEndDate = EndDate FROM UserBlocks WHERE UserID = @SearchPersonID;
                IF @BlockEndDate < GETDATE() 
                BEGIN
                    DELETE FROM UserBlocks WHERE UserID = @SearchPersonID;
                END
                ELSE 
                BEGIN
                    SET @Success_out = 0;
                    RETURN;
                END
            END
        END

        IF EXISTS (SELECT 1 FROM Managers WHERE Login = @Login)
        BEGIN
            SELECT @RoleID = RoleID, @Correct_Password = Password, @SearchPersonID = ManagerID FROM Managers WHERE Login = @Login; 
        END

        IF EXISTS (SELECT 1 FROM Organizers WHERE CompanyName = @Login)
        BEGIN
            SELECT @RoleID = RoleID, @Correct_Password = Password, @SearchPersonID = OrganizerID 
            FROM Organizers WHERE CompanyName = @Login;

            SELECT @Block_count = COUNT(*) FROM OrganizerBlocks WHERE OrganizerID = @SearchPersonID;
            IF (@Block_count > 0) 
            BEGIN
                SELECT @BlockEndDate = EndDate FROM OrganizerBlocks WHERE OrganizerID = @SearchPersonID;
                IF @BlockEndDate < GETDATE() 
                BEGIN
                    DELETE FROM OrganizerBlocks WHERE OrganizerID = @SearchPersonID;
                END
                ELSE 
                BEGIN
                    SET @Success_out = 0;
                    RETURN;
                END
            END
        END

        IF (@RoleID = 0) 
        BEGIN
            SET @Success_out = 0;
            RETURN;
        END;

        IF @Password_in = @Correct_Password 
        BEGIN
            SET @Success_out = @RoleID;
			SET @PersonID = @SearchPersonID;
        END
        ELSE 
        BEGIN
            SET @Success_out = 0;
        END

    END TRY
    BEGIN CATCH
        SET @Success_out = 0;
        RETURN; 
    END CATCH
END;
GO

CREATE PROCEDURE Registration
    @Login_in NVARCHAR(40),
    @Password_in NVARCHAR(30),
    @FirstName_in NVARCHAR(20),
    @LastName_in NVARCHAR(25),
    @Email_in NVARCHAR(50),
    @Phone_in NVARCHAR(30),
	@RoleSwitch INT
AS
BEGIN
    DECLARE @maxID INT;
    DECLARE @login_exists INT;
	DECLARE @phone_exists INT;
    DECLARE @email_exists INT;
	DECLARE @minID INT = 999;

    SELECT @maxID = MAX(MAX_ID) + 1
		FROM (
			SELECT ISNULL(MAX(UserID), @minID) AS MAX_ID FROM Users
			UNION ALL
			SELECT ISNULL(MAX(OrganizerID), @minID) AS MAX_ID FROM Organizers
			UNION ALL
			SELECT ISNULL(MAX(ManagerID), @minID) AS MAX_ID FROM Managers
		) AS AllIDs;

    SELECT @login_exists = (
        SELECT COUNT(*)
        FROM (
            SELECT 1 AS Match FROM Users WHERE Login = @Login_in
            UNION ALL
            SELECT 1 AS Match FROM Organizers WHERE CompanyName = @Login_in
            UNION ALL
            SELECT 1 AS Match FROM Managers WHERE Login = @Login_in
        ) AS LoginMatches
    );

    SELECT @phone_exists = (
        SELECT COUNT(*)
        FROM (
            SELECT 1 AS Match FROM Users WHERE Phone = @Phone_in
            UNION ALL
            SELECT 1 AS Match FROM Organizers WHERE Phone = @Phone_in
            UNION ALL
            SELECT 1 AS Match FROM Managers WHERE Phone = @Phone_in
        ) AS PhoneMatches
    );

    SELECT @email_exists = (
        SELECT COUNT(*)
        FROM (
            SELECT 1 AS Match FROM Users WHERE Email = @Email_in
            UNION ALL
            SELECT 1 AS Match FROM Organizers WHERE Email = @Email_in
            UNION ALL
            SELECT 1 AS Match FROM Managers WHERE Email = @Email_in
        ) AS EmailMatches
    );
		
	IF @login_exists = 0 AND @phone_exists = 0 AND @email_exists = 0
	BEGIN
		IF @RoleSwitch = 0
		BEGIN
			INSERT INTO Users (UserID, Login, Password, FirstName, LastName, Email, Phone, RoleID)
				VALUES (@maxID, @Login_in, @Password_in, @FirstName_in, @LastName_in, @Email_in, @Phone_in, 1);
		END;
		IF @RoleSwitch = 1
		BEGIN
			INSERT INTO CheckingOrganizers (OrganizerID, CompanyName, Password, FirstName, LastName, Email, Phone, RoleID)
				VALUES (@maxID, @Login_in, @Password_in, @FirstName_in, @LastName_in, @Email_in, @Phone_in, 2);
		END;
	END;
END;
GO


