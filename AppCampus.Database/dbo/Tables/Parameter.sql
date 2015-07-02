CREATE TABLE [dbo].[Parameter] (
    [ParameterId]           UNIQUEIDENTIFIER NOT NULL,
    [ParameterDefinitionId] UNIQUEIDENTIFIER NOT NULL,
    [Value]                 VARCHAR (200)    NOT NULL,
    [SlideWidgetId]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Parameter] PRIMARY KEY CLUSTERED ([ParameterId] ASC),
    CONSTRAINT [FK_Parameter_ParameterDefinition] FOREIGN KEY ([ParameterDefinitionId]) REFERENCES [dbo].[ParameterDefinition] ([ParameterDefinitionId]),
    CONSTRAINT [FK_Parameter_SlideWidget] FOREIGN KEY ([SlideWidgetId]) REFERENCES [dbo].[SlideWidget] ([SlideWidgetId])
);







