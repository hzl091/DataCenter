using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DC.Web.Controllers
{
    public class TableController : Controller
    {
        // GET: Table
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int id, string orderBy = "ID", string sort = "DESC", int pageIndex = 1)
        {
            return View();
        }
    }
}