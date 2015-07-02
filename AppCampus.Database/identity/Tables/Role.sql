CREATE TABLE [identity].[Role] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_identity.Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [identity].[Role]([Name] ASC);

