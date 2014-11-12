using System;
using System.IO;
using System.Web.Mvc;
using log4net;

namespace igdBot.Controllers
{
    public class IgdController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(IgdController));

        private string BlindStorePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BlindStore.txt");
        private string CardStorePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CardStore.txt");
        private string OppoMovePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OppoMoveStore.txt");

        [HttpGet]
        public string Move()
        {
            var move = "BET";

            int oppBet = GetOppoMove();

            var blind = GetBlindValue();

            if (oppBet > 30)
                move = "FOLD";
            else
            {
                if (System.IO.File.Exists(CardStorePath))
                {
                    using (var sr = new StreamReader(System.IO.File.Open(CardStorePath,
                                                                         FileMode.Open)))
                    {
                        var card = "";
                        card = sr.ReadLine();

                        if (card == null)
                            return move;

                        if (card.Contains("A"))
                            move = "BET:30";

                        if (card.Contains("Q") || card.Contains("K"))
                            move = "BET:5";

                        int cardNum;
                        if (int.TryParse(card, out cardNum))
                        {
                            if (blind)
                            {
                                if (cardNum < 5)
                                    move = "FOLD";
                            }
                            else
                            {
                                if (cardNum < 9)
                                    move = "FOLD";
                            }

                        }
                    }
                }
            }

            logger.Info(string.Format("Move Played: {0}", move));
            return move;
        }

        private int GetOppoMove()
        {
            if (System.IO.File.Exists(OppoMovePath))
            {
                using (var sr = new StreamReader(System.IO.File.Open(OppoMovePath,
                                                                     FileMode.Open)))
                {
                    var oppMove = sr.ReadLine();

                    if (oppMove != null)
                    {
                        oppMove = oppMove.Replace("BET", "").Replace(":", "");

                        int oppBet;
                        if (int.TryParse(oppMove, out oppBet))
                        {
                            return oppBet;
                        }
                    }
                }
            }
            return 2;
        }

        private bool GetBlindValue()
        {
            var blind = false;
            if (System.IO.File.Exists(BlindStorePath))
            {
                using (var sr = new StreamReader(System.IO.File.Open(BlindStorePath,
                                                                     FileMode.Open)))
                {
                    var blindText = "";
                    blindText = sr.ReadLine();

                    if (blindText != null)
                    {
                        if (blindText.Contains("BLIND"))
                        {
                            blind = true;
                        }
                    }
                }
            }
            return blind;
        }

        [HttpPost]
        public string Start(string OPPONENT_NAME, int STARTING_CHIP_COUNT, int HAND_LIMIT)
        {
            using (var sr = new StreamWriter(System.IO.File.Open(CardStorePath, FileMode.Create)))
            {
                sr.Write("");
            }

            using (var sr = new StreamWriter(System.IO.File.Open(BlindStorePath, FileMode.Create)))
            {
                sr.Write("");
            }

            using (var sr = new StreamWriter(System.IO.File.Open(OppoMovePath, FileMode.Create)))
            {
                sr.Write("");
            }

            string msg = string.Format("Opponent_Name:{0}, Starting_Chip_Count:{1}, Hand_Limit:{2}", OPPONENT_NAME, STARTING_CHIP_COUNT, HAND_LIMIT);
            Log("start", msg);
            return "starttest";
        }

        private void Log(string action, string command)
        {
            logger.Info(command);
        }

        [HttpPost]
        public string Update(string COMMAND, string DATA)
        {
            switch (COMMAND)
            {
                case "RECEIVE_BUTTON":
                    StoreBlind(false);
                    break;
                case "POST_BLIND":
                    StoreBlind(true);
                    break;
                case "CARD":
                    StoreCard(DATA);
                    break;
                case "OPPONENT_MOVE":
                    StoreOppoMove(DATA);
                    break;
                case "RECEIVE_CHIPS":
                    break;
                case "OPPONENT_CARD":
                    break;
            }

            Log("Update", string.Format("Command: {0}, Data: {1}", COMMAND ?? "NoCommand", DATA ?? "NoData"));

            return "done";
        }

        private void StoreOppoMove(string data)
        {
            using (var sr = new StreamWriter(System.IO.File.Open(OppoMovePath, FileMode.Create)))
            {
                sr.WriteLine(data);
            }
        }

        private void StoreBlind(bool blind)
        {
            using (var sr = new StreamWriter(System.IO.File.Open(BlindStorePath, FileMode.Create)))
            {
                sr.WriteLine(blind ? "BLIND" : "");
            }
        }

        private void StoreCard(string data)
        {
            using (var sr = new StreamWriter(System.IO.File.Open(CardStorePath, FileMode.Create)))
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
