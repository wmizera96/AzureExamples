using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureExamples
{
    public class IndexModel : PageModel
    {
        public string SimpleSetting { get; set; }
        public string NestedSetting { get; set; }

        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        
        public void OnGet()
        {
            this.SimpleSetting = this._configuration["appSetting"];
            this.NestedSetting= this._configuration["nestedSetting:levelOne:levelTwo"];

        }
    }
}
