using DotNetEnv;

namespace Persistence
{
    public class DBConnection
    {
        //Declarado como protected para evitar acceso a la db
        protected string db_connection { get; set; } // ✅


        public DBConnection()
        {
            Env.Load(@"D:\Cursos\C sharp\Proyecto Ado.Net\Proyecto Hotel\.env");
            db_connection = Environment.GetEnvironmentVariable("db_connection");
        }

    }
}
