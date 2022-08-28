using GrpcGreeter.Services;

var builder = WebApplication.CreateBuilder(args);

//https://github.com/Azure/app-service-linux-docs/blob/master/HowTo/gRPC/use_gRPC_with_dotnet.md
//https://docs.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-6.0&tabs=visual-studio-code
//https://docs.microsoft.com/en-us/aspnet/core/grpc/aspnetcore?view=aspnetcore-6.0&tabs=visual-studio-code
//https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-6.0
//In order to prepare our gRPC server application to deploy to App Service, 
//we will need to configure Kestrel to listen to an additional port that only listens for plain-text HTTP/2.
// Configure Kestrel to listen on a specific HTTP port 
// In this example we're listening to port 8081, but you can use another number.
builder.WebHost.ConfigureKestrel(options => 
{ 
    // Comment out 8080 for local development, uncomment when publishing to App Service
    options.ListenAnyIP(8080); 
    options.ListenAnyIP(8081, listenOptions => 
    { 
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2; 
    }); 
});

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
