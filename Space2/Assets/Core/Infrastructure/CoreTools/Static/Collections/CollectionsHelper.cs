using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Infrastructure.CoreTools.Static.Collections
{
    public static class CollectionsHelper
    {
        private static readonly Random Rnd = new Random();

        public static int[] GenerateRandomIndexesArray(int length)
        {
            int[] answer = new int[length];

            for (int i = 0; i < length; i++)
            {
                answer[i] = i;
            }

            answer = answer.OrderBy(x => Rnd.Next()).ToArray();

            return answer;
        }

        public static T RandomEnumValue<T>()
        {
            Array v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(Rnd.Next(v.Length));
        }

        public static TValue RandomValueFromDictionary<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            List<TValue> values = dict.Values.ToList();
            return values[Rnd.Next(values.Count)];
        }

        public static T RandomListValue<T>(List<T> usableSlots)
        {
            return (T)usableSlots[Rnd.Next(usableSlots.Count)];
        }
    }
}