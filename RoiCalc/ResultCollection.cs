using System;
using System.Collections.Generic;

namespace RoiCalc
{
    class ResultCollection : Dictionary<Item, float>
    {
        public ResultCollection() { }
        public ResultCollection(ResultCollection rhs)
        {
            foreach (var result in rhs)
            {
                Add(result.Key, result.Value);
            }
        }

        public static ResultCollection operator +(ResultCollection lhs, ResultCollection rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            var results = new ResultCollection(lhs);

            foreach (var result in rhs)
            {
                if (results.ContainsKey(result.Key))
                {
                    results[result.Key] += result.Value;
                }
                else
                {
                    results.Add(result.Key, result.Value);
                }
            }

            return results;
        }
    }
}
