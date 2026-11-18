using PC28168.U20241a860.Openpay.Platform.Shared.Domain.Model;

namespace PC28168.U20241a860.Openpay.Platform.Disbursements.Domain.Model.Aggregates;

public  partial class DisbursermentOrderAudit: IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    
    public DateTimeOffset? UpdatedAt { get; set; }
    
}