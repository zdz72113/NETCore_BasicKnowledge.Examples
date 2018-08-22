using StackExchange.Redis;
using System;

namespace CacheDemo.StackExchangeRedisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost"))
            {
                IDatabase db = redis.GetDatabase();

                //var hashs = new HashEntry[] {
                //    new HashEntry("name1", "value1"),
                //    new HashEntry("name2", "value2"),
                //    new HashEntry("name3", "value3"),
                //};

                //db.HashSet("hash_key_1", hashs);

                //db.HashSet("hash_key_1", "name3", "value3.1");

                //var val = db.HashGetAll("hash_key_1");

                //foreach (var item in val)
                //{
                //    Console.WriteLine(item.Name + " : " + item.Value);
                //}

                db.ListRightPush("list_key_1", "1");
                db.ListRightPush("list_key_1", "2");
                db.ListLeftPush("list_key_1", "3");
                db.ListLeftPush("list_key_1", "3");
                var val = db.ListRange("list_key_1");
                foreach (var item in val)
                {
                    Console.WriteLine(item);
                }
            }

            Console.ReadKey();
        }
    }
}
