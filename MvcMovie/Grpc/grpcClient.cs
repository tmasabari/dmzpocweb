using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterClient;
using System.Configuration;

namespace MvcMovie.Grpc
{
    public class GrpcClient
    {
        public async Task<HelloReply> GetData()
        {
            // The port number must match the port of the gRPC server.
            var serverURL = System.Configuration.ConfigurationManager.AppSettings["GrpcServerUrl"];
            using var channel = GrpcChannel.ForAddress(serverURL);
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                            new HelloRequest { Name = "GreeterClient" });
            return reply;
        }
    }
}