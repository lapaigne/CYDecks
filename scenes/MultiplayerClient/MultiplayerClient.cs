using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Godot;

public enum ActionMode
{
    Stand,
    More
}

public partial class MultiplayerClient : Node
{
    [Export]
    public bool isServer;
    private ENetMultiplayerPeer peer;
    private int port;
    private string address;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (isServer)
        {
            StartServer();
        }
        else
        {
            StartClient();
        }
    }

    private void StartClient()
    {
        peer = new ENetMultiplayerPeer();
        port = 9999;
        address = "127.0.0.1";

        peer.CreateClient(address, port);
        peer.Host.Compress(ENetConnection.CompressionMode.Fastlz);
        Multiplayer.MultiplayerPeer = peer;

        GD.Print("Trying to connect...");
    }

    private void StartServer()
    {
        port = 9999;
        peer = new ENetMultiplayerPeer();

        peer.CreateServer(port);
        peer.Host.Compress(ENetConnection.CompressionMode.Fastlz);
        Multiplayer.MultiplayerPeer = peer;

        GD.Print("Server started");

        peer.PeerConnected += OnPeerConnected;
        peer.PeerDisconnected += OnPeerDisconnected;
    }

    private void OnPeerConnected(long peerId)
    {
        GD.Print($"Peer {peerId} connected");
    }

    private void OnPeerDisconnected(long peerId)
    {
        GD.Print($"Peer {peerId} disconnected");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public void OnStandBtnPressed()
    {
        GD.Print("ENUF!!!");
        SendAction(ActionMode.Stand);
    }

    public void OnMoreBtnPressed()
    {
        GD.Print("MOAR!!1");
        SendAction(ActionMode.More);
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
    private void SendAction(ActionMode action)
    {
        if (isServer)
        {
            GD.Print($"data is {action}");
        }
        else
        {
            GD.Print($"Sending data: action: {action}\t");
            RpcId(1, nameof(SendAction), (int)action); //sends data only to server
        }
    }
}
