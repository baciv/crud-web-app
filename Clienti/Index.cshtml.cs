using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace FermaViticola.Pages.Clienti
{
    public class IndexModel : PageModel
    {
        public List<InformatiiClient> InformatiiClienti = new List<InformatiiClient>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {                     sqlConnection.Open();
                                   String query = "SELECT * FROM Clienti";
                                   using(SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while(sqlDataReader.Read())
                            {
                                InformatiiClienti.Add(new InformatiiClient()
                                {
                                    idClient = sqlDataReader["idClient"].ToString(),
                                    numeClient = sqlDataReader["numeClient"].ToString(),
                                    adresa = sqlDataReader["adresa"].ToString(),
                                    telefon = sqlDataReader["telefon"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class InformatiiClient
    {
        public String idClient;
        public String numeClient;
        public String adresa;
        public String telefon;

    }
}
