using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Angajati
{
    public class AngajatiSalariiMariModel : PageModel
    {
        public List<AngajatiSalariiMari> angajatiSalariiMari = new List<AngajatiSalariiMari>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT COUNT(DISTINCT A.idAngajat) AS NumarAngajati\r\nFROM Angajati A\r\nJOIN DetaliiComanda DC ON A.idAngajat = DC.idAngajat\r\nJOIN Comenzi C ON DC.idComanda = C.idComanda\r\nWHERE A.salariu > (SELECT AVG(salariu) FROM Angajati)\r\n   AND DC.cantitate >= 5;";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                angajatiSalariiMari.Add(new AngajatiSalariiMari()
                                {
                                    numarAngajati = sqlDataReader["numarAngajati"].ToString(),
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

    public class AngajatiSalariiMari
    {
        public String numarAngajati;
    }
}
