using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Godot;

public partial class MultiplayerClient : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override async void _Ready()
    {
        await TryConnecting();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public static async Task TryConnecting()
    {
        using TcpClient tcpClient = new TcpClient();
        GD.Print("Клиент запущен");
        await tcpClient.ConnectAsync("127.0.0.1", 8888);

        if (tcpClient.Connected)
            GD.Print($"Подключение с {tcpClient.Client.RemoteEndPoint} установлено");
        else
            GD.Print("Не удалось подключиться");
    }
}
