using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BorrowedFormController : Controller
    {
        [Route("borrowedform/borrowedForm", Name = "MuonSach")] 
		public IActionResult BorrowedForm()
        {
            return View();
        }
    }
}
