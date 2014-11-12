using System;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using log4net;
using log4net.Core;

namespace igdBot.Controllers
{
    public class IgdController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(IgdController));

        [HttpGet]
        public string Move()
        {
            return "BET";
        }

        [HttpPost]
        public string Start(string OPPONENT_NAME, int STARTING_CHIP_COUNT, int HAND_LIMIT)
        {
            string msg = string.Format("Opponent_Name:{0}, Starting_Chip_Count:{1}, Hand_Limit:{2}", OPPONENT_NAME, STARTING_CHIP_COUNT, HAND_LIMIT);
            Log("start", msg);
            return "starttest";
        }

        private void Log(string action, string command)
        {
            logger.Info(command);

            using (var sr = new StreamWriter(System.IO.File.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogCommands.txt"), FileMode.Append)))
            {
                sr.WriteLine(command);
            }
        }

        [HttpPost]
        public string Update(string COMMAND, string DATA)
        {
            Log("Update", string.Format("Command: {0}, Data: {1}", COMMAND ?? "NoCommand", DATA ?? "NoData"));

            return "done";
        }

        [HttpGet]
        public JsonResult GetLogs()
        {
            var logContent = "";
            using (var sr = new StreamReader(System.IO.File.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogCommands.txt"), FileMode.Open)))
            {
                logContent = sr.ReadToEnd();
            }
            
            return new JsonResult
            {
                Data = logContent,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
