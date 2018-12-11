using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        /// <summary>
        /// Точка входа - меню и вывод результатов
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string S = "";
            while (S != "3")
            {
                S = "";
                Console.ForegroundColor = (ConsoleColor)10;
                Console.WriteLine("Карточная игра \"Black Jack\"");
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("1) Игра с виртуальным диллером");
                Console.WriteLine("2) Расчёт максимальной награды");
                Console.WriteLine("3) Выход");
                while (S != "1" && S != "2") S = Console.ReadLine();
                switch (S)
                {
                    case "1":
                        ManualPlay();
                        break;
                    case "2":
                        Console.WriteLine("Максимальная нагарда = "
                            + MaxReward.GetMaxReward(out string Way).ToString());
                        Console.WriteLine(Way);
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Игра вручную
        /// </summary>
        static void ManualPlay()
        {
            int p = 1;
            Chunk Deck = Chunk.Deck;
            while (Deck.Cards.Count > 10)
            {
                bool f1 = false, f2 = false;
                Console.WriteLine((p++).ToString() + "-я партия");
                Chunk D_Hand = new Chunk();
                D_Hand.RandomPush(Deck);
                D_Hand.RandomPush(Deck);
                Chunk P_Hand = new Chunk();
                P_Hand.RandomPush(Deck);
                P_Hand.RandomPush(Deck);
                while (P_Hand.Sum < 21 && (!f1 || !f2))
                {
                    f1 = true; f2 = true;
                    Console.Write("Карты диллера: ");
                    Console.WriteLine(D_Hand.ToString().Remove(0, 2).Insert(0, "??"));
                    Console.Write("Карты игрока: ");
                    Console.WriteLine(P_Hand.ToString());
                    Console.WriteLine("1 - \"Карту\"");
                    Console.WriteLine("Любая другая клавиша - \"Хватит\"");
                    if (Console.ReadLine() == "1")
                    {
                        P_Hand.RandomPush(Deck);
                        f1 = false;
                    }
                    if (D_Hand.Sum < 17)
                    {
                        D_Hand.RandomPush(Deck);
                        f2 = false;
                    }
                }
                Console.Write("Карты диллера: ");
                Console.WriteLine(D_Hand.ToString());
                Console.Write("Карты игрока: ");
                Console.WriteLine(P_Hand.ToString());
                if (P_Hand.Sum > 21) Console.WriteLine("Много. Выигрывает казино");
                else
                {
                    string S = "";
                    if (P_Hand.Sum >= 21 && P_Hand.Cards.Count == 2 &&
                        (P_Hand.Cards[0].Number == 11 || P_Hand.Cards[1].Number == 11))
                    {
                        P_Hand.Sum = 22;
                        S = " тоже";
                        Console.WriteLine("Black Jack!");
                    }
                    if (D_Hand.Sum >= 21 && D_Hand.Cards.Count == 2 &&
                        (D_Hand.Cards[0].Number == 11 || D_Hand.Cards[1].Number == 11))
                    {
                        D_Hand.Sum = 22;
                        Console.WriteLine("У диллера" + S + " Black Jack");
                    }
                    if (P_Hand.Sum > D_Hand.Sum) Console.WriteLine("Вы выиграли!");
                    else if (D_Hand.Sum > P_Hand.Sum)
                        Console.WriteLine("Вы проиграли");
                    else if (D_Hand.Sum == P_Hand.Sum)
                        Console.WriteLine("Ничья, все остаются при своих");
                }
                Console.WriteLine();
            }
            Console.WriteLine("В колоде кончились карты");
        }
    }
}
