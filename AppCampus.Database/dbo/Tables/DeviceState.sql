CREATE TABLE [dbo].[DeviceState] (
    [DeviceStateId] UNIQUEIDENTIFIER CONSTRAINT [DV_DeviceState] DEFAULT (newid()) NOT NULL,
    [DeviceId]      UNIQUEIDENTIFIER NOT NULL,
    [State]         VARCHAR (20)     NOT NULL,
    [ChangedDate]   DATETIME         NOT NULL,
    CONSTRAINT [PK_DeviceState] PRIMARY KEY CLUSTERED ([DeviceStateId] ASC),
    CONSTRAINT [FK_DeviceState_Device] FOREIGN KEY ([DeviceId]) REFERENCES [dbo].[Device] ([DeviceId])
);

