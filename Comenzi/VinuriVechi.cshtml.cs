using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FermaViticola.Pages.Comenzi.PretTotalModel;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Comenzi
{
    public class VinuriVechiModel : PageModel
    {
        public List<InformatiiVinuriVechi> informatiiVinuriVechi = new List<InformatiiVinuriVechi>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT C.idComanda, V.numeVin, V.anRecolta\r\nFROM Comenzi C\r\nJOIN DetaliiComanda DC ON C.idComanda = DC.idComanda\r\nJOIN Vinuri V ON DC.idVin = V.idVin\r\nWHERE V.anRecolta < YEAR(GETDATE()) - 5;";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                informatiiVinuriVechi.Add(new InformatiiVinuriVechi()
                                {
                                    idComanda = sqlDataReader["idComanda"].ToString(),
                                    numeVin = sqlDataReader["numeVin"].ToString(),
                                    anRecolta = sqlDataReader["anRecolta"].ToString(),
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

    public class InformatiiVinuriVechi
    {
        public String idComanda;
        public String numeVin;
        public String anRecolta;
    }
}
