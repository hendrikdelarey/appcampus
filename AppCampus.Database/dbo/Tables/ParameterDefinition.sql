CREATE TABLE [dbo].[ParameterDefinition] (
    [ParameterDefinitionId] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [WidgetDefinitionId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]                  VARCHAR (50)     NOT NULL,
    [DefaultValue]          VARCHAR (200)    NULL,
    [ParameterType] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_ParameterDefinition] PRIMARY KEY CLUSTERED ([ParameterDefinitionId] ASC),
    CONSTRAINT [FK_ParameterDefinition_WidgetDefinition] FOREIGN KEY ([WidgetDefinitionId]) REFERENCES [dbo].[WidgetDefinition] ([WidgetDefinitionId])
);


GO


CREATE NONCLUSTERED INDEX [CStoreIX_ParameterDefinition] ON [dbo].[ParameterDefinition] ([Name], [WidgetDefinitionId]) INCLUDE (DefaultValue, ParameterType)
