using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using SaberTest.ListElements;

namespace SaberTest.Helpers
{
    public static class Factory
    {
        public static ListRand FirstGenerate()
        {
            int length = 10;
            
            var rnd = new Random();
            
            List<ListNode> list = new List<ListNode>();
            int i;
            for (i = 0; i < length; i++)
            {
                var e = new ListNode(i.ToString());
                if (i > 0)
                {
                    var prev = list[i - 1];
                    e.Data = Guid.NewGuid().ToString();
                    e.Prev = list[i - 1];
                    prev.Next = e;
                }
                list.Add(e);
            }

            ListRand listRand = new ListRand
            {
                Head = list.First(),
                Tail = list.Last(),
                Count = list.Count
            };

            for (i = 0; i < list.Count; i++)
            {
                var randIndex = rnd.Next(list.Count);
                (list[randIndex], list[i]) = (list[i], list[randIndex]);
            }

            for (i = 0; i < list.Count; i++)
            {
                list[i].Rand = i >= list.Count - 1 ? list[0] : list[i + 1];
            }
            return listRand;
        }

        public static void Serialization(ListRand listRand)
        {
            using (var file = new FileStream(Constants.Path, FileMode.Create))
            {
                listRand.Serialize(file);
            }
        }

        public static void Deserialization(ListRand listRand)
        {
            using (var file = new FileStream(Constants.Path, FileMode.Open))
            {
                listRand.Deserialize(file);
            }

            Console.WriteLine("--------------");
            for (var element = listRand.Head; element != null; element = element.Next)
            {
                Console.WriteLine(element);
            }
        }
    }
}