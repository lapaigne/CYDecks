using System.Threading.Tasks;
using Godot;
using MySqlConnector;

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

        DBConnect();
    }

    private void DBConnect()
    {
        var dbConnection = DBConnection.Instance();

        if (dbConnection.TryConnecting())
        {
            var query = "SELECT * FROM cydecks_db.standard_cards;"; // query string
            var command = new MySqlCommand(query, dbConnection.Connection);
            var reader = command.ExecuteReader();

            GD.Print(dbConnection.Connection.State);

            while (reader.Read())
            {
                GD.Print(
                    $"{reader.GetValue(1)}\t{reader.GetValue(2)}\t{reader.GetValue(3)}\t{reader.GetValue(4)}\t{reader.GetValue(5)}"
                );
            }
        }
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

    public void OnDBConnectBtnPressed()
    {
        GD.Print("Connecting to DB");
        DBConnect();
    }

    public void OnStandBtnPressed()
    {
        GD.Print("хватит");
        SendAction(ActionMode.Stand);
    }

    public void OnMoreBtnPressed()
    {
        GD.Print("еще");
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
