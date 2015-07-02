CREATE TABLE [widget].[Image] (
    [ImageId]     UNIQUEIDENTIFIER CONSTRAINT [Df_Image_ImageId] DEFAULT (newid()) NOT NULL,
    [Base64Image] VARCHAR (MAX)    NOT NULL,
    [Name]        VARCHAR (64)     DEFAULT ('Untitled') NOT NULL,
    [CreatedDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ImageId] ASC)
);



