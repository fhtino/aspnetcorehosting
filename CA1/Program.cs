namespace CA1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            while (true)
            {
                Console.WriteLine("data...");

                int size = 90 * 1000 * 1000;
                byte[] data = new byte[size];
                for (int i = 0; i < size; i++) { data[i] = 80; }
                Console.WriteLine(data.Length.ToString());

                Console.ReadKey();
            }
        }
    }
}
