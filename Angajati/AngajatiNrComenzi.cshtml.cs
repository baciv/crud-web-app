using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Angajati
{
    public class AngajatiNrComenziModel : PageModel
    {
        public List<AngajatiNrComenzi> angajatiNrComenzi = new List<AngajatiNrComenzi>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT\r\n    A.numeAngajat,\r\n    A.functie,\r\n    COUNT(DISTINCT C.idComanda) AS NumarComenzi\r\nFROM Angajati A\r\nLEFT JOIN DetaliiComanda DC ON A.idAngajat = DC.idAngajat\r\nLEFT JOIN Comenzi C ON DC.idComanda = C.idComanda\r\nGROUP BY A.idAngajat, A.numeAngajat, A.functie;";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                angajatiNrComenzi.Add(new AngajatiNrComenzi()
                                {
                                    numeAngajat = sqlDataReader["numeAngajat"].ToString(),
                                    functie = sqlDataReader["functie"].ToString(),
                                    NumarComenzi = sqlDataReader["NumarComenzi"].ToString(),
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

    public class AngajatiNrComenzi
    {
        public String numeAngajat;
        public String functie;
        public String NumarComenzi;
    }
}
