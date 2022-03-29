using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Domain;
// using MyShop.Domain;
using MyStore.Integration;

namespace MyStore.Pages
{
    public class CartModel : PageModel
    {
        private readonly OrderService orderService;
        #region Bind Properties
        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public IEnumerable<Item>? Items { get; set; }
        #endregion

        public CartModel(OrderService orderService)
        {
            this.orderService = orderService;
        }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            ArgumentNullException.ThrowIfNull(Email);
            ArgumentNullException.ThrowIfNull(Items);

            await 
                orderService.QueueOrder(new(Email, Items));

            return RedirectToPage("ThankYou");
        }
    }
}