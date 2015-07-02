CREATE TABLE [dbo].[SlideWidget] (
    [SlideWidgetId]      UNIQUEIDENTIFIER NOT NULL,
    [SlideId]            UNIQUEIDENTIFIER NOT NULL,
    [WidgetDefinitionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SlideWidget] PRIMARY KEY CLUSTERED ([SlideWidgetId] ASC),
    CONSTRAINT [FK_SlideWidget_Slide] FOREIGN KEY ([SlideId]) REFERENCES [dbo].[Slide] ([SlideId]),
    CONSTRAINT [FK_SlideWidget_WidgetDefinition] FOREIGN KEY ([WidgetDefinitionId]) REFERENCES [dbo].[WidgetDefinition] ([WidgetDefinitionId])
);





