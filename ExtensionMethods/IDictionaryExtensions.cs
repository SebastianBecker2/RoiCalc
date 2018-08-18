using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class IDictionaryExtensions
{
    public static TValue GetValueOrDefault<TKey, TValue> 
        (this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue defaultValue)
    {
        return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
    }
}
