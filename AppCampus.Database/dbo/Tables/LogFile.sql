﻿CREATE TABLE [dbo].[LogFile] (
    [LogFileId] UNIQUEIDENTIFIER NOT NULL,
    [Binary]    VARBINARY (MAX)  NOT NULL,
    CONSTRAINT [PK_LogFile] PRIMARY KEY CLUSTERED ([LogFileId] ASC)
);

