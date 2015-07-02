CREATE TABLE [dbo].[SignboardSlideshow] (
    [SignboardSlideshowId] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [SignboardId]          UNIQUEIDENTIFIER NOT NULL,
    [SlideshowId]          UNIQUEIDENTIFIER NOT NULL,
    [StartDate]            DATETIME         NOT NULL,
    [CreatedDate]          DATETIME         NOT NULL,
    [IsActive]             BIT              NOT NULL,
    CONSTRAINT [PK_SignboardSlideshow] PRIMARY KEY CLUSTERED ([SignboardSlideshowId] ASC),
    CONSTRAINT [FK_SignboardSlideshow_Signboard] FOREIGN KEY ([SignboardId]) REFERENCES [dbo].[Signboard] ([SignboardId]),
    CONSTRAINT [FK_SignboardSlideshow_Slideshow] FOREIGN KEY ([SlideshowId]) REFERENCES [dbo].[Slideshow] ([SlideshowId])
);

