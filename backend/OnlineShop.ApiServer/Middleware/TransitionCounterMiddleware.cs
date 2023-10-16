using System.Collections.Concurrent;

namespace OnlineShop.WebApi.Middleware
{
    public class TransitionCounterMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ITransitionCounterService _counterService;
        private readonly ConcurrentDictionary<PathString, int> _counter;

        public TransitionCounterMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            //_counterService = counterService ?? throw new ArgumentNullException(nameof(counterService));
            _counter = new ConcurrentDictionary<PathString, int>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string domain = context.Request.Path;
            //_counterService.DomainRequestCounterDictionary.AppOrUpdate(domain, 1(_, existing) => existing + 1);
            if (_counter.TryGetValue(domain, out int value))
            {
                _counter[domain] = ++value;
            }
            else
            {
                _counter.TryAdd(domain, 1);
            }
            await _next(context);
        }
    }
}
