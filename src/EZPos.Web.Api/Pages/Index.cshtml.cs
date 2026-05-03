using Microsoft.AspNetCore.Mvc.RazorPages;
using EZPos.Web.Api.Services;

namespace EZPos.Web.Api.Pages;

public class IndexModel : PageModel
{
    private readonly ILicenseApiService _licenseApiService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILicenseApiService licenseApiService, ILogger<IndexModel> logger)
    {
        _licenseApiService = licenseApiService;
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogInformation("Landing page accessed");
    }
}
