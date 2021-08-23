using Microsoft.Extensions.Hosting;

namespace DatabaseTasks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .Build()
                .Run();
        }
    }
}