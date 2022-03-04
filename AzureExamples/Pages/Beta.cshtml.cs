using AzureExamples.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace AzureExamples.Pages
{
    [RazorPageFeatureGate(Feature.Beta)]
    public class BetaModel : PageModel
    {
        private readonly IFeatureManager featureManager;

        public BetaModel(IFeatureManager featureManager)
        {
            this.featureManager = featureManager;
        }

        public async Task OnGet()
        {
            var isEnabled = await featureManager.IsEnabledAsync("Beta");

            if (isEnabled == false)
                RedirectToPage("Index");
        }
    }
}
