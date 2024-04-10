using FermaViticola.Pages.Vinuri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Comenzi
{
    public class VinuriCeDepasescSumaModel : PageModel
    {
        public String limita;
        public List<InformatiiVinuriCeDepasescSuma> infoVinCeDepSuma = new List<InformatiiVinuriCeDepasescSuma>();
        public void OnGet()
        {
        }

        public void OnPost()
        {
            limita = Request.Form["limita"];

            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT V.numeVin, SUM(DC.cantitate * V.pretLitru) AS SumaPretTotal\r\nFROM DetaliiComanda DC\r\nJOIN Vinuri V ON DC.idVin = V.idVin\r\nGROUP BY V.numeVin\r\nHAVING SUM(DC.cantitate * V.pretLitru) > " + limita.ToString() +";";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                infoVinCeDepSuma.Add(new InformatiiVinuriCeDepasescSuma()
                                {
                                    numeVin = sqlDataReader["numeVin"].ToString(),
                                    SumaPretTotal = sqlDataReader["SumaPretTotal"].ToString(),
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
    public class InformatiiVinuriCeDepasescSuma
    {
        public String numeVin;
        public String SumaPretTotal;
    }
}
