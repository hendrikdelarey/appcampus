CREATE TABLE [dbo].[SoftwareFile] (
    [SoftwareFileId] UNIQUEIDENTIFIER NOT NULL,
    [FileName]       VARCHAR (100)    NOT NULL,
    [Binary]         VARBINARY (MAX)  NOT NULL,
    CONSTRAINT [PK_SoftwareFileId] PRIMARY KEY CLUSTERED ([SoftwareFileId] ASC)
);

