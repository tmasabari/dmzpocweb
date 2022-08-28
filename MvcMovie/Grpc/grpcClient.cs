using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterClient;

namespace MvcMovie.Grpc
{
    public class GrpcClient
    {
        public async Task<HelloReply> GetData()
        {
            // The port number must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:7139");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                            new HelloRequest { Name = "GreeterClient" });
            return reply;
        }
    }
}