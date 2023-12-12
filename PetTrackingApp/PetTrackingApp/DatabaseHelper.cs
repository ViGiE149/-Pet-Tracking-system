using System;
using System.Data;
using System.Data.OleDb;

public class DatabaseHelper
{
    private string connectionString;

    public DatabaseHelper(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public OleDbConnection GetConnection()
    {
        return new OleDbConnection(connectionString);
    }

    public int ExecuteScalar(string query, params OleDbParameter[] parameters)
    {
        using (OleDbConnection connection = GetConnection())
        using (OleDbCommand command = new OleDbCommand(query, connection))
        {
            command.Parameters.AddRange(parameters);
            connection.Open();
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public void ExecuteNonQuery(string query, params OleDbParameter[] parameters)
    {
        using (OleDbConnection connection = GetConnection())
        using (OleDbCommand command = new OleDbCommand(query, connection))
        {
            command.Parameters.AddRange(parameters);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    internal DataTable ExecuteDataTable(string v, OleDbParameter oleDbParameter)
    {
        throw new NotImplementedException();
    }

    internal DataTable ExecuteDataTable(string querySelectOwners, OleDbParameter[] selectOwnerParameters)
    {
        throw new NotImplementedException();
    }

    internal DataTable ExecuteDataTable(string querySelectOwners)
    {
        throw new NotImplementedException();
    }
}
