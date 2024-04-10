using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FermaViticola.Pages.Comenzi.PretTotalModel;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Comenzi
{
    public class NrComenziClientModel : PageModel
    {
        public List<NrComenziClient> nrComenziClient = new List<NrComenziClient>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT Cl.numeClient, NumarComenzi\r\nFROM Clienti Cl\r\nJOIN (SELECT idClient, COUNT(*) AS NumarComenzi FROM Comenzi GROUP BY idClient) C\r\nON Cl.idClient = C.idClient;";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                nrComenziClient.Add(new NrComenziClient()
                                {
                                    numeClient = sqlDataReader["numeClient"].ToString(),
                                    numarComenzi = sqlDataReader["numarComenzi"].ToString(),
                                    
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class NrComenziClient
    {
        public String numeClient;
        public String numarComenzi;
    }
}
