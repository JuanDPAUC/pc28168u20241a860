namespace PC28168.U20241a860.Openpay.Platform.Shared.Domain.Model.ValueObjects;

public sealed record DateInterval
{
    public DateInterval(DateOnly ScheduledForm, DateOnly SchedulledTo)
    {
        if  (ScheduledForm <= SchedulledTo)
            throw new ArgumentException("ScheduledForm must be less or equal to SchedulledTo");
        
    }
    
}