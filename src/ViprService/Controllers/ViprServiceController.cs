using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViprService.Models;

namespace ViprService.Controllers
{
    public class ViprServiceController : Controller
    {
        private ViprServiceContext db = new ViprServiceContext();

        // GET: ViprServiceUser
        public async Task<ActionResult> Index()
        {
             var userId = User.Identity.Name;

             //var list = await this.db.ViprServiceUsers
             //        // .Where(u => u.UserName == userId)
             //        .Select(v => v.Configurations).ToListAsync();

             var list = await this.db.Configurations.ToListAsync();

            return View(list);
        }
    
    }
}