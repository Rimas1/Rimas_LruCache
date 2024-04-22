using System;

class Program
{
    static void Main(string[] args)
    {
        // self testing
        LruCache<int, int> cache = new LruCache<int, int>(6);

        cache.Put(1, 12);
        cache.Put(1, 123);
        cache.Put(2, 1234);
        cache.Put(3, 12345);
        cache.Put(4, 123456);
        cache.Put(5, 1234567);
        cache.Put(6, 12345678);

        int value;
        if (cache.Get(1, out value))
        {
            Console.WriteLine($"key 1: {value}");
        }

        cache.Put(7, 1223);

        if (!cache.Get(2, out value))
        {
            Console.WriteLine("item not found");
        }
    }
}
