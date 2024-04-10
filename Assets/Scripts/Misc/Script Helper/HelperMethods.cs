using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperMethods
{
    public static List<T> Reverse<T>(this List<T> list)
    {
        List<T> res = new List<T>();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            res.Add(list[i]);
        }
        return res;
    }
}
