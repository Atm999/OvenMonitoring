using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OvenMonitoring.Controllers
{
    public class VueComponentsController : Controller
    {
        public IActionResult DatePicker()
        {
            return View();
        }
    }
}
