using Azure.Identity;
using Microsoft.Data.SqlClient;

static class Program
{
    static void Main()
    {
        const string connString = "Server=sqlmi-exos-dev.2f8fdfe3c502.database.windows.net;Authentication=Active Directory Managed Identity;User Id=05193909-dd03-463d-ab41-85db66391173;Database=Exos9300";
        const string queryString = "SELECT UserName from dbo.uUsers";

        using (SqlConnection connection = new SqlConnection(connString))
        {
            //var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "1fc6287a-e57a-40d6-8616-f09dfd98ad9f" });
            //// var token = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { ""}));
            //// connection.AccessToken = token.Token;
            //connection.Credential = credential;

            SqlCommand command = new SqlCommand(queryString, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t{0}", reader[0]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
