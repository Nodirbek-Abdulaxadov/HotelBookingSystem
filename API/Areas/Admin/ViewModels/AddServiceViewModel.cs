using Microsoft.Build.Framework;

namespace API.Areas.Admin.ViewModels;

public class AddServiceViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int OrderId { get; set; }
}