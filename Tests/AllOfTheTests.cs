using NUnit.Framework;
using igdBot.Controllers;

namespace igdBot.Tests
{
    [TestFixture]
    public class AllOfTheTests
    {
        [Test, Ignore]
        public void If_card_dealt_is_10_then_next_get_should_be_Bet_10()
        {
            var controller = new IgdController();

            controller.Update("CARD", "10");

            var response = controller.Move();

            Assert.That(response, Is.EqualTo("BET:10"));
        }

        [Test, Ignore]
        public void If_card_dealt_is_J_then_next_get_should_be_Bet_10()
        {
            var controller = new IgdController();

            controller.Update("CARD", "J");

            var response = controller.Move();

            Assert.That(response, Is.EqualTo("BET:10"));
        }

        [Test, Ignore]
        public void If_card_dealt_is_Q_then_next_get_should_be_Bet_10()
        {
            var controller = new IgdController();

            controller.Update("CARD", "Q");

            var response = controller.Move();

            Assert.That(response, Is.EqualTo("BET:10"));
        }

        [Test, Ignore]
        public void If_card_dealt_is_K_then_next_get_should_be_Bet_10()
        {
            var controller = new IgdController();

            controller.Update("CARD", "K");

            var response = controller.Move();

            Assert.That(response, Is.EqualTo("BET:10"));
        }
    }
}