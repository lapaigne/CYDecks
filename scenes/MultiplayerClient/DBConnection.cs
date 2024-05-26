using MySqlConnector;

partial class DBConnection
{
    public string Server { get; set; }
    public string Database { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public MySqlConnection Connection { get; set; }

    private static DBConnection _instance = null;

    public static DBConnection Instance()
    {
        if (_instance == null)
        {
            _instance = new DBConnection();
        }
        return _instance;
    }

    public bool TryConnecting()
    {
        if (Connection == null)
        {
            if (string.IsNullOrEmpty(Database))
            {
                return false;
            }

            var connectionString = $"server={Server};uid={User};pwd={Password};database={Database}";
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }
        else if (Connection.State == System.Data.ConnectionState.Closed)
        {
            Connection.Open();
        }

        return true;
    }
}
