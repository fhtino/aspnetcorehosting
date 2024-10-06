using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class MyWebUtility
    {
        public static string GetTXTInfo(HttpContext hc)
        {
            var p = Process.GetCurrentProcess();

            var sb = new StringBuilder();            
            sb.AppendLine($"-----");
            sb.AppendLine($"DateTimeUtc:    {DateTime.UtcNow.ToString("O")}");
            sb.AppendLine($"ProcessName:    {p.ProcessName}");
            sb.AppendLine($"ModuleName:     {p.MainModule?.ModuleName}");
            sb.AppendLine($"ModuleFileName: {p.MainModule?.FileName}");
            sb.AppendLine($"User name:      {hc.User?.Identity?.Name}");
            sb.AppendLine($"User IsAuth:    {hc.User?.Identity?.IsAuthenticated}");
            sb.AppendLine($"-----");

            return sb.ToString();
        }

    }
}
