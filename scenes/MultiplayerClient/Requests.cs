using System.Collections.Generic;
using System.Data;
using Godot;
using MySqlConnector;

partial class MultiplayerClient
{
    public void CreateMatch(int firstPlayerId, int secondPlayerId)
    {
        if (firstPlayerId == secondPlayerId)
        {
            return;
        }

        var query =
            @$"
        INSERT INTO cydecks_db.matches (`finished`) VALUES (0);
        SET @last_id = LAST_INSERT_ID();
        INSERT INTO cydecks_db.match_players (`match_id`, `user_id`, `score`) 
        VALUES (@last_id, {firstPlayerId}, 0), (@last_id, {secondPlayerId}, 0);
        ";

        var command = new MySqlCommand(query, dbConnection.Connection);
        command.ExecuteNonQuery();
    }

    public List<int> GetCardsInDeck(int deckId)
    {
        var deck = new List<int>();

        var query =
            @$"
        SELECT card_type_id FROM cydecks_db.deck_cards WHERE deck_id = {deckId};
        ";

        var command = new MySqlCommand(query, dbConnection.Connection);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            deck.Add(reader.GetInt32(0));
        }

        return deck;
    }

    public List<int> GetDecks(int playerId)
    {
        var deck = new List<int>();

        var query =
            @$"
        SELECT id FROM cydecks_db.decks WHERE owner_id = {playerId};
        ";

        var command = new MySqlCommand(query, dbConnection.Connection);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            deck.Add(reader.GetInt32(0));
        }

        return deck;
    }
}
