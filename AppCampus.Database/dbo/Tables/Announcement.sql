CREATE TABLE [dbo].[Announcement] (
    [AnnouncementId] UNIQUEIDENTIFIER NOT NULL,
    [Message]        VARCHAR (256)    NOT NULL,
    [StartDate]      DATETIME         NOT NULL,
    [EndDate]        DATETIME         NOT NULL,
    [Severity]       VARCHAR (16)     NOT NULL,
    [CompanyId]      UNIQUEIDENTIFIER NOT NULL,
    [IsActive]       BIT              NOT NULL,
    [Name]           VARCHAR (64)     NOT NULL,
    [IsDeleted]      BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([AnnouncementId] ASC),
    FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);







