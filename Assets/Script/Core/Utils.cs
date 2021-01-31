
using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static T PickRandom<T>( this IEnumerable<T> ts)
    {
        int count = ts.Count();
        if (count == 0)
            throw new ArgumentOutOfRangeException("enum must greater then 0");
        int item  = UnityEngine.Random.Range(0, count);
        return ts.ElementAt(item);
    }
}
