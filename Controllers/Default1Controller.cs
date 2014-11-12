using System.Web.Mvc;

namespace igdBot.Controllers
{
    public class IgdController : Controller
    {
        //
        // 
        [HttpGet]
        public string Move()
        {
            return "BET";
        }

        [HttpPost]
        public string Start()
        {
            return "start";
        }

        [HttpPost]
        public string Update()
        {
            return "done";
        }
    }
}
