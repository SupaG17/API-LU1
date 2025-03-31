/*using Microsoft.Data.SqlClient;

string connectionString ="Server=tcp:avansict2229187.database.windows.net,1433;Initial Catalog=db2229187;Persist Security Info=False;User ID=SupaGmin;Password=Ado-170306;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;";

using(var connection = new SqlConnection(connectionString)){
    connection.Open();

    using (var command = connection.CreateCommand()){
        command.CommandText = "Insert into [dbo].[UserLU1] (id, Email, ConnectionToken, UserName) values(@id, @Email, @ConnectionToken, @UserName)";
        
        command.Parameters.AddWithValue("@id", 3);
        command.Parameters.AddWithValue("@Email", "helppls@email.com");
        command.Parameters.AddWithValue("@ConnectionToken", "aahaaahaaaa");
        command.Parameters.AddWithValue("@UserName", "testperson");

        command.ExecuteNonQuery();
    }
    
    using(SqlCommand command =connection.CreateCommand()){
        command.CommandText= "SELECT * FROM [UserLU1]";
        using(SqlDataReader reader = command.ExecuteReader()){
            reader.Read();
            Console.WriteLine($"{reader.GetString(1)}");
        }
    }


    Console.WriteLine("database connection geopend");

    Console.WriteLine(connection.Database);

    connection.Close();
}
