CREATE TABLE [dbo].[Screenshot] (
    [SignboardScreenshotId] UNIQUEIDENTIFIER NOT NULL,
    [DeviceId]              UNIQUEIDENTIFIER NOT NULL,
    [Base64ImageString]     VARCHAR (MAX)    NOT NULL,
    [CreatedDate]           DATETIME         NOT NULL,
    CONSTRAINT [PK_Screenshot] PRIMARY KEY CLUSTERED ([SignboardScreenshotId] ASC)
);



