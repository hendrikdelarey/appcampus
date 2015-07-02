CREATE TABLE [dbo].[SignboardAnnouncement] (
    [SignboardAnnouncementId] UNIQUEIDENTIFIER CONSTRAINT [SignboardAnnouncement_DefaultValue] DEFAULT (newid()) NOT NULL,
    [AnnouncementId]          UNIQUEIDENTIFIER NOT NULL,
    [SignboardId]             UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([SignboardAnnouncementId] ASC),
    FOREIGN KEY ([AnnouncementId]) REFERENCES [dbo].[Announcement] ([AnnouncementId]),
    FOREIGN KEY ([SignboardId]) REFERENCES [dbo].[Signboard] ([SignboardId])
);

