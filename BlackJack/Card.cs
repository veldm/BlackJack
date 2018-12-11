using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    /// <summary>
    /// Представление игральной карты
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Кодировка номинала
        /// </summary>
        public int Number;

        /// <summary>
        /// Масть (0 - ♠, 1 - ♣, 2 - ♥, 3 - ♦)
        /// </summary>
        public int Suit;

        /// <summary>
        /// Значение
        /// </summary>
        public int Value
        {
            get
            {
                if (Number <= 11) return Number;
                else return 10;
            }
        }

        /// <summary>
        /// Конструктор с параметрами (масть и номинал)
        /// </summary>
        /// <param name="Number">Кодировка номинала</param>
        /// <param name="Suit">Масть (0 - ♠, 1 - ♣,
        /// 2 - ♥, 3 - ♦)</param>
        public Card(int Number, int Suit)
        {
            this.Suit = Suit;
            this.Number = Number;
        }

        /// <summary>
        /// Базовый конструктор без параметров
        /// </summary>
        public Card() { }

        /// <summary>
        /// Возвращает строковое представление карты
        /// </summary>
        /// <returns>Строковое представление карты</returns>
        public override string ToString()
        {
            string S = "";
            if (Number < 11) S += Number.ToString();
            else switch (Number)
                {
                    case 11:
                        S += 'A';
                        break;
                    case 12:
                        S += 'J';
                        break;
                    case 13:
                        S += 'Q';
                        break;
                    case 14:
                        S += 'K';
                        break;
                }
            switch (Suit)
            {
                case 0:
                    S += (char)9824;
                        break;
                case 1:
                    S += (char)9827;
                    break;
                case 2:
                    S += (char)9829;
                    break;
                case 3:
                    S += (char)9830;
                    break;
            }
            return S;
        }

        /// <summary>
        /// Преобразует строковое представление карты в эквивалентный
        /// ему объект класса Card
        /// </summary>
        /// <param name="MS">Входная строка</param>
        /// <returns>Эквивалентный входной строке объект класса Card</returns>
        static public Card Parse(string MS)
        {
            int suit = 10;
            switch (MS.Substring(MS.Length - 1))
            {
                case "♠":
                    suit = 0;
                    break;
                case "♣":
                    suit = 1;
                    break;
                case "♥":
                    suit = 2;
                    break;
                case "♦":
                    suit = 3;
                    break;
                default:
                    throw new Exception("Неправильно задана масть карты");
            }
            Card C = new Card();
            if (int.TryParse(MS.Substring(0, 2), out int p))
                C = new Card(p, suit);
            else if (int.TryParse(MS[0].ToString(), out int h))
                C = new Card(h, suit);
            else if (char.IsUpper(MS[0])) switch (MS[0])
                {
                    case 'A':
                        C = new Card(11, suit);
                        break;
                    case 'J':
                        C = new Card(12, suit);
                        break;
                    case 'Q':
                        C = new Card(13, suit);
                        break;
                    case 'K':
                        C = new Card(14, suit);
                        break;
                }
            else throw new Exception("Неправильно задан номинал карты");
            return C;
        }
    }
}
