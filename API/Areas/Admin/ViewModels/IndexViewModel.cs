namespace API.Areas.Admin.ViewModels;

public class IndexViewModel
{
    public int OrdersCount { get; set; }
    public int GuestsCount { get; set; }
    public int PendingOrdersCount { get; set; }
    public decimal TotalBalance { get; set; }
}