CREATE TABLE [dbo].[Device] (
    [DeviceId]    UNIQUEIDENTIFIER CONSTRAINT [DV_Device] DEFAULT (newid()) NOT NULL,
    [CompanyId]   UNIQUEIDENTIFIER NOT NULL,
    [MacAddress]  VARCHAR (20)     NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [Comment]     VARCHAR (256)    NULL,
    CONSTRAINT [PK_Device] PRIMARY KEY CLUSTERED ([DeviceId] ASC),
    CONSTRAINT [FK_Device_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [UX_Device_MacAddress] UNIQUE NONCLUSTERED ([MacAddress] ASC)
);



