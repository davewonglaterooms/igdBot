using System;
using System.IO;
using System.Web.Mvc;
using log4net;

namespace igdBot.Controllers
{
    public class IgdController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(IgdController));

        [HttpGet]
        public string Move()
        {
            using (var sr = new StreamReader(System.IO.File.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CardStore.txt"), FileMode.Open)))
            {
                var card = "";
                card = sr.ReadLine();

                if (card.Contains("A"))
                    return "BET:200";

                if (card.Contains("10") || card.Contains("J") || card.Contains("Q") || card.Contains("K"))
                    return "BET:5";
            }

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
            switch (COMMAND)
            {
                case "RECEIVE_BUTTON":
                    break;
                case "POST_BLIND":
                    break;
                case "CARD":
                    StoreCard(DATA);
                    break;
                case "OPPONENT_MOVE":
                    break;
                case "RECEIVE_CHIPS":
                    break;
                case "OPPONENT_CARD":
                    break;
            }

            Log("Update", string.Format("Command: {0}, Data: {1}", COMMAND ?? "NoCommand", DATA ?? "NoData"));

            return "done";
        }

        private void StoreCard(string data)
        {
            using (var sr = new StreamWriter(System.IO.File.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CardStore.txt"), FileMode.Create)))
            {
                sr.WriteLine(data);
            }
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
