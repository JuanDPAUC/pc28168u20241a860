using PC28168.U20241a860.Openpay.Platform.Disbursements.Domain.Model.Aggregates;
using PC28168.U20241a860.Openpay.Platform.Settlements.Application.Services;
using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.Queries;
using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Repositories;

namespace PC28168.U20241a860.Openpay.Platform.Settlements.Application.Internal.QueryServices;

public class DisbursementOrderQueryService(IDisbursementOrderRepository disbursementOrderRepository)
: IDisbursementOrderQueryService
{
public async Task<DisbursementOrder?> Handle(GetDisbursementOrderByOrderTrackingCode  query,
    CancellationToken cancellationToken = default) =>
    await disbursementOrderRepository.FindByOderTrackingCodedAsync(query.OrderTrackingCode, cancellationToken);
}