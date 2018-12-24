using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJack;
using static BlackJack.MaxReward;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Тест на колоде, где максимально можно только свести вничью
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            string eWay = "SSHSSS";
            double eResult = 0;
            Chunk C = Chunk.Parse("6♥ 9♣ K♠ 2♣ 8♠ 6♠ 5♦ Q♠ Q♥ A♦ J♦ J♣" +
                " 8♥ K♣ 10♠ Q♣ 4♥ 6♦ 5♠ A♣ 10♣ 8♣ 6♣ 2♠ 10♦");
            double Result = Testing(C, out string Way);
            Assert.AreEqual(eWay, Way);
            Assert.AreEqual(eResult, Result);
        }

        /// <summary>
        /// Тест на проигрышной колоде
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            string eWay = "SSSSSS";
            double eResult = -2;
            Chunk C = Chunk.Parse("A♣ 5♠ 3♣ 7♦ 10♦ 6♣ 8♣ Q♥ Q♦ 10♣ 10♠" +
                " 5♦ 8♦ 4♥ Q♠ Q♣ 2♠ 6♠ A♦ K♦ J♣ 7♥ 3♠ 9♣ 5♣");
            double Result = Testing(C, out string Way);
            Assert.AreEqual(eWay, Way);
            Assert.AreEqual(eResult, Result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string eWay = "HSHSSSH";
            double eResult = 1;
            Chunk C = Chunk.Parse("4♥ K♣ 7♣ 5♠ 5♣ 8♠ 2♣ 7♥ J♠ 6♠ Q♦ 9♥" +
                " Q♠ A♣ 3♦ 5♥ 9♦ A♥ A♠ 9♣ 10♦ 4♣ K♥ 4♦ 3♥");
            double Result = Testing(C, out string Way);
            Assert.AreEqual(eWay, Way);
            Assert.AreEqual(eResult, Result);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string eWay = "SSSSHH";
            double eResult = -1;
            Chunk C = Chunk.Parse("10♣ 2♠ 10♣ 9♦ K♥ 4♥ 7♣ 9♥ K♥ A♠ 4♥ " +
                "A♣ 5♠ 3♣ 7♦ 10♦ 6♣ 8♣ Q♥ Q♦ 10♣ 10♠ 5♥");
            double Result = Testing(C, out string Way);
            Assert.AreEqual(eWay, Way);
            Assert.AreEqual(eResult, Result);
        }
    }
}
