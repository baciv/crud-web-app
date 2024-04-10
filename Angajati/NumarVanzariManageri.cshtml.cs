using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Angajati
{
    public class NumarVanzariManageriModel : PageModel
    {
        public List<informatiiManageri> informatiiManageri = new List<informatiiManageri>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT idAngajat, numeAngajat, functie\r\nFROM Angajati\r\nWHERE functie like 'Manager%' AND idAngajat IN (SELECT DISTINCT idAngajat FROM DetaliiComanda);";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                informatiiManageri.Add(new informatiiManageri()
                                {
                                    numeAngajat = sqlDataReader["numeAngajat"].ToString(),
                                    functie = sqlDataReader["functie"].ToString(),
                                    idAngajat = sqlDataReader["idAngajat"].ToString(),
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

    public class informatiiManageri
    {
        public String numeAngajat;
        public String functie;
        public String idAngajat;
    }
}
