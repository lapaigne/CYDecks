using Godot;
using MySqlConnector;

partial class MultiplayerClient
{
    public void CreateMatch(int firstPlayerId, int secondPlayerId)
    {
        var query = @$"
        INSERT INTO cydecks_db.matches (`finished`) VALUES (0);
        SET @last_id = LAST_INSERT_ID();
        INSERT INTO cydecks_db.match_players (`match_id`, `user_id`, `score`) 
        VALUES (@last_id, {firstPlayerId}, 0), (@last_id, {secondPlayerId}, 0);
        ";

        var command = new MySqlCommand(query, dbConnection.Connection);
        command.ExecuteNonQuery();
    }

    
}
