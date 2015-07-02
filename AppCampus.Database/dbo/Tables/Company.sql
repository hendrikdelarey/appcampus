CREATE TABLE [dbo].[Company] (
    [CompanyId] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]      VARCHAR (200)    NOT NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([CompanyId] ASC)
);

