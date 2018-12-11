using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    /// <summary>
    /// Статический класс, содержащий "функцию BJ" и вспомогательные методы
    /// </summary>
    public static class MaxReward
    {
        /// <summary>
        /// Награды для всех возможных исходов всех партий
        /// </summary>
        static private List<Double> Reward = new List<Double>();

        /// <summary>
        /// Последовательность шагов для каждого из вариантов
        /// (H - "hit", S - "stay")
        /// </summary>
        static private List<string> WayToReward = new List<string>();

        /// <summary>
        /// Колода, на которой происходит игра
        /// </summary>
        static private Chunk PlayingDeck = new Chunk();

        /// <summary>
        /// "Рука" игрока
        /// </summary>
        static private Chunk P_Hand = new Chunk();

        /// <summary>
        /// "Рука" диллера
        /// </summary>
        static private Chunk D_Hand = new Chunk();

        /// <summary>
        /// Вывод максимально возможной итоговой награды
        /// на псевдослучайно сформированной колоде
        /// </summary>
        /// <param name="Way">Итоговый путь к максимальной награде</param>
        /// <returns>Максимальная итоговая награда</returns>
        static public double GetMaxReward(out string Way)
        {
            RunUp();
            Play(PlayingDeck, false, false, 0, "");
            Double Result = Reward.Max();
            Way = WayToReward[Reward.IndexOf(Result)];
            return Result;
        }

        /// <summary>
        /// Метод для тестирования расчёта максимальной награды
        /// на небольших колодах с известным результатом
        /// </summary>
        /// <param name="TestDeck">Тествая колода</param>
        /// <param name="Way">Итоговый путь к максимальной награде</param>
        /// <returns>Максимальная итоговая награда</returns>
        static public Double Testing(Chunk TestDeck, out string Way)
        {
            P_Hand.Add(TestDeck.Pull(0));
            P_Hand.Add(TestDeck.Pull(0));
            D_Hand.Add(TestDeck.Pull(0));
            Play(TestDeck, false, false, 0, "");
            Double Result = Reward.Max();
            Way = WayToReward[Reward.IndexOf(Result)];
            return Result;
        }

        /// <summary>
        /// Создание игровой колоды с псевдослучайным
        /// распределением карт и первая раздача
        /// </summary>
        static private void RunUp()
        {
            Reward = new List<Double>();
            WayToReward = new List<string>();
            PlayingDeck = new Chunk();
            P_Hand = new Chunk();
            D_Hand = new Chunk();
            Console.WriteLine("Введите количество карт");
            if (!int.TryParse(Console.ReadLine(), out int K))
            {
                Console.WriteLine("Неверно задано количество карт" + (char)13);
                GetMaxReward(out string Way);
            }
            int k = K / 52;
            if (K % 52 > 0) k++;
            Random R = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < k; i++)
            {
                Chunk Deck = Chunk.Deck;
                for (int j = 0; j < 52 && PlayingDeck.Cards.Count < K; j++)
                {
                    int f = R.Next(0, Deck.Cards.Count);
                    PlayingDeck.Add(Deck.Pull(f));
                }
            }
            if (PlayingDeck.Cards.Count <= 25)
                Console.WriteLine(PlayingDeck.ToString());
            P_Hand.Add(PlayingDeck.Pull(0));
            P_Hand.Add(PlayingDeck.Pull(0));
            D_Hand.Add(PlayingDeck.Pull(0));
        }

        /// <summary>
        /// Рекурсивный метод, "просматривающий" все
        /// вероятные исходы партии на заданной колоде
        /// </summary>
        /// <param name="Deck">Предопределённая колода</param>
        /// <param name="f1">Флаг "Stay" дилера</param>
        /// <param name="f2">Флаг "Stay" игрока</param>
        /// <param name="k">Награда на данный момент</param>
        /// <param name="Way">История ходовна данный момент</param>
        static private void Play(Chunk Deck, bool f1, bool f2, int k, string Way)
        {
            // Если и диллер, и игрок больше не берут карты - "считаемся"
            if (f1 && f2)
            {
                k += Counting();
                P_Hand = new Chunk();
                D_Hand = new Chunk();
                P_Hand.Add(Deck.Pull(0));
                P_Hand.Add(Deck.Pull(0));
                D_Hand.Add(Deck.Pull(0));
            }
            f1 = f2 = true;
            // Если карты в колоде подошли к концу - записываем награду и
            // истрию ходов в соответствующие массивы, обнуляем эти значения и 
            // возвращаемся на шаг назад по дереву решений
            if (Deck.Cards.Count < 13)
            {
                Reward.Add(k);
                WayToReward.Add(Way);
                k = 0;
                Way = "";
                return;
            }
            // пока сумма "руки" диллера менше 17 - диллер берёт карты
            if (D_Hand.Sum < 17)
            {
                D_Hand.Add(Deck.Pull(0));
                f1 = false;
            }
            // Пока у игрока меньше 21 он может взять карту или
            // оставить всё как есть
            if (P_Hand.Sum <= 21)
            {
                // Резервирование колоды и "рук"
                Card[] C = new Card[Deck.Cards.Count];
                Deck.Cards.CopyTo(C);
                Chunk NewDeck = new Chunk(Deck.Sum, C.ToList<Card>());
                Chunk[] Reserve = new Chunk[2];
                Reserve[0] = P_Hand.Clone();
                Reserve[1] = D_Hand.Clone();
                Play(NewDeck, f1, f2, k, (Way + 'S')); // Вариант "stay"
                P_Hand = Reserve[0];
                D_Hand = Reserve[1];
                try { P_Hand.Add(Deck.Pull(0)); }
                catch { throw new Exception("Закончилась колода"); }
                f2 = false;
                C = new Card[Deck.Cards.Count];
                Deck.Cards.CopyTo(C);
                NewDeck = new Chunk(Deck.Sum, C.ToList<Card>());
                Play(NewDeck, f1, f2, k, (Way + 'H')); // Вариант "hit"
            }
        }

        /// <summary>
        /// Подсчитывает награду или проигрыш игрока в текущей партии
        /// </summary>
        /// <returns>Награда/проигрыш</returns>
        static private int Counting()
        {
            if (P_Hand.Sum > 21) return -1;
            if (P_Hand.Sum >= 21 && P_Hand.Cards.Count == 2 &&
                (P_Hand.Cards[0].Number == 11 || P_Hand.Cards[1].Number == 11))
                P_Hand.Sum = 22;
            if (D_Hand.Sum >= 21 && D_Hand.Cards.Count == 2 &&
                (D_Hand.Cards[0].Number == 11 || D_Hand.Cards[1].Number == 11))
                D_Hand.Sum = 22;
            if (P_Hand.Sum == 22 && D_Hand.Sum < 22) return (3 / 2);
            if (P_Hand.Sum > D_Hand.Sum) return 1;
            if (P_Hand.Sum < D_Hand.Sum) return -1;
            return 0;
        }
    }
}
