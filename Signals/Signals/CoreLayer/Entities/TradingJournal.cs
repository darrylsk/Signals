using System;
using Signals.CoreLayer.Entities.Base;
using Signals.CoreLayer.Enums;

namespace Signals.CoreLayer.Entities;

public class TradingJournal : Entity
{
    public TradingJournal() : base()
    {
        Symbol ??= "";
    }
    
    public TradingJournal(string symbol, DateTime timeOfTransaction, decimal? quantity, TransactionTypes transactionType, 
        decimal? unitPrice)
    {
        Symbol = symbol;
        TimeOfTransaction = timeOfTransaction;
        Quantity = quantity;
        TransactionType = transactionType;
        UnitPrice = unitPrice;
    }

    public string Symbol { get; set; }
    public DateTime TimeOfTransaction { get; set; }
    public decimal? Quantity { get; set; }
    public TransactionTypes TransactionType { get; set; }
    public decimal? UnitPrice { get; set; }
}