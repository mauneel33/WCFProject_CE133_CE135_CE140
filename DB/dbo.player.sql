CREATE TABLE [dbo].[player] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [Team_ID]    INT          NOT NULL,
    [name]       VARCHAR (50) NOT NULL,
    [batruns]    INT          DEFAULT ((0)) NULL,
    [wickets]    INT          DEFAULT ((0)) NULL,
    [strikerate] FLOAT (53)   DEFAULT ((0.0)) NULL,
    [economy]    FLOAT (53)   DEFAULT ((0.0)) NULL,
    [balls]      INT          DEFAULT ((0)) NULL,
    [fours]      INT          DEFAULT ((0)) NULL,
    [sixes]      INT          DEFAULT ((0)) NULL,
    [type]       INT          NOT NULL,
    [overs]      FLOAT (53)   DEFAULT ((0.0)) NULL,
    [bowlruns]   INT          DEFAULT ((0)) NULL,
    [status]     INT          DEFAULT ((0)) NULL,
    CONSTRAINT [PK_player] PRIMARY KEY CLUSTERED ([Id] ASC)
);

