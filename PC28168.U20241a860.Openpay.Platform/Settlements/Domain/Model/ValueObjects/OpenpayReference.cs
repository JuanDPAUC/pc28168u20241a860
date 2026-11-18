namespace PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.ValueObjects;

public sealed record OpenpayReference
{
    public OpenpayReference(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("open pay Id cannot be null", nameof(value));
        
        Value = value;
    
    }
    public Guid Value { get; }
}