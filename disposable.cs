public class DbService : IDisposable
{
    private List<DbConnection> _connections = new List<DbConnection>();

    public void Dispose()
    {
        foreach (var dbConnection in _connections)
        {
            dbConnection.Dispose();
        }
    }
}

public class DbConnection : IDisposable
{
    public void Dispose()
    {
        //Dispose here
    }
}
