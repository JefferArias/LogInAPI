namespace LogInAPI
{
    public static class Connection
    {
        public static string GetConnectionString()
        {
            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return conf.GetConnectionString("LogIn");
        }
    }
}
