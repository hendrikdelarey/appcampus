CREATE TABLE [identity].[UserRole] (
    [UserRoleId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [RoleId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.UserRoles] PRIMARY KEY CLUSTERED ([UserRoleId] ASC),
    CONSTRAINT [FK_identity.UserRole_identity.Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [identity].[Role] ([Id]),
    CONSTRAINT [FK_identity.UserRole_identity.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [identity].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [identity].[UserRole]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [identity].[UserRole]([UserId] ASC);

