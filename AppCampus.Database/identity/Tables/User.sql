CREATE TABLE [identity].[User] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [CompanyId]    UNIQUEIDENTIFIER NOT NULL,
    [PasswordHash] NVARCHAR (MAX)   NOT NULL,
    [UserName]     NVARCHAR (256)   NOT NULL,
    [FirstName]    VARCHAR (50)     NOT NULL,
    [LastName]     VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_identity.Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [identity].[User]([UserName] ASC);

