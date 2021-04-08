CREATE TABLE [dbo].[BitcoinPriceHistory] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [CreateDate] DATETIME     NULL,
    [LastPrice]  MONEY        NULL,
    [LastChange] DECIMAL (18) NULL,
    [Volume]     DECIMAL (18) NULL,
    [HighPrice]  MONEY        NULL,
    [LowPrice]   MONEY        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

