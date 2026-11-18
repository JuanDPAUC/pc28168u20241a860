namespace PC28168.U20241a860.Openpay.Platform.Shared.Domain.Model.ValueObjects;

public sealed record FinancialAmount
{
    public FinancialAmount(decimal Amount, string CurrencyCode)
    {
        if (Amount <= decimal.Zero)
            throw new ArgumentNullException( "Amount must be greater than zero", nameof(Amount));
        if (string.IsNullOrWhiteSpace(CurrencyCode))
            throw new ArgumentNullException( "Currency must not be null or empty", nameof(CurrencyCode));
        
    }
    public FinancialAmount MultiplyBy(decimal factor)
    {
        return new FinancialAmount(Amount * factor, CurrencyCode);
    }
    public decimal Amount { get; }
    public string CurrencyCode { get; }
}