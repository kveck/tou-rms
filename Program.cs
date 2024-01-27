using System;
using System.Resources;
using System.Runtime.InteropServices;

namespace MigrateTOUData
{

//1. Register your DbContext class in your "Program.cs" file.

//    ```csharp
//    builder.Services.AddDbContext<touResourceDatabaseContext>(
//        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//    ```

//2. Add "ConnectionStrings" to your configuration file(secrets.json, appsettings.Development.json or appsettings.json).

//    ```json
//    {
//        "ConnectionStrings": {
//            "DefaultConnection": "Data Source=KRISXPS;Initial Catalog=touResourceDatabase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"
//        }
//    }
//    ```

    internal class Program
    {
        static void Main(string[] args)
        {
 
        }
    }
}
