using FermaViticola.Pages.Vinuri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Angajati
{
    public class IndexModel : PageModel
    {
        public List <InformatiiAngajat> informatiiAngajati = new List<InformatiiAngajat>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT * FROM Angajati";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                informatiiAngajati.Add(new InformatiiAngajat()
                                {
                                    idAngajat = sqlDataReader["idAngajat"].ToString(),
                                    numeAngajat = sqlDataReader["numeAngajat"].ToString(),
                                    functie = sqlDataReader["functie"].ToString(),
                                    salariu = sqlDataReader["salariu"].ToString(),
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

    public class InformatiiAngajat
    {
        public String idAngajat;
        public String numeAngajat;
        public String functie;
        public String salariu;
    }
}
