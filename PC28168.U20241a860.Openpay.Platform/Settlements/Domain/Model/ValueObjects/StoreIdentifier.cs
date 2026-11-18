namespace PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.ValueObjects;

public sealed record  StoreIdentifier
{
    public StoreIdentifier(Guid value)
    {
        if (value == Guid.Empty)
        throw new ArgumentException("Advertiser Id cannot be null", nameof(value));
        
        Value = value;
    }
    public Guid Value { get; }
}