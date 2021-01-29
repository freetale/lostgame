
using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static T PickRandom<T>( this IEnumerable<T> ts)
    {
        int count = ts.Count();
        if (count == 0)
            throw new ArgumentOutOfRangeException("enum must greater then 0");
        int item  = UnityEngine.Random.Range(0, count);
        return ts.ElementAt(item);
    }
}
