using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Vinuri
{
    public class IndexModel : PageModel
    {
        public List<InformatiiVin> InformatiiVinuri = new List<InformatiiVin>();


        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {                     sqlConnection.Open();
                                                  String query = "SELECT * FROM Vinuri";
                                                  using(SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while(sqlDataReader.Read())
                            {
                                InformatiiVinuri.Add(new InformatiiVin()
                                {
                                    idVin = sqlDataReader["idVin"].ToString(),
                                    numeVin = sqlDataReader["numeVin"].ToString(),
                                    anRecolta = sqlDataReader["anRecolta"].ToString(),
                                    tipVin = sqlDataReader["tipVin"].ToString(),
                                    pretLitru = sqlDataReader["pretLitru"].ToString()
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

    public class InformatiiVin
    {
        public String idVin;
        public String numeVin;
        public String anRecolta;
        public String tipVin;
        public String pretLitru;
    }
}
