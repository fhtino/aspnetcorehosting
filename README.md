# Asp.Net Core hosting: notes and experiments

[Work in progress]

## Windows Asp.Net Core hosting

Check documentation for limitations, details and nuances.

| Technology | Hosting | 
| -- | -- |
| Kestrel | self hosting (console exe) |   |
| Kestrel | Windows Service (?) |  |
| http.sys | self hosting (console exe) |
| http.sys | Windows Service (?) |
| IIS-Express (VS) | in-proc | |   |
| IIS-Express (VS) | out-of-proc |  uses Kestrel |   |
| IIS (WinServer) | in-proc | | requires ANCM |
| IIS (WinServer) | out-of-proc |  uses Kestrel | requires ANCM |

ANCM = IIS AspNet Core Module. Required for hosting on IIS. During installation, it add all required stuff: Core, AspNet core, iis modules, etc.


## Visual Studio options

When running AspNet Core web-app from Visual Studio there are many options. The way the app is started is controlled by launchSetting.json profiles.

<AspNetCoreHostingModel> option in the project file influences:
 - IIS express running mode
 - Publish to Folder: web.config results with a different setting.

### Kestrel

...

### IIS Express

...

### WSL

Visual Studio starts WSL and then dotnet (Kestrel hosting) passing the dll path on the command line.

```
~$ ps -a
  PID TTY          TIME CMD
  741 pts/1    00:00:01 dotnet
  849 pts/2    00:00:00 ps
~$ cat /proc/741/cmdline
/usr/bin/dotnet/mnt/xxxxxxxxxxxxxxxxx/iis_in/bin/Debug/net8.0/iis_in.dll
```



## IIS Hosting : in / out processing

AspNet Core Web app can be hosted in-process or out-of-process. The running processes appear like as follow:

![](_imgs/iis_in_out.png)

- "svchost.exe" is hosting the whole IIS
- the second w3wp.exe is hoting the in-process web-app
- the first w3wp.exe is hosting the out-of-process web-app. Notice the dotnet.exe child process running the app inside Kestrel. IIS and Kestrel commununicate over tcp/ip 127.0.0.1 port, passed by IIS to dotnet.exe as environment variable ASPNETCORE_PORT

![](_imgs/iis_kestrel_port.png)

**WARNING:** 1 web application < - - > 1 app pool
Do not assign more than one aspnet core web-app to a single iis app-pool. Otherwise you will get error *"HTTP Error 500.35 - ASP.NET Core does not support multiple apps in the same app pool"*

**Web.config**, normally created by VS2022 during publish to Folder, contains information used by IIS to map to ANCM and which hosting to use (in/out process).

Information about how the web-app is running, can be get from C# using Process.GetCurrentProcess()



```
Hello AspNetCore IIS-IN
-----
ProcessName:    w3wp
ModuleName:     w3wp.exe
ModuleFileName: c:\windows\system32\inetsrv\w3wp.exe
-----
```

```
Hello AspNetCore IIS-OUT
-----
ProcessName:    dotnet
ModuleName:     dotnet.exe
ModuleFileName: C:\Program Files\dotnet\dotnet.exe
-----
```

## IIS Windows Authentication

In IIS, select the Application / Authentication, disable "Anonymous authentication" and enable "Windows Authentication".

![](_imgs/iis_win_auth.png)

The browser will ask for a username/password or will perform automatic integrated authentication if client PC and server are on the same or trusted domain.

![](_imgs/iis_login.png)

The username of the authenticated user is visibile in C# as usual, through HttpContext.User.Identity. This work with in-prc and out-of-proc hosted application.

```
Hello AspNetCore IIS-IN
-----
DateTimeUtc:    2024-10-06T07:46:39.2820440Z
ProcessName:    w3wp
ModuleName:     w3wp.exe
ModuleFileName: c:\windows\system32\inetsrv\w3wp.exe
User name:      WIN2022TEST\foo
User IsAuth:    True
-----
```

```
Hello AspNetCore IIS-OUT
-----
DateTimeUtc:    2024-10-06T07:46:47.6837464Z
ProcessName:    dotnet
ModuleName:     dotnet.exe
ModuleFileName: C:\Program Files\dotnet\dotnet.exe
User name:      WIN2022TEST\foo
User IsAuth:    True
-----
```



### Proxies

[TODO]


## Linux

[TODO]



## HTTPS Certificates
 
[TODO]]



## Links

Servers: Web server implementations in ASP.NET Core   
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/

Host ASP.NET Core in a Windows Service  
https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service