using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Safety.Models;

namespace Safety.Controllers
{
    public class BaseController : Controller
    {
        protected SecurityContext context;

        protected BaseController()
        {
            context = new SecurityContext();
        }
    }
}
