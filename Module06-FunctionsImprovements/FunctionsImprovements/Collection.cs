using System;

namespace FunctionsImprovements
{
    /// <summary>
    /// Provides a simple collection of things;
    /// </summary>
    public class Collection<T>
    {
        readonly T[] items;

        public Collection(T[] items)
        {
            this.items = items;
        }

        public T Find<TState>(TState state, Func<TState, T, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(state, item))
                {
                    return item;
                }
            }

            return default;
        }

        public T Find(Func<T, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return default;
        }
    }
}