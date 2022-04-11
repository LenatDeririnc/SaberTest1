using SaberTest.Helpers;

namespace SaberTest
{
    public class Program
    {
        static void Main()
        {
            var list = Factory.FirstGenerate();
            Factory.Serialization(list);
            Factory.Deserialization(list);
        }
    }
}