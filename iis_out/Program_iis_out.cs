using SharedLib;
using System.Diagnostics;
using System.Text;

namespace iis_out
{
    public class Program_iis_out
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();
            app.UseStaticFiles();
            app.MapGet("/", (HttpContext context) => { return "Hello AspNetCore IIS-OUT\n" + MyWebUtility.GetTXTInfo(context); });
            app.Run();
        } 
    }
}
