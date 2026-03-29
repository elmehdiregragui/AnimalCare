namespace AnimalCareApplication.Patterns.Singleton
{
    public class Singleton
    {
        private static Singleton _instance;
        private static readonly object _lock = new object();

        public static Singleton Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                    return _instance;
                }
            }
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}