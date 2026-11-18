using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.ValueObjects;
using PC28168.U20241a860.Openpay.Platform.Shared.Domain.Model.ValueObjects;

namespace PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.Commands;

public record CreateDibusermentOrderCommand(OpenpayReference OrderTrackingCode, StoreIdentifier AffiliateStoreId,
    DateInterval ExecutionSchedule, FinancialAmount TransferCap, string User);
    