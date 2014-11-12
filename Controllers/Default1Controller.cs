using System;
using System.IO;
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
        public string Start(string OPPONENT_NAME, int STARTING_CHIP_COUNT, int HAND_LIMIT)
        {
            return "starttest";
        }

        private void Log(string command)
        {
            using (var sr = new StreamWriter(System.IO.File.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogCommands.txt"), FileMode.Append)))
            {
                sr.WriteLine(command);
            }
        }

        [HttpPost]
        public string Update()
        {
            return "done";
        }
    }
}
