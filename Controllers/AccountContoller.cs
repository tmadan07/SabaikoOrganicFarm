using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SabaikoOrganicFarm.Controllers
{
    public class AccountContoller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
