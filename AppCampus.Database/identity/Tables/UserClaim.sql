CREATE TABLE [identity].[UserClaim] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [ClaimType]  NVARCHAR (MAX)   NULL,
    [ClaimValue] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_identity.UserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_identity.UserClaim_identity.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [identity].[User] ([Id]) ON DELETE CASCADE
);

