using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace API.Controllers
{
    public class NewApiController : Controller
    {
        public ActionResult Index()
        {
            return Content("Hellow World!");
        }
    }
}