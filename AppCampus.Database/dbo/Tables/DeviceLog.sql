CREATE TABLE [dbo].[DeviceLog] (
    [DeviceLogId] UNIQUEIDENTIFIER NOT NULL,
    [LogFileId]   UNIQUEIDENTIFIER NULL,
    [FileName]    VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_DeviceLog] PRIMARY KEY CLUSTERED ([DeviceLogId] ASC),
    CONSTRAINT [FK_DeviceLog_LogFile1] FOREIGN KEY ([LogFileId]) REFERENCES [dbo].[LogFile] ([LogFileId])
);

