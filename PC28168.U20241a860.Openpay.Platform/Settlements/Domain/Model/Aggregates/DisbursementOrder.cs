using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.Commands;
using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.ValueObjects;
using PC28168.U20241a860.Openpay.Platform.Shared.Domain.Model.ValueObjects;

namespace PC28168.U20241a860.Openpay.Platform.Disbursements.Domain.Model.Aggregates;

public partial class DisbursementOrder
{
    protected DisbursementOrder()
    {
        OrderTrackingCode = null!;
        AffiliateStoreId = null!;
        ExecutionSchedule = null!;
        TransferCap = null!;
        User = null!;

    }
    
    public DisbursementOrder(CreateDibusermentOrderCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        OrderTrackingCode = command.OrderTrackingCode;
        AffiliateStoreId = command.AffiliateStoreId;
        ExecutionSchedule = command.ExecutionSchedule;
        TransferCap= command.TransferCap;
        User = command.User;
        
    }
    
    
    public int Id { get; private set; }
    public OpenpayReference OrderTrackingCode { get; private set; }
    public StoreIdentifier AffiliateStoreId { get; private set; }
    public DateInterval ExecutionSchedule { get; private set; }
    public FinancialAmount TransferCap { get; private set; }
    public DisbursementStatus Status { get; private set; }
    public string User { get; private set; }
    public string Remarks{ get; private set; }

}