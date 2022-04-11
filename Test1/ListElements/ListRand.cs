
using System;
using System.Collections.Generic;
using System.IO;

namespace SaberTest.ListElements
{
    public class ListRand
    {
        public ListNode Head { get; set; }
        public ListNode Tail { get; set; }
        public int Count { get; set; }

        public void Serialize(FileStream stream)
        {
            int index = 0;

            Dictionary<ListNode, int> randSearching = new Dictionary<ListNode, int>();
            Dictionary<ListNode, int> writtenElement = new Dictionary<ListNode, int>();

            using StreamWriter writer = new StreamWriter(stream);
            for (ListNode element = Head; element != null; element = element.Next)
            {
                writtenElement[element] = index;
                randSearching[element.Rand] = index;
                
                Console.WriteLine(element);
                writer.WriteLine($"{index}:Data:{element.Data}");

                if (index < Count-1)
                    writer.WriteLine($"{index}:Next:{index+1}");
                if (index > 0)
                    writer.WriteLine($"{index}:Prev:{index-1}");

                if (randSearching.ContainsKey(element))
                {
                    writer.WriteLine($"{randSearching[element]}:Rand:{index}");
                }

                if (writtenElement.ContainsKey(element.Rand))
                {
                    writer.WriteLine($"{index}:Rand:{writtenElement[element.Rand]}");
                }

                index += 1;
            }
        }

        public void Deserialize(FileStream stream)
        {
            using StreamReader writer = new StreamReader(stream);
            string line;
            
            Dictionary<int, ListNode> nodes = new Dictionary<int, ListNode>();

            while ((line = writer.ReadLine()) != null)
            {
                var args = line.Split(":");
                var index = Convert.ToInt32(args[0]);
                var name = args[1];
                var value = line.Remove(0, args[0].Length+1).Remove(0, args[1].Length+1);

                if (!nodes.ContainsKey(index)) nodes[index] = new ListNode();

                int secondIndex;
                switch (name)
                {
                    case "Data":
                        nodes[index].Data = value;
                        break;
                    case "Next":
                        secondIndex = Convert.ToInt32(value);
                        if (!nodes.ContainsKey(secondIndex)) nodes[secondIndex] = new ListNode();
                        nodes[index].Next = nodes[secondIndex];
                        break;
                    case "Prev":
                        secondIndex = Convert.ToInt32(value);
                        if (!nodes.ContainsKey(secondIndex)) nodes[secondIndex] = new ListNode();
                        nodes[index].Prev = nodes[secondIndex];
                        break;
                    case "Rand":
                        secondIndex = Convert.ToInt32(value);
                        if (!nodes.ContainsKey(secondIndex)) nodes[secondIndex] = new ListNode();
                        nodes[index].Rand = nodes[secondIndex];
                        break;
                    default:
                        throw new Exception("Unexpected parameter name");
                }
            }
            
            Head = nodes[0];
            Tail = nodes[nodes.Count-1];
            Count = nodes.Count;
        }
    }
}