using PC28168.U20241a860.Openpay.Platform.Shared.Domain.Repositories;
using PC28168.U20241a860.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace PC28168.U20241a860.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;


public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}