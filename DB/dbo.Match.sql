CREATE TABLE [dbo].[Match] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [StartTime]     DATETIME      NULL,
    [EndTime]       DATETIME      NULL,
    [Name]          VARCHAR (50)  NULL,
    [overs]         INT           NULL,
    [batFirstid]    INT           NULL,
    [winnerid]      INT           DEFAULT ((0)) NULL,
    [endComment]    VARCHAR (MAX) NULL,
    [tossComment]   VARCHAR (MAX) NULL,
    [team1id]       INT           NULL,
    [team2id]       INT           NULL,
    [inning]        INT           DEFAULT ((1)) NULL,
    [inningComment] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Match] PRIMARY KEY CLUSTERED ([Id] ASC)
);

