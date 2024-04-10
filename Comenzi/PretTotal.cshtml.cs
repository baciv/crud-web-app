using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static FermaViticola.Pages.Comenzi.PretTotalModel;

namespace FermaViticola.Pages.Comenzi
{
    public class PretTotalModel : PageModel
    {
        public List<InformatiiPretTotal> informatiiPretTotal = new List<InformatiiPretTotal>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT\r\n    C.idComanda,\r\n    V.numeVin,\r\n    DC.cantitate * V.pretLitru AS PretTotal\r\nFROM Comenzi C\r\nJOIN DetaliiComanda DC ON C.idComanda = DC.idComanda\r\nJOIN Vinuri V ON DC.idVin = V.idVin;";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                informatiiPretTotal.Add(new InformatiiPretTotal()
                                {
                                    idComanda = sqlDataReader["idComanda"].ToString(),
                                    pretTotal = sqlDataReader["PretTotal"].ToString(),
                                    numeVin = sqlDataReader["numeVin"].ToString(),
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

        public class InformatiiPretTotal
        {
            public String idComanda;
            public String pretTotal;
            public String numeVin;

        }
    }
}
