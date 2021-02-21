CREATE TABLE [dbo].[Team] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Score]    INT          DEFAULT ((0)) NULL,
    [wideball] INT          DEFAULT ((0)) NULL,
    [Noball]   INT          DEFAULT ((0)) NULL,
    [name]     VARCHAR (50) NOT NULL,
    [wickets]  INT          DEFAULT ((0)) NULL,
    [overs]    FLOAT (53)   DEFAULT ((0.0)) NULL,
    [runrate]  FLOAT (53)   DEFAULT ((0.0)) NULL,
    CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED ([Id] ASC)
);

