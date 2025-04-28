using DotNetEnv;

namespace Persistence
{
    public class DBConnection
    {
        public string db_connection { get; set; }

        public DBConnection()
        {
            Env.Load(@"D:\Cursos\C sharp\Proyecto Ado.Net\Proyecto Hotel\.env");
            db_connection = Environment.GetEnvironmentVariable("db_connection");
        }

    }
}
