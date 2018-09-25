# A-Game-Of-Faces


##To Creat DB

Create Data.mdf in App_Data, needs SQL Server specified in connection string in web.config: (LocalDB)\MSSQLLocalDB
Run:
CREATE TABLE [dbo].[Statistics] (
    [Id]              INT IDENTITY(1,1) NOT NULL,
    [User]            NVARCHAR (50)     NOT NULL,
    [Total_Guesses]   INT               NOT NULL,
    [Correct_Guesses] INT               NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);