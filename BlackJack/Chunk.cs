using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    /// <summary>
    /// Представление колоды игральных карт
    /// </summary>
    class Chunk
    {
        /// <summary>
        /// Сумма значений карт в колоде
        /// </summary>
        public int Sum;

        /// <summary>
        /// Набор карт в колоде
        /// </summary>
        public List<Card> Cards = new List<Card>();

        /// <summary>
        /// Базовая колода из 52 карт - исползуется для
        /// создания псевдослучайных игровых колод произвольнго размера
        /// </summary>
        public static Chunk Deck
        {
            get
            {
                Chunk Deck = new Chunk();
                Deck.Cards = new List<Card>();
                for (int i = 2; i <= 14; i++)
                    for (int j = 0; j <= 3; j++)
                        Deck.Cards.Add(new Card(i, j));
                return Deck;
            }
        }

        /// <summary>
        /// Базовый конструктор без параметров
        /// </summary>
        public Chunk() { }

        /// <summary>
        /// Конструктор с параметрами - сумма колоды и набор карт
        /// </summary>
        /// <param name="Sum">Сумма значений карт в колоде</param>
        /// <param name="Cards">Набор карт в колоде</param>
        public Chunk(int Sum, List<Card> Cards)
        {
            this.Sum = Sum;
            this.Cards = Cards;
        }

        /// <summary>
        /// Возвращает строковое представление колоды как набора карт
        /// </summary>
        /// <returns>Строковое представление колоды как набора карт</returns>
        public override string ToString()
        {
            string Result = "";
            foreach (Card C in Cards)
                Result += C.ToString() + ' ';
            return Result;
        }

        /// <summary>
        /// Добавление заданной карты в колоду
        /// </summary>
        /// <param name="C">Добавляемая карта</param>
        public void Add(Card C)
        {
            Cards.Add(C);
            if (C.Number == 11 && Sum > 10) Sum++;
            else Sum += C.Value;
        }

        /// <summary>
        /// Перекладывание псевдослучайной карты из одной колоды в другую
        /// (из колоды-донора карта удаляется)
        /// </summary>
        /// <param name="deck">Колода, из которой берётся карта</param>
        public void RandomPush(Chunk deck)
        {
            Random R = new Random(DateTime.UtcNow.Millisecond);
            int i = R.Next(0, deck.Cards.Count);
            Cards.Add(deck.Cards[i]);
            Sum += deck.Cards[i].Value;
            if (Cards[Cards.Count - 1].Number == 11 && Sum > 21) Sum -= 10;
            deck.Cards.RemoveAt(i);
        }

        /// <summary>
        /// Вытаскивание карты с заданным номером из колоды 
        /// (из колоды карта удаляется)
        /// </summary>
        /// <param name="index">Номер вытаскиваемой карты</param>
        /// <returns>Вытащенная карта</returns>
        public Card Pull(int index)
        {
            Card C = Cards[index];
            Cards.RemoveAt(index);
            return C;
        }

        /// <summary>
        /// Клонирование колоды
        /// </summary>
        /// <returns>Новая колода-клон</returns>
        public Chunk Clone()
        {
            Chunk Result = new Chunk();
            Result.Sum = Sum;
            Result.Cards = new List<Card>();
            for (int i = 0; i < Cards.Count; i++)
            {
                Result.Cards.Add(Cards[i]);
            }
            return Result;
        }

        /// <summary>
        /// Преобразует строковое представление колоды в эквивалентный
        /// ему объект класса Chunk
        /// </summary>
        /// <param name="S">Входная строка</param>
        /// <returns>Эквивалентный входной строке объект класса Chunk</returns>
        static public Chunk Parse(string S)
        {
            string[] MS = S.Split(' ');
            Chunk Result = new Chunk();
            for (int i = 0; i < MS.Length; i++)
            {
                Card C;
                try
                {
                    C = Card.Parse(MS[i]);
                }
                catch
                {
                    throw new Exception("Не удалось преобразовать строку");
                }
                Result.Add(C);
            }
            return Result;
        }
    }
}
