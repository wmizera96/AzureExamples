#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureExamples.Data;

namespace AzureExamples.Pages
{
    public class DataModel : PageModel
    {
        private readonly ExampleDatabaseContext _context;
        private readonly ILogger<DataModel> _logger;

        public DataModel(ExampleDatabaseContext context, ILogger<DataModel> logger)
        {
            _context = context;
            this._logger = logger;
        }

        public IList<Customer> Customers { get;set; }

        public async Task OnGetAsync()
        {
            Customers = await _context.Customers.Take(30).ToListAsync();
            this._logger.LogInformation("Data page loaded");
        }
    }
}
