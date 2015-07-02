CREATE TABLE [dbo].[Slideshow] (
    [SlideshowId] UNIQUEIDENTIFIER NOT NULL,
    [CompanyId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]        VARCHAR (200)    NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [IsDeleted]   BIT              NOT NULL,
    CONSTRAINT [PK_Slideshow] PRIMARY KEY CLUSTERED ([SlideshowId] ASC),
    CONSTRAINT [FK_Slideshow_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);







