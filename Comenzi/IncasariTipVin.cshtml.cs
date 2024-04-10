using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FermaViticola.Pages.Comenzi.PretTotalModel;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Comenzi
{
    
    public class IncasariTipVinModel : PageModel
    {
        public List<InformatiiIncasariTipVin> informatiiIncasariTipVin = new List<InformatiiIncasariTipVin>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT\r\n    V.tipVin,\r\n    SUM(DC.cantitate * V.pretLitru) AS SumaPretTotal\r\nFROM Comenzi C\r\nJOIN DetaliiComanda DC ON C.idComanda = DC.idComanda\r\nJOIN Vinuri V ON DC.idVin = V.idVin\r\nGROUP BY V.tipVin;";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                informatiiIncasariTipVin.Add(new InformatiiIncasariTipVin()
                                {
                                    tipVin = sqlDataReader["tipVin"].ToString(),
                                    TotalIncasari = sqlDataReader["SumaPretTotal"].ToString(),
                                   
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

    public class InformatiiIncasariTipVin
    {
        public String tipVin;
        public String TotalIncasari;

    }
}
