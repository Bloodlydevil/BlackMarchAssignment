using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  A Basic Collection fo Data To Emmulate Priority Queue 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PriorityQueue<T> where T:Component
{
    /// <summary>
    /// A Data Holder To Bind Data With Priority
    /// </summary>
    private class DataHolder
    {
        public T Value;
        public int Priority;
    }



    private readonly List<DataHolder> Datas = new List<DataHolder>();

    public int Count => Datas.Count;

    public void Enqueue(T item, int priority)
    {
        Datas.Add(new()
        {
            Value = item,
            Priority = priority
        });
    }

    public T Dequeue()
    {
        int HighPriority = 0;
        for (int i = 1; i < Datas.Count; i++)
        {
            if (Datas[i].Priority < Datas[HighPriority].Priority)
                HighPriority = i;
        }

        var Item = Datas[HighPriority].Value;
        Datas.RemoveAt(HighPriority);
        return Item;
    }

    public bool Contains(T item)
    {
        for(int i=0;i<Datas.Count;i++)
        {
            if (Datas[i].Value.Equals (item))
                return true;
        }
        return false;
    }
}