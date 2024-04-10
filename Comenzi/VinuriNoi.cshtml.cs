using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Comenzi
{
    public class VinuriNoiModel : PageModel
    {
        public List<InformatiiVinuriNoi> informatiiVinuriNoi = new List<InformatiiVinuriNoi>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT V.numeVin, (\r\n    SELECT COUNT(DC.idComanda)\r\n    FROM DetaliiComanda DC\r\n    WHERE DC.idVin = V.idVin\r\n) AS NumarComenzi\r\nFROM Vinuri V\r\nWHERE V.anRecolta >= YEAR(GETDATE()) - 3;";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                informatiiVinuriNoi.Add(new InformatiiVinuriNoi()
                                {
                                    numarComenzi = sqlDataReader["numarComenzi"].ToString(),
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
    }

    public class InformatiiVinuriNoi
    {
        public String numarComenzi;
        public String numeVin;
    }
}