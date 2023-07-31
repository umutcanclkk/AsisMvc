using AsisMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsisMvc.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CategoryChart()
        {
            return Json(BlogList(),JsonRequestBehavior.AllowGet);
        }

        public List<CategoryClass> BlogList()
        {
            List<CategoryClass> ct = new List<CategoryClass>();
            ct.Add(new CategoryClass()
            {

                CategoryName="Kitap",
                CategoryCount= 3

            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Dizi",
                CategoryCount = 2
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Eğitim",
                CategoryCount = 4 
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Spor",
                CategoryCount = 1
            });

            return ct;
        }

    }
}