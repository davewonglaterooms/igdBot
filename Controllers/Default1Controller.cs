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
            string msg = string.Format("Opponent_Name:{0}, Starting_Chip_Count:{1}, Hand_Limit:{2}", OPPONENT_NAME, STARTING_CHIP_COUNT, HAND_LIMIT);
            Log(msg);
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
        public string Update(string COMMAND, string DATA)
        {
            Log(string.Format("Command: {0}, Data: {1}", COMMAND ?? "NoCommand", DATA ?? "NoData"));

            return "done";
        }
    }
}
