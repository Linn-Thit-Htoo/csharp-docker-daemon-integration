using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using MigrationCore.AppDbContextModels;

namespace MigrationCore.Services
{
    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<MigrationHostedService> _logger;

        public MigrationHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<MigrationHostedService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var lst = new List<TblBlog>();
                var scope = _serviceScopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                for (int i = 1; i <= 20000; i++)
                {
                    lst.Add(new TblBlog()
                    {
                        BlogId = Guid.NewGuid().ToString(),
                        BlogTitle = $"Blog Title {i}",
                        BlogAuthor = $"Blog Author: {i}",
                        BlogContent = $"Blog Content: {i}",
                        IsDeleted = false
                    });
                }

                await context.BulkInsertAsync(lst, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
