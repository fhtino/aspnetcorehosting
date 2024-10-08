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

            builder.Services.AddHostedService<Worker1>();
            builder.Services.AddHostedService<Worker2>();

            var app = builder.Build();
            app.UseStaticFiles();
            app.MapGet("/", (HttpContext context) => { return "Hello AspNetCore IIS-IN\n" + MyWebUtility.GetTXTInfo(context); });

            app.MapGet("/bigdata", (HttpContext context) =>
            {
                string sizeS = context.Request.Query["s"];
                int size = sizeS != null ? int.Parse(sizeS) : 100;
                byte[] data = new byte[size];
                for (int i = 0; i < size; i++) { data[i] = 80; }
                //return Results.Bytes(data, "text/plain");
                //return Results.File(data, "application/octet-stream","uffa.dat");
                return size;
            });

            app.Run();



        }
    }
}
