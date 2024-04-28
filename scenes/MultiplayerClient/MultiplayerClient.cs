using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Godot;

public partial class MultiplayerClient : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override async void _Ready()
    {
        await Task.Run(async () => await TryConnecting());
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public static async Task TryConnecting()
    {
        var words = new string[] { "red", "yellow", "blue", "green" };

        using var tcpClient = new TcpClient();
        await tcpClient.ConnectAsync("5.253.62.130", 8888);
        var stream = tcpClient.GetStream();

        var response = new List<byte>();
        int bytesRead;
        foreach (var word in words)
        {
            byte[] data = Encoding.UTF8.GetBytes(word + '\n');
            await stream.WriteAsync(data);

            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                response.Add((byte)bytesRead);
            }

            var result = Encoding.UTF8.GetString(response.ToArray());
            GD.Print($"Слово {word}: {result}");
            response.Clear();
        }
        await stream.WriteAsync(Encoding.UTF8.GetBytes("END\n"));
    }
}
