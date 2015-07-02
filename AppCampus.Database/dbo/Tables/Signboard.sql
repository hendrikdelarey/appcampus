CREATE TABLE [dbo].[Signboard] (
    [SignboardId]          UNIQUEIDENTIFIER NOT NULL,
    [CompanyId]            UNIQUEIDENTIFIER NOT NULL,
    [Name]                 VARCHAR (200)    NOT NULL,
    [CreatedDate]          DATETIME         NOT NULL,
    [DeviceId]             UNIQUEIDENTIFIER NOT NULL,
    [SoftwareVersion]      VARCHAR (10)     CONSTRAINT [DF_Signboard_SoftwareVersion] DEFAULT ('0.0.0') NOT NULL,
    [FontFactor]           FLOAT (53)       CONSTRAINT [DF_Signboard_FontFactor] DEFAULT ((1)) NOT NULL,
    [IsShowingScreensaver] BIT              CONSTRAINT [DF_Signboard_IsShowingScreensaver] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Signboard] PRIMARY KEY CLUSTERED ([SignboardId] ASC),
    CONSTRAINT [FK_Signboard_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_Signboard_Device] FOREIGN KEY ([DeviceId]) REFERENCES [dbo].[Device] ([DeviceId]),
    CONSTRAINT [UC_Signboard_DeviceId] UNIQUE NONCLUSTERED ([DeviceId] ASC)
);







