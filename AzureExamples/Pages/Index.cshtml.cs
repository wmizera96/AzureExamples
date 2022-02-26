using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureExamples
{
    public class IndexModel : PageModel
    {
        public string AppSetting { get; set; }
        public void OnGet()
        {
            this.AppSetting = "setting";
        }
    }
}
