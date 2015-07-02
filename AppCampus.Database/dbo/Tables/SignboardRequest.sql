CREATE TABLE [dbo].[SignboardRequest] (
    [SignboardRequestId] UNIQUEIDENTIFIER NOT NULL,
    [SignboardId]        UNIQUEIDENTIFIER NOT NULL,
    [RequestType]        VARCHAR (50)     NOT NULL,
    [IsSent]             BIT              NOT NULL,
    [IsProcessed]        BIT              NOT NULL,
    [IsCancelled]        BIT              NOT NULL,
    [Value]              VARCHAR (64)     NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT '2015-01-01', 
    CONSTRAINT [PK_SignboardRequest] PRIMARY KEY CLUSTERED ([SignboardRequestId] ASC),
    CONSTRAINT [FK_Signboard_SignboardRequest] FOREIGN KEY ([SignboardId]) REFERENCES [dbo].[Signboard] ([SignboardId])
);

