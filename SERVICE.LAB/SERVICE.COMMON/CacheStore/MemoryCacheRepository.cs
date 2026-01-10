using System;
using System.Runtime.Caching;

namespace DEV.Common
{
    public sealed  class MemoryCacheRepository
    {

        private MemoryCacheRepository()
        {
        }
        static readonly ObjectCache cache = MemoryCache.Default;
        static readonly Object Insert = new object();
        static readonly Object Remove = new object();

        /// <summary>
        /// GetCacheItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static T? GetCacheItem<T>(string Key) where T : class
        {
            try
            {
                return (T)cache[Key];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// AddItem
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objValue"></param>
        /// <param name="cachedurationMins"></param>
        public static void AddItem(string Key, object objValue, int cachedurationMins)
        {
            lock (Insert)
            {
                cache.Add(Key, objValue, DateTime.Now.AddMinutes(cachedurationMins));
            }
        }
        /// <summary>
        /// AddItem
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objValue"></param>
        /// <param name="ExpiryDateTime"></param>
        public static void AddItem(string Key, object objValue, DateTime ExpiryDateTime)
        {
            lock (Insert)
            {
                cache.Add(Key, objValue, ExpiryDateTime);
            }
        }
        /// <summary>
        /// RemoveItem
        /// </summary>
        /// <param name="Key"></param>
        public static void RemoveItem(string Key)
        {
            lock (Remove)
            {
                cache.Remove(Key);
            }
        }

    }
}
