namespace Assessment
{
    internal class Program
    {
        public enum Turn
        {
            Left =1,
            Right
        }
        public class MaxSum
        {
            static async Task Print(int delay, string str)
            {
                await Task.Delay(delay);
                Console.WriteLine(str);
            }

            public static void Main(string[] args)
            {
                string name = null;
                string test = string.Empty;
                Console.WriteLine(name);
                Console.WriteLine(test);
            }
        }
    }
}