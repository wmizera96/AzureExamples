#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureExamples.Data;

namespace AzureExamples.Pages
{
    public class DataModel : PageModel
    {
        private readonly ExampleDatabaseContext _context;

        public DataModel(ExampleDatabaseContext context)
        {
            _context = context;
        }

        public IList<Customer> Customers { get;set; }

        public async Task OnGetAsync()
        {
            Customers = await _context.Customers.Take(30).ToListAsync();
        }
    }
}
