using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureExamples
{
    public class IndexModel : PageModel
    {
        public string SimpleSetting { get; set; }
        public string NestedSetting { get; set; }

        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this._configuration = configuration;
            this._logger = logger;
            this.SimpleSetting = this._configuration["simpleSetting"];
            this.NestedSetting= this._configuration["nestedSetting:levelOne:levelTwo"];
        }

        
        public void OnGet()
        {
            this._logger.LogInformation("Index page loaded");
        }
    }
}
