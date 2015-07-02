CREATE TABLE [dbo].[Software] (
    [SoftwareId]     UNIQUEIDENTIFIER CONSTRAINT [DF__Software__Softwa__7814D14C] DEFAULT (newid()) NOT NULL,
    [Version]        VARCHAR (20)     NOT NULL,
    [SoftwareFileId] UNIQUEIDENTIFIER NULL,
    [CreatedDate]    DATETIME         NOT NULL,
    [Comment]        VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_Software] PRIMARY KEY CLUSTERED ([SoftwareId] ASC),
    CONSTRAINT [FK_Software_SoftwareFile] FOREIGN KEY ([SoftwareFileId]) REFERENCES [dbo].[SoftwareFile] ([SoftwareFileId])
);



