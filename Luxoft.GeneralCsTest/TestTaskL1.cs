using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Luxoft.GeneralCsTest;

//the text is taken from here
// https://www.notion.so/Test-Task-L1-e8e8891dda82467b83da37c64e7c0b28
//on behalf of Grygoriev Viacheslav - tech lead at Hallibutron project at Luxoft
public class TestTaskL1 : IEnumerable<(string name, int[] data)>
{
    //q1: do we need CI comparison? do we need culture independent comparison?
    //q2: currently the data is stored as a dictionary for convenience;
    // for the outer world it looks like a collection of tuples string+int[] - from my point of view it's convenient
    // if it does not fit the needs, a custom class could be introduced with 2 readonly properties: name+data
    private readonly Dictionary<string, int[]> _storage = new (StringComparer.OrdinalIgnoreCase);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<(string name, int[] data)> GetEnumerator()
    {
        foreach (var pair in _storage)
        {
            //performance wise it should be ok as tuple creates a copy of _references_ to string and int[]
            yield return (pair.Key, pair.Value);
        }
    }

    public int[] this[string name] => _storage[name];

    public bool KnowsName(string name) => _storage.ContainsKey(name);

    public int CountForName(string name) => _storage.TryGetValue(name, out var data) ? data.Length : 0;

    public void Add(string name, int value)
    {
        if (!_storage.ContainsKey(name))
            _storage.Add(name, new[] { value });
        else
            //it's expected reading is more common than adding values one by one into the collection
            _storage[name] = _storage[name].Union(new[] { value }).ToArray();

        //it could be a fluent API, but to emphasis this method should be called rarely, it returns void
    }

    public void Add(string name, int[] values)
    {
        if (!_storage.ContainsKey(name))
            _storage.Add(name, values);
        else
            _storage[name] = _storage[name].Union(values).ToArray();
    }

    public void Merge(TestTaskL1 source)
    {
        foreach (var (name, data) in source)
        {
            if (!_storage.ContainsKey(name))
                _storage.Add(name, data);
            else
                _storage[name] = _storage[name].Union(data).ToArray();
        }
    }

    public void Cut(TestTaskL1 source)
    {
        foreach (var (name, data) in source)
        {
            if (!_storage.ContainsKey(name)) 
                continue;

            var remainingItems = _storage[name].Except(data).ToArray();
            if (remainingItems.Any())
                _storage[name] = remainingItems;
            else
                _storage.Remove(name);
        }
    }
}