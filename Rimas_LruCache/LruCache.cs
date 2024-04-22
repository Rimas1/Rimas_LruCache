using System.Collections.Concurrent;
public class LruCache<TK, TV>
{
    /// <summary>
    /// LRUCache
    /// Time taken: 4hrs
    /// </summary>

    private readonly int _capacity;
    private readonly LinkedList<(TK key, TV value)> _cacheLinkedList;
    private readonly ConcurrentDictionary<TK, LinkedListNode<(TK key, TV value)>> _cacheDict;
    private readonly object _padlock = new object();

    public LruCache(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException("Cache capacity is less than or equal to 0");
        }
        _capacity = capacity;
        _cacheLinkedList = new LinkedList<(TK key, TV value)>();
        _cacheDict = new ConcurrentDictionary<TK, LinkedListNode<(TK key, TV value)>>();
    }

    public void Put(TK key, TV value)
    {
        lock (_padlock)
        {
            // if the item exists already, we remove it from the list and add it back, 
            //so that most recently used items are at the front of the list.
            if (_cacheDict.TryGetValue(key, out var node))
            {
                _cacheLinkedList.Remove(node);
            }
            // if the cache is full and item doesnt not exist already, then we remove the LRU from the cache
            else if (_cacheLinkedList.Count >= _capacity)
            {
                Remove();
            }

            node = new LinkedListNode<(TK key, TV value)>((key, value));
            _cacheLinkedList.AddFirst(node);
            _cacheDict[key] = node;

        }
    }

    public bool Get(TK key, out TV value)
    {
        lock (_padlock)
        {
            // if item exists we move the node to the front as it was used.
            if (_cacheDict.TryGetValue(key, out var node))
            {
                _cacheLinkedList.Remove(node);
                _cacheLinkedList.AddFirst(node);
                value = node.Value.value;
                return true;
            }

            value = default; 
            return false;
        }
    }

    public void Remove()
    {
        var Lru = _cacheLinkedList.Last;
        if (Lru != null)
        {
            _cacheLinkedList.RemoveLast();
            _cacheDict.TryRemove(Lru.Value.key, out _);
            //// here you can then notify the consumer that an item was evicted.
        }
    }
}

