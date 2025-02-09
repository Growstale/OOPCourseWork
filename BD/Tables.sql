use CourseWork;

CREATE TABLE Roles (
    RoleID INT PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL CHECK (RoleName IN ('User', 'Manager', 'Organizer'))
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY,
    CategoryName NVARCHAR(40) NOT NULL UNIQUE
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY, 
    Login NVARCHAR(40) NOT NULL UNIQUE,
    Password NVARCHAR(30) NOT NULL,
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    Phone NVARCHAR(30) NOT NULL UNIQUE,
    RoleID INT NOT NULL,
    CONSTRAINT fk_RoleUser FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

INSERT INTO Roles VALUES (1, 'User');
INSERT INTO Roles VALUES (2, 'Manager');

INSERT INTO Users VALUES (1, '213FDS', '123FE', '123', '43ER', 'FSDF', 'FSEFDSF', 1);
CREATE TABLE CheckingOrganizers (
    OrganizerID INT PRIMARY KEY,
    CompanyName NVARCHAR(40) NOT NULL UNIQUE,    
    Password NVARCHAR(30) NOT NULL,
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    Phone NVARCHAR(30) NOT NULL UNIQUE,
    RoleID INT NOT NULL,
    CONSTRAINT fk_RoleCheckingOrganizer FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);


CREATE TABLE Organizers (
    OrganizerID INT PRIMARY KEY,
    CompanyName NVARCHAR(40) NOT NULL UNIQUE,    
    Password NVARCHAR(30) NOT NULL,
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    Phone NVARCHAR(30) NOT NULL UNIQUE,
    RoleID INT NOT NULL,
    CONSTRAINT fk_RoleOrganizer FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

CREATE TABLE Locations (
    LocationID INT PRIMARY KEY,
    LocationName NVARCHAR(60) NOT NULL UNIQUE,
    NumberOfSectors INT NOT NULL
);

CREATE TABLE Events (
    EventID INT PRIMARY KEY,
    EventName NVARCHAR(50) NOT NULL UNIQUE,
    EventDuration TIME NOT NULL,
    CategoryID INT NOT NULL,
    OrganizerID INT NOT NULL,
    Description NVARCHAR(400),
    Cost DECIMAL(18, 2) NOT NULL,
    ImagePath NVARCHAR(255), 
	StartDate DATETIME NOT NULL,
    CONSTRAINT fk_CategoryForEvents FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    CONSTRAINT fk_OrganizerForEvents FOREIGN KEY (OrganizerID) REFERENCES Organizers(OrganizerID)
);

CREATE TABLE EventsSchedule (
    EventScheduleID INT PRIMARY KEY,
    EventDate DATETIME NOT NULL,
    EventID INT NOT NULL,
	LocationID INT NOT NULL,
	CONSTRAINT fk_LocationForEvents FOREIGN KEY (LocationID) REFERENCES Locations(LocationID),
    CONSTRAINT fk_EventsScheduleEvents FOREIGN KEY (EventID) REFERENCES Events(EventID)
);

CREATE TABLE SectorRows (
    SectorRowID INT PRIMARY KEY,
    SectorRow INT NOT NULL,
    NumberOfSeats INT NOT NULL,
    LocationID INT NOT NULL,
    CostFactor DECIMAL(18, 2) NOT NULL,
    CONSTRAINT fk_Location FOREIGN KEY (LocationID) REFERENCES Locations(LocationID)
);

CREATE TABLE Tickets (
    TicketID INT PRIMARY KEY,
    EventScheduleID INT NOT NULL,
    Status NVARCHAR(50) NOT NULL CHECK (Status IN ('On sale', 'Booked', 'Purchased')),
    Price DECIMAL(18, 2) NOT NULL,
    SectorRowID INT NOT NULL,
    PlaceInRow INT NOT NULL,
    CONSTRAINT fk_TicketsEventsScheduleID FOREIGN KEY (EventScheduleID) REFERENCES EventsSchedule(EventScheduleID),
    CONSTRAINT fk_SectorRowOfTicket FOREIGN KEY (SectorRowID) REFERENCES SectorRows(SectorRowID)
);

CREATE TABLE Managers (
    ManagerID INT PRIMARY KEY,
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    Phone NVARCHAR(30) NOT NULL UNIQUE,
    DateOfEmployment DATE NOT NULL,
    Password NVARCHAR(30) NOT NULL,
    RoleID INT NOT NULL,
    Login NVARCHAR(40) NOT NULL UNIQUE,
    CONSTRAINT fk_RoleManager FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
INSERT INTO Managers VALUES (2, '213FDS', '123FE', '123', '43ER', SYSDATETIME(), '123', 2, '123');

CREATE TABLE OrganizerBlocks (
    BlockID INT PRIMARY KEY,
    OrganizerID INT NOT NULL,
    ManagerID INT NOT NULL,
    Reason NVARCHAR(200) NOT NULL,
    EndDate DATE,
    CONSTRAINT fk_OrganizerBlocks FOREIGN KEY (OrganizerID) REFERENCES Organizers(OrganizerID),
    CONSTRAINT fk_ManagerBlocks_Organizer FOREIGN KEY (ManagerID) REFERENCES Managers(ManagerID)
);

CREATE TABLE OrganizerQuestions (
    QuestionID INT PRIMARY KEY,
    OrganizerID INT NOT NULL,
    QuestionText NVARCHAR(500) NOT NULL,
    QuestionDate DATE NOT NULL,
    AnswerText NVARCHAR(500),
    Status NVARCHAR(30) NOT NULL CHECK (Status IN ('Not Viewed', 'In processing', 'closed')),
    ManagerID INT NOT NULL,
    CONSTRAINT fk_OrganizerQuestion FOREIGN KEY (OrganizerID) REFERENCES Organizers(OrganizerID),
    CONSTRAINT fk_ManagerQuestion_Organizer FOREIGN KEY (ManagerID) REFERENCES Managers(ManagerID)
);

CREATE TABLE Sales (
    SaleID INT PRIMARY KEY,
    UserID INT NOT NULL,
    TicketID INT NOT NULL,
    SaleDate DATE NOT NULL,
    Status NVARCHAR(50) NOT NULL CHECK (Status IN ('Valid', 'Refund')),
    CONSTRAINT fk_UserOfSale FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT fk_TicketOfSale FOREIGN KEY (TicketID) REFERENCES Tickets(TicketID)
);

CREATE TABLE ShoppingCart (
	ShoppingCartID INT PRIMARY KEY,
    UserID INT NOT NULL,
    TicketID INT NOT NULL,
    CONSTRAINT fk_UserShoppingCart FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT fk_TicketShoppingCart FOREIGN KEY (TicketID) REFERENCES Tickets(TicketID)
);

CREATE TABLE TicketRefund (
	TicketRefundID INT PRIMARY KEY,
    SaleID INT NOT NULL,
    UserID INT NOT NULL,
    Message NVARCHAR(400) NOT NULL,
    RefundDate DATE NOT NULL,
    CONSTRAINT fk_UserOfTicketRefund FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT fk_SaleOfTicketRefund FOREIGN KEY (SaleID) REFERENCES Sales(SaleID)
);

CREATE TABLE UserBlocks (
    BlockID INT PRIMARY KEY,
    UserID INT NOT NULL,
    ManagerID INT NOT NULL,
    Reason NVARCHAR(200) NOT NULL,
    EndDate DATE,
    CONSTRAINT fk_UserBlocks FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT fk_ManagerBlocks_User FOREIGN KEY (ManagerID) REFERENCES Managers(ManagerID)
);

CREATE TABLE UserQuestions (
    QuestionID INT PRIMARY KEY,
    UserID INT NOT NULL,
    QuestionText NVARCHAR(500) NOT NULL,
    QuestionDate DATE NOT NULL,
    AnswerText NVARCHAR(500),
    Status NVARCHAR(30) NOT NULL CHECK (Status IN ('Not Viewed', 'In processing', 'closed')),
    ManagerID INT NOT NULL,
    CONSTRAINT fk_UserQuestion FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT fk_ManagerQuestion_User FOREIGN KEY (ManagerID) REFERENCES Managers(ManagerID)
);

CREATE TABLE Comments (
    CommentID INT PRIMARY KEY,
    UserID INT NOT NULL,
    EventID INT NOT NULL,
    CommentText NVARCHAR(400) NOT NULL,
    CommentDate DATE NOT NULL,
    FivePointRating INT NOT NULL CHECK (FivePointRating IN (0, 1, 2, 3, 4, 5)),
    CONSTRAINT fk_CommentUser FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT fk_CommentEvent FOREIGN KEY (EventID) REFERENCES Events(EventID)
);


DROP TABLE ShoppingCart;
DROP TABLE UserBlocks;
DROP TABLE UserQuestions;
DROP TABLE Comments;
DROP TABLE TicketRefund;
DROP TABLE OrganizerBlocks;
DROP TABLE OrganizerQuestions;
DROP TABLE Sales;
DROP TABLE CheckingOrganizers;
DROP TABLE Tickets;
DROP TABLE EventsSchedule;
DROP TABLE Events;
DROP TABLE Categories;
DROP TABLE Managers;
DROP TABLE Organizers;
DROP TABLE SectorRows;
DROP TABLE Users;
DROP TABLE Roles;
DROP TABLE Locations;

DELETE FROM ShoppingCart;
DELETE FROM UserBlocks;
DELETE FROM UserQuestions;
DELETE FROM Comments;
DELETE FROM TicketRefund;
DELETE FROM OrganizerBlocks;
DELETE FROM OrganizerQuestions;
DELETE FROM Sales;
DELETE FROM CheckingOrganizers;
DELETE FROM Tickets;
DELETE FROM EventsSchedule;
DELETE FROM Events;
DELETE FROM Locations;
DELETE FROM Categories;
DELETE FROM Managers;
DELETE FROM Organizers;
DELETE FROM SectorRows;
DELETE FROM Users;
DELETE FROM Roles;
