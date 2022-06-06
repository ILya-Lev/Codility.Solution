using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Luxoft.GeneralCsTest;

//the problem is described here:
// https://coherent-event-437.notion.site/Test-Task-L2-5d79f691a8bf4255a8f3fefcc3435dfe
//problem author: Grygoriev Viacheslav
//solution author: Levandovskyi Illia
public class TestTaskL2<TElement> : IEnumerable<(string name, TElement[] data)>
{
    //question: currently the data is stored as a dictionary for convenience;
    // for the outer world it looks like a collection of tuples string+T[] - from my point of view it's convenient
    // if it does not fit the needs, a custom class could be introduced with 2 readonly properties: name+data
    private readonly Dictionary<string, TElement[]> _storage;
    
    private readonly IEqualityComparer<TElement> _elementComparer;
    public TestTaskL2(IEqualityComparer<TElement> elementComparer = null
    , StringComparer nameComparer = null)
    {
        _elementComparer = elementComparer ?? EqualityComparer<TElement>.Default;
        _storage = new Dictionary<string, TElement[]>(nameComparer ?? StringComparer.OrdinalIgnoreCase);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<(string name, TElement[] data)> GetEnumerator()
    {
        foreach (var pair in _storage)
        {
            //performance wise it should be ok as tuple creates a copy of _references_ to string and T[]
            yield return (pair.Key, pair.Value);
        }
    }

    public TElement[] this[string name] => _storage[name];

    public bool KnowsName(string name) => _storage.ContainsKey(name);

    public int CountForName(string name) => _storage.TryGetValue(name, out var data) ? data.Length : 0;

    public void Add(string name, TElement value)
    {
        //it's expected reading is more common than adding values one by one into the collection
        Add(name, new []{value});

        //it could be a fluent API, but to emphasis this method should be called rarely, it returns void
    }

    public void Add(string name, TElement[] values)
    {
        if (!_storage.ContainsKey(name))
            _storage.Add(name, values);
        else
            _storage[name] = _storage[name].Union(values, _elementComparer).ToArray();
    }

    public void Merge(TestTaskL2<TElement> source)
    {
        foreach (var (name, data) in source)
        {
            Add(name, data);
        }
    }

    public void Cut(TestTaskL2<TElement> source)
    {
        foreach (var (name, data) in source)
        {
            if (!_storage.ContainsKey(name)) 
                continue;

            var remainingItems = _storage[name].Except(data, _elementComparer).ToArray();
            if (remainingItems.Any())
                _storage[name] = remainingItems;
            else
                _storage.Remove(name);
        }
    }
}