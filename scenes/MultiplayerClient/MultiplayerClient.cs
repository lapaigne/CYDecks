using Godot;
using MySqlConnector;

public enum ActionMode
{
    Stand,
    More
}

public partial class MultiplayerClient : Node
{
    private ENetMultiplayerPeer peer;
    private int port;
    private string address;
    private DBConnection dbConnection;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() { }

    public void StartClient()
    {
        address = "127.0.0.1";

        port = 9999;
        peer = new ENetMultiplayerPeer();

        peer.CreateClient(address, port);
        peer.Host.Compress(ENetConnection.CompressionMode.Fastlz);
        Multiplayer.MultiplayerPeer = peer;

        GD.Print("Trying to connect...");
    }

    public void StartServer()
    {
        port = 9999;
        peer = new ENetMultiplayerPeer();

        peer.CreateServer(port);
        peer.Host.Compress(ENetConnection.CompressionMode.Fastlz);
        Multiplayer.MultiplayerPeer = peer;

        GD.Print("Server started");

        peer.PeerConnected += OnPeerConnected;
        peer.PeerDisconnected += OnPeerDisconnected;

        DBConnect();
    }

    private void DBConnect()
    {
        dbConnection = DBConnection.Instance();

        if (dbConnection.TryConnecting())
        {
            var list = GetCardsInDeck(1);
            foreach (var e in list)
            {
                GD.Print(e);
            }
        }

        // if (dbConnection.TryConnecting())
        // {
        //     var query = "SELECT * FROM cydecks_db.standard_cards;"; // query string
        //     var command = new MySqlCommand(query, dbConnection.Connection);
        //     var reader = command.ExecuteReader();

        //     while (reader.Read())
        //     {
        //         GD.Print(
        //             $"{reader.GetValue(1)}\t{reader.GetValue(2)}\t{reader.GetValue(3)}\t{reader.GetValue(4)}\t{reader.GetValue(5)}"
        //         );
        //     }
        // }
    }

    private void OnPeerConnected(long peerId)
    {
        GD.Print($"Peer {peerId} connected");
    }

    private void OnPeerDisconnected(long peerId)
    {
        GD.Print($"Peer {peerId} disconnected");
    }

    public void OnDBConnectBtnPressed()
    {
        GD.Print("Connecting to DB");
        DBConnect();
    }

    public void OnStandBtnPressed()
    {
        RpcId(1, nameof(SendAction), (int)ActionMode.Stand);
    }

    public void OnMoreBtnPressed()
    {
        RpcId(1, nameof(SendAction), (int)ActionMode.More);
    }
}
