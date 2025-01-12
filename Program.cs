using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener? server = null;
try
{
    //Definindo porta e ip  para comunicação TCP
    Int32 port = 22;
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

    server = new TcpListener(localAddr, port);
    Console.Write($"IP:{localAddr} PORT:{port}\n");

    // Inicia listagem de clientes
    server.Start();

    // Buffer de dados
    Byte[] bytes = new Byte[256];
    String? data = null;

    while (true)
    {
        Console.Write("Waiting for a connection... ");

        using TcpClient client = await server.AcceptTcpClientAsync();
        // Obtem stream para leitura e escrita
        NetworkStream stream = client.GetStream();

        data = null;

        int i;

        while ((i = await stream.ReadAsync(bytes)) != 0)
        {
            // Traduzindo data bytes para ASCII string.
            data = Encoding.ASCII.GetString(bytes, 0, i);
            Console.WriteLine("Received: {0}", data);

            if (!string.IsNullOrEmpty(data))
            {
                // Processa os dados para o Client
                byte[] msgCallBackServer = Encoding.ASCII.GetBytes(string.Concat(Guid.NewGuid().ToString(), " - ", data.ToUpper()));

                // Envia escrita das informação no response.
                await stream.WriteAsync(msgCallBackServer.AsMemory(0, msgCallBackServer.Length));
                //await sslStream.WriteAsync(msg);
            }

            Console.WriteLine("Sent: {0}", data);
        }
    }
}
catch (SocketException e)
{
    Console.WriteLine("SocketException: {0}", e);
}
finally
{
    server?.Stop();
}

Console.WriteLine("\nHit enter to continue...");
Console.Read();
