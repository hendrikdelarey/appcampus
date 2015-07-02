CREATE TABLE [dbo].[Slide] (
    [SlideId]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [SlideshowId] UNIQUEIDENTIFIER NOT NULL,
    [Duration]    INT              NOT NULL,
    [Transition]  VARCHAR (50)     NULL,
    [OrderNumber] INT              NOT NULL,
    [IsDeleted]   BIT              CONSTRAINT [Slide_IsDeleted_DefaultValue] DEFAULT ((0)) NOT NULL,
    [IsActive]    BIT              DEFAULT ((1)) NOT NULL,
    [Name]        VARCHAR (64)     DEFAULT ('Untitled') NOT NULL,
    [BackgroundColour] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Slide] PRIMARY KEY CLUSTERED ([SlideId] ASC),
    CONSTRAINT [FK_Slide_Slideshow] FOREIGN KEY ([SlideshowId]) REFERENCES [dbo].[Slideshow] ([SlideshowId])
);









