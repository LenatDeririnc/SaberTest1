using System;

namespace SaberTest.ListElements
{
    public class ListNode
    {
        public ListNode Prev { get; set; }
        public ListNode Next { get; set; }
        public ListNode Rand { get; set; } // произвольный элемент внутри списка
        public string Data { get; set; }

        public ListNode()
        {
            Data = Guid.NewGuid().ToString();
        }

        public ListNode(string data)
        {
            Data = data;
        }

        public override string ToString()
        {
            const string @null = "-";
            
            var prev = Prev == null ? @null : Prev.Data;
            var next = Next == null ? @null : Next.Data;
            var rand = Rand == null ? @null : Rand.Data;
            
            return $"<{Data}, {prev}, {next}, {rand}>";
        }
    }
}