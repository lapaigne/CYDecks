using System.Collections.Generic;
using Godot;

public enum ActionMode
{
    Stand,
    More
}

public enum MoveMode { }

partial class MultiplayerClient
{
    private ENetMultiplayerPeer peer;
    private int port;
    private string address;
    private DBConnection dbConnection;

    public int ClientId;
    public PlayerData Player;
    public PlayerData Opponent;

    /// <summary>
    /// Список всех экземпляров класса <c>Card</c>
    /// </summary>
    public List<Card> TotalList;
}
