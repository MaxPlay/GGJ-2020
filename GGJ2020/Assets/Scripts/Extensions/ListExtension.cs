using System.Collections.Generic;

public static class ListExtension
{
    public static bool RemoveAtSwap<T>(this IList<T> list, int index)
    {
        if (list.Count == 0)
            return false;

        if (list.Count <= index)
            return false;

        T last = list.Last();
        list[index] = last;
        list.RemoveLast();
        return true;
    }

    public static bool RemoveSwap<T>(this IList<T> list, T obj)
    {
        int index = list.IndexOf(obj);
        return list.RemoveAtSwap(index);
    }

    public static void RemoveLast<T>(this IList<T> list)
    {
        if (list.Count == 0)
            return;

        list.RemoveAt(list.Count - 1);
    }

    public static T Last<T>(this IList<T> list)
    {
        if (list.Count == 0)
            return default;

        return list[list.Count - 1];
    }
}
