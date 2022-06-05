using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Luxoft.GeneralCsTest;

//the text is taken from here
// https://coherent-event-437.notion.site/Test-Task-L2-5d79f691a8bf4255a8f3fefcc3435dfe
//on behalf of Grygoriev Viacheslav - tech lead at Hallibutron project at Luxoft
public class TestTaskL2<TElement> : IEnumerable<(string name, TElement[] data)>
{
    //q1: do we need CI comparison? do we need culture independent comparison?
    //q2: currently the data is stored as a dictionary for convenience;
    // for the outer world it looks like a collection of tuples string+int[] - from my point of view it's convenient
    // if it does not fit the needs, a custom class could be introduced with 2 readonly properties: name+data
    private readonly Dictionary<string, TElement[]> _storage = new (StringComparer.OrdinalIgnoreCase);
    
    private readonly IEqualityComparer<TElement> _elementEqualityComparer;
    public TestTaskL2(IEqualityComparer<TElement> elementEqualityComparer = null)
    {
        _elementEqualityComparer = elementEqualityComparer ?? EqualityComparer<TElement>.Default;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<(string name, TElement[] data)> GetEnumerator()
    {
        foreach (var pair in _storage)
        {
            //performance wise it should be ok as tuple creates a copy of _references_ to string and int[]
            yield return (pair.Key, pair.Value);
        }
    }

    public TElement[] this[string name] => _storage[name];

    public bool KnowsName(string name) => _storage.ContainsKey(name);

    public int CountForName(string name) => _storage.TryGetValue(name, out var data) ? data.Length : 0;

    public void Add(string name, TElement value)
    {
        if (!_storage.ContainsKey(name))
            _storage.Add(name, new[] { value });
        else
            //it's expected reading is more common than adding values one by one into the collection
            _storage[name] = _storage[name].Union(new[] { value }, _elementEqualityComparer).ToArray();

        //it could be a fluent API, but to emphasis this method should be called rarely, it returns void
    }

    public void Add(string name, TElement[] values)
    {
        if (!_storage.ContainsKey(name))
            _storage.Add(name, values);
        else
            _storage[name] = _storage[name].Union(values, _elementEqualityComparer).ToArray();
    }

    public void Merge(TestTaskL2<TElement> source)
    {
        foreach (var (name, data) in source)
        {
            if (!_storage.ContainsKey(name))
                _storage.Add(name, data);
            else
                _storage[name] = _storage[name].Union(data, _elementEqualityComparer).ToArray();
        }
    }

    public void Cut(TestTaskL2<TElement> source)
    {
        foreach (var (name, data) in source)
        {
            if (!_storage.ContainsKey(name)) 
                continue;

            var remainingItems = _storage[name].Except(data, _elementEqualityComparer).ToArray();
            if (remainingItems.Any())
                _storage[name] = remainingItems;
            else
                _storage.Remove(name);
        }
    }
}