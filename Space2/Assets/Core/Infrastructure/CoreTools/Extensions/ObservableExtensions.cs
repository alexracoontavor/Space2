using System;
using UniRx;

namespace Assets.Infrastructure.Assets.Infrastructure.CoreTools.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<T> SampleEvery<T>(this IObservable<T> source,
            Func<int> multipleProvider)
        {
            int counter = 0;
            Func<T, bool> predicate = ignored =>
            {
                counter++;
                if (counter >= multipleProvider())
                {
                    counter = 0;
                }
                return counter == 0;
            };
            return source.Where(predicate);
        }
    }
}