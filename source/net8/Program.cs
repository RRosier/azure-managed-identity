using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Managed.Identity.Configuration;

static class Program
{
    static void Main()
    {
        // string? connString = Configuration.Instance.GetConnectionString("azureManagedInstance");
        string? connString = Configuration.Instance.GetConnectionString("azureUserManagedInstance");
        const string queryString = "SELECT Name from dbo.People";

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
