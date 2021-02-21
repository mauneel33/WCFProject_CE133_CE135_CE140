CREATE TABLE [dbo].[Commentary] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Match_ID] INT           NOT NULL,
    [overs]    FLOAT (53)    DEFAULT ((0.0)) NULL,
    [comment]  VARCHAR (MAX) NULL,
    [Time]     DATETIME      NULL,
    CONSTRAINT [PK_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

