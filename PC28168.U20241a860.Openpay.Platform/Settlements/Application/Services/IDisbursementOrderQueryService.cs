using PC28168.U20241a860.Openpay.Platform.Disbursements.Domain.Model.Aggregates;
using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.Queries;

namespace PC28168.U20241a860.Openpay.Platform.Settlements.Application.Services;

public interface IDisbursementOrderQueryService
{
    Task<DisbursementOrder?> Handle( GetDisbursementOrderByOrderTrackingCode query, CancellationToken cancellationToken = default);

}