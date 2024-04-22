namespace Test_Rimas_LruCache
{
    public class Tests
    {
        [Test]
        public void LruCacheAddGetTest()
        {
            LruCache<int, int> lruCache = new LruCache<int, int>(2);
            lruCache.Put(1, 1);
            lruCache.Put(2, 1235445);

            lruCache.Get(1, out int value);
            Assert.That(value, Is.EqualTo(1));
            lruCache.Get(2, out value);
            Assert.That(value, Is.EqualTo(1235445));

        }

        [Test]
        public void LruCacheAddGetTest2()
        {
            LruCache<int, string> lruCache = new LruCache<int, string>(2);
            lruCache.Put(1, "1");
            lruCache.Put(2, "1235445");

            lruCache.Get(1, out string value);
            Assert.That(value, Is.EqualTo("1"));
            lruCache.Get(2, out value);
            Assert.That(value, Is.EqualTo("1235445"));

        }

        [Test]
        public void LruCacheEvictTest()
        {
            LruCache<int, string> lruCache = new LruCache<int, string>(2);
            lruCache.Put(1, "1");
            lruCache.Put(2, "1235445");
            lruCache.Put(3, "123");

            // when adding key 3 it should evict key 1

            lruCache.Get(1, out string value);
            Assert.That(value, Is.EqualTo(null));
        }

        [Test]
        public void LruCacheEvictLRUTest()
        {
            LruCache<int, string> lruCache = new LruCache<int, string>(2);
            lruCache.Put(1, "1");
            lruCache.Put(2, "1235445");
            lruCache.Put(1, "1");
            lruCache.Put(3, "123");

            // when adding key 3 it should evict key 2 as key 1 was used again, therefore key 2 is now LRU

            lruCache.Get(2, out string value);
            Assert.That(value, Is.EqualTo(null));
        }

        [Test]
        public void LruCacheExceptionsTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LruCache<int, int>(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LruCache<int, int>(-10));
            Assert.DoesNotThrow(() => new LruCache<int, int>(1));
        }
    }
}