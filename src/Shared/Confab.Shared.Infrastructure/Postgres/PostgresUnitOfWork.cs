using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Postgres;

public abstract class PostgresUnitOfWork<T> : IUnitOfWork where T : DbContext
{
    private readonly T _dbContext;
    private readonly ILogger<PostgresUnitOfWork<T>> _logger;

    protected PostgresUnitOfWork(T dbContext, ILogger<PostgresUnitOfWork<T>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await RollbackWithLogsAsync(transaction, ex);
            throw;
        }
    }

    private async Task RollbackWithLogsAsync(IDbContextTransaction transaction, Exception exContext)
    {
        try
        {
            await transaction.RollbackAsync();
            _logger.LogWarning(exContext, message: $"Rollback DB transaction {transaction.TransactionId} applied.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: $"Rollback DB transaction {transaction.TransactionId} failed.");
            throw;
        }
    }
}