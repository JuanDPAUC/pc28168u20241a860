using Microsoft.EntityFrameworkCore;
using PC28168.U20241a860.Openpay.Platform.Disbursements.Domain.Model.Aggregates;
using PC28168.U20241a860.Openpay.Platform.Settlements.Application.Errors;
using PC28168.U20241a860.Openpay.Platform.Settlements.Application.Services;
using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.Commands;
using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Repositories;
using PC28168.U20241a860.Openpay.Platform.Shared.Application.Patterns;
using PC28168.U20241a860.Openpay.Platform.Shared.Domain.Repositories;

namespace PC28168.U20241a860.Openpay.Platform.Settlements.Application.Internal.CommandServices;

public class DisbursementOrderCommandService(IDisbursementOrderRepository disbursementOrderRepository,
IUnitOfWork unitOfWork,
ILogger<DisbursementOrderCommandService> logger)
: IDisbursementOrderCommandService
{
    public async Task<Result<DisbursementOrder, CreateDisbursementOrderError>> Handle(CreateDisbursementOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var existing = await disbursementOrderRepository.FindByOderTrackingCodedAsync(command.OrderTrackingCode, cancellationToken);
        if (existing != null)
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(CreateDisbursementOrderError.DuplicateOrderTrackingCode);
        try
        {
            var campaign = new DisbursementOrder(command);
            await disbursementOrderRepository.AddAsync(campaign, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Success(campaign);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex,
                "Invalid arguments while creating disbursement order for  {CreateDisbursementOrderError}", command.OrderTrackingCode);
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(
                CreateDisbursementOrderError.UnexpectedError);
        }
        catch (DbUpdateException ex) when (IsDuplicateKeyViolation(ex))
        {
            logger.LogWarning(ex,
                "Duplicate key violation creating disbursement order for {CreateDisbursementOrderError}",
                command.OrderTrackingCode);
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(
                CreateDisbursementOrderError.DuplicateOrderTrackingCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex,
                "Database update failed creating disbursement order  for {CreateDisbursementOrderError}",
                command.OrderTrackingCode);
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(
                CreateDisbursementOrderError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Unexpected error creating disbursement order for {CampaignIdentifier}",
                command.OrderTrackingCode);
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(
                CreateDisbursementOrderError.UnexpectedError);
        }
        
        
    }
    private static bool IsDuplicateKeyViolation(DbUpdateException exception)
    {
        for (Exception? current = exception; current is not null; current = current.InnerException)
        {
            if (!string.Equals(current.GetType().Name, "MySqlException", StringComparison.Ordinal)) continue;
            var numberProperty = current.GetType().GetProperty("Number");
            if (numberProperty?.PropertyType == typeof(int) &&
                numberProperty.GetValue(current) is int errorCode &&
                errorCode == 1062)
                return true;
        }
        return false;
    }
    
    
}