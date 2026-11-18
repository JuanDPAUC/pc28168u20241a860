using PC28168.U20241a860.Openpay.Platform.Disbursements.Domain.Model.Aggregates;
using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.ValueObjects;
using PC28168.U20241a860.Openpay.Platform.Shared.Domain.Repositories;

namespace PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Repositories;

public interface IDisbursementOrderRepository: IBaseRepository<DisbursementOrder>
{
    Task<DisbursementOrder?> FindByOderTrackingCodedAsync(OpenpayReference orderTrackingCode,
        CancellationToken cancellationToken = default);
}