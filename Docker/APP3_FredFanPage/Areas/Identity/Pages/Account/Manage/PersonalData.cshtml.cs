using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using APP3_FredFanPage.Areas.Identity.Data;

namespace APP3_FredFanPage.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly ILogger<PersonalDataModel> _logger;

        private readonly UserManager<FredFanPageUser> _userManager;

        public PersonalDataModel(
            UserManager<FredFanPageUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            this._userManager = userManager;
            this._logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            return this.Page();
        }
    }
}