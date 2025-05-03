using System.Collections.Concurrent;

namespace OnlineShop.API.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, RequestLog> RequestCounts = new ConcurrentDictionary<string, RequestLog>();
        private const int MaxRequests = 10; // محدودیت درخواست‌ها
        private readonly TimeSpan TimeWindow = TimeSpan.FromMinutes(1); // مدت زمان پنجره

        public RateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
         
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (ipAddress == null)
            {
                await _next(context);
                return;
            }

            
            if (IsRateLimited(ipAddress))
            {
                context.Response.StatusCode = 429; 
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

        
            RegisterRequest(ipAddress);

         
            await _next(context);
        }

        private bool IsRateLimited(string ipAddress)
        {
            var requestLog = RequestCounts.GetOrAdd(ipAddress, new RequestLog());

         
            var recentRequests = requestLog.Requests.Where(r => r > DateTime.UtcNow - TimeWindow).ToList();

           
            if (recentRequests.Count >= MaxRequests)
            {
                return true;
            }

            return false;
        }

        private void RegisterRequest(string ipAddress)
        {
            var requestLog = RequestCounts.GetOrAdd(ipAddress, new RequestLog());
            requestLog.Requests.Add(DateTime.UtcNow);
        }
    }

    public class RequestLog
    {
        // ذخیره تاریخ و زمان درخواست‌ها
        public ConcurrentBag<DateTime> Requests { get; } = new ConcurrentBag<DateTime>();
    }
}
