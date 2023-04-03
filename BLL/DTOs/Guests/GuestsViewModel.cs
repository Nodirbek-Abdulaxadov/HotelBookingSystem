using Entities;

namespace BLL.DTOs.Guests
{
    public class GuestsViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<User> Guests { get; set; }
    }
}
