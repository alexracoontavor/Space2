using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Infrastructure.CoreTools.Extensions;

namespace Assets.Infrastructure.Components.Cards
{
    /// <summary>
    /// A tool for generating and operating cards deck data
    /// Instantiate for use
    /// </summary>
    public class CardsDeck
    {
        private readonly Random _rnd = new Random();
        private int[] _cards;
        private int _lastPulledCardIndex;

        private static Dictionary<int, int[]> _indexToValue;
        private static Dictionary<int, int[]> IndexToValue
        {
            get
            {
                if (_indexToValue != null) return _indexToValue;

                _indexToValue = new Dictionary<int, int[]>();
                var indexii = Enumerable.Range(0, 52).ToArray();
                var values = Enumerable.Range(1, 13).ToArray();
                var valCounter = 0;

                for (int i = 0; i < indexii.Length; i++)
                {
                    var val0 = valCounter > 8 ? 10 : values[valCounter];
                    var val1 = valCounter == 0 ? 11 : val0;
                    valCounter = valCounter == values.Length - 1 ? 0 : valCounter + 1;

                    _indexToValue.Add(i, new[] { val0, val1 });
                }

                return _indexToValue;
            }
        }

        /// <summary>
        /// Creates a new deck with the given potential card values
        /// </summary>
        /// <param name="valuesMap">Values map - key is card index, value is array of possible values for this card</param>
        public CardsDeck(Dictionary<int, int[]> valuesMap)
        {
            _indexToValue = valuesMap;
            Reset();
        }

        /// <summary>
        /// Creates a new deck with default values
        /// </summary>
        public CardsDeck()
        {
            Reset();
        }

        /// <summary>
        /// Returns all the possible values for given card
        /// </summary>
        /// <param name="card">card index</param>
        /// <returns>values array</returns>
        public static int[] ValuesOf(int card)
        {
            return IndexToValue.ContainsKey(card) ? IndexToValue[card] : new[] { 0 };
        }

        /// <summary>
        /// Returns cards from deck
        /// </summary>
        /// <param name="count">number of cards to pull</param>
        /// <returns>Array of pulled cards. -1 means deck ran out of cards</returns>
        public int[] PullCards(int count)
        {
            var answer = new int[count];

            for (var i = 0; i < count; i++)
            {
                answer[i] = _lastPulledCardIndex < _cards.Length ? _cards[_lastPulledCardIndex++] : -1;
            }

            return answer;
        }

        /// <summary>
        /// Generates all possible value arrays for given cards
        /// </summary>
        /// <param name="cards">card indexes array to generate values from</param>
        /// <returns>Values array</returns>
        public int[] GetValues(int[] cards)
        {
            return cards
                .Select(s => ValuesOf(s))
                .ToArray()
                .CartesianProduct()
                .Select(v => v.Sum())
                .Distinct()
                .ToArray();
        }

        private void Reset()
        {
            _lastPulledCardIndex = 0;
            _cards = Enumerable.Range(0, 52).OrderBy(x => _rnd.Next()).ToArray();
        }

        private void HandleEmpty()
        {
            //TODO - handle empty
        }
    }
}