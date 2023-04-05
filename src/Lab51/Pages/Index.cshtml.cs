using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Lab51.Pages;

public class IndexModel : PageModel
{
    private IOptionsMonitor<ConversionRates> _optionsMonitor;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IOptionsMonitor<ConversionRates> optionsMonitor)
    {
        _logger = logger;
        _optionsMonitor = optionsMonitor;
    }

    [BindProperty]
    [Required]
    [Range(0.01, 1000000000000, ErrorMessage = "Please enter a positive dollar amount")]
    public decimal DollarAmount { get; set; }

    public ConvertedAmounts? ConvertedAmounts { get; private set; }
    public DateTime? LastConversionTime { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        if (ModelState.IsValid)
        {
            LastConversionTime = DateTime.Now;
            ConvertedAmounts = new ConvertedAmounts
            {
                MexicanPesos = DollarAmount * _optionsMonitor.CurrentValue.DollarToMexicanPesoRate,
                Euros = DollarAmount * _optionsMonitor.CurrentValue.DollarToEuroRate,
                DominicanPesos = DollarAmount * _optionsMonitor.CurrentValue.DollarToDominicanPesoRate
            };
        }
    }
}

public class ConvertedAmounts
{
    public decimal MexicanPesos { get; set; }
    public decimal Euros { get; set; }
    public decimal DominicanPesos { get; set; }
}