using System;

namespace RoiCalc
{
    class Calculation : IEquatable<Calculation>
    {
        public Item Item { get; }

        public int Count { get; }

        public int Interval { get; }

        public Calculation(Item item, int count, int interval)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Must be 1 or higher");
            }

            if (interval <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(interval), "Must be 1 or higher");
            }

            Item = item ?? throw new ArgumentNullException(nameof(item), "Must not be null");
            Count = count;
            Interval = interval;
        }

        public ResultCollection CalculateResults()
        {
            return CalculateResults(Item, Count, Interval);
        }

        private ResultCollection CalculateResults(Item item, int count, int interval)
        {
            var request = (float)count / interval;
            return CalculateResults(item, request);
        }

        private ResultCollection CalculateResults(Item item, float request)
        {
            var results = new ResultCollection();
            var production = (float)item.Count / item.Interval;
            var required_count = request / production;
            results.Add(item, required_count);

            foreach (var Ingredient in item.Ingredients)
            {
                var Ingredient_request = ((required_count * Ingredient.Value) / item.Interval);
                var res = CalculateResults(Ingredient.Key, Ingredient_request);
                foreach (var r in res)
                {
                    if (results.ContainsKey(r.Key))
                    {
                        results[r.Key] += r.Value;
                    }
                    else
                    {
                        results.Add(r.Key, r.Value);
                    }
                }
            }

            return results;
        }

        public bool Equals(Calculation other)
        {
            if (Item != other.Item) { return false; }
            if (Count != other.Count) { return false; }
            return Interval == other.Interval;
            
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Item, Count, Interval).GetHashCode();
        }
    }
}
