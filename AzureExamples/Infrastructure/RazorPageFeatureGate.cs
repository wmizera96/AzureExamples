using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement;

namespace AzureExamples.Infrastructure
{
    public enum Feature
    {
        Beta
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class RazorPageFeatureGate : Attribute, IAsyncPageFilter
    {
        private IEnumerable<Feature> Features { get; }
        public RazorPageFeatureGate(params Feature[] features)
            => Features = features;


        public virtual async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var featureManager = context.HttpContext.RequestServices.GetRequiredService<IFeatureManagerSnapshot>();

            foreach(var feature in Features)
            {
                var isEnabled = await featureManager.IsEnabledAsync(Enum.GetName(feature)).ConfigureAwait(false);
                if (isEnabled)
                {
                    await next.Invoke().ConfigureAwait(false);
                    return;
                }
            }

            context.HttpContext.Response.StatusCode = 404;
            await context.HttpContext.Response.CompleteAsync().ConfigureAwait(false);
        }

        public virtual Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context) => Task.CompletedTask;
    }
}
