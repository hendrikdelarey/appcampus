CREATE TABLE [dbo].[WidgetDefinition] (
    [WidgetDefinitionId] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]               VARCHAR (50)     NOT NULL,
    [AssemblyType]       VARCHAR (200)    NOT NULL,
    CONSTRAINT [PK_WidgetDefinition] PRIMARY KEY CLUSTERED ([WidgetDefinitionId] ASC)
);

