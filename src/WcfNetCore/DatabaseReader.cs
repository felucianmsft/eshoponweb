using System;  
using System.Data.SqlClient;  

public class DatabaseReader  
{  
    private SqlConnection connection;  
  
    public DatabaseReader(string connectionString)  
    {  
        connection = new SqlConnection(connectionString);  
        connection.Open();  
    }  
  
    public void ReadData()  
    {  
        string query = "SELECT * FROM Customers;";  
        SqlCommand command = new SqlCommand(query, connection);  
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())  
        {  
            for (int i = 0; i < reader.FieldCount; i++)  
            {  
                Console.Write($"{reader.GetName(i)}: {reader.GetValue(i)}");  
            }  
            Console.WriteLine();  
        }  

        return order.OrderId;
    }  
}  