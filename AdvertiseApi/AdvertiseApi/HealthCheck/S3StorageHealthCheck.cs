using System;
using System.Threading;
using System.Threading.Tasks;
using AdvertiseApi.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AdvertiseApi.HealthCheck
{
    public class S3StorageHealthCheck : IHealthCheck
    {

        private readonly IadvertiseStorageService _advStorageService;

        public S3StorageHealthCheck(IadvertiseStorageService advStorageService)
        {
            _advStorageService = advStorageService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var isStorageOk = await _advStorageService.CheckHealthAsync();
            return new HealthCheckResult(isStorageOk ? HealthStatus.Healthy : HealthStatus.Unhealthy);
        }
    }
}
