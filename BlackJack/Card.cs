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
    class Card
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
    }
}
