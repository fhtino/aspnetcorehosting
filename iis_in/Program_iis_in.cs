using SharedLib;
using System.Diagnostics;
using System.Text;

namespace iis_in
{
    public class Program_iis_in
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();
            app.UseStaticFiles();
            app.MapGet("/", (HttpContext context) => { return "Hello AspNetCore IIS-IN\n" + MyWebUtility.GetTXTInfo(context); });
            app.Run();
            
        }        
    }
}
