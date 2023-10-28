
namespace MyBlog.Data;

public static class MigrationsExtensions
{
    public static IServiceCollection AddMigration<TContext>(this IServiceCollection services) where TContext : DbContext
        => services.AddHostedService<MigrationHostedService<TContext>>();

    public class MigrationHostedService<TContext>(IServiceScopeFactory serviceScopeFactory) : IHostedService
        where TContext : DbContext
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = serviceScopeFactory.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await context.Database.EnsureCreatedAsync(cancellationToken: cancellationToken);
                await context.Database.MigrateAsync(cancellationToken: cancellationToken);
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}