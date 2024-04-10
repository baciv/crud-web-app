using FermaViticola.Pages.Clienti;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Comenzi
{
    public class IndexModel : PageModel
    {
        public List<InformatiiComanda> informatiiComenzi = new List<InformatiiComanda>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT\r\n    C.idComanda,\r\n    C.dataComanda,\r\n    V.numeVin,\r\n    V.anRecolta,\r\n    DC.cantitate,\r\n    Cl.numeClient,\r\n    A.numeAngajat\r\nFROM Comenzi C\r\nLEFT JOIN DetaliiComanda DC ON C.idComanda = DC.idComanda\r\nLEFT JOIN Vinuri V ON DC.idVin = V.idVin\r\nLEFT JOIN Clienti Cl ON C.idClient = Cl.idClient\r\nLEFT JOIN Angajati A ON DC.idAngajat = A.idAngajat;";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                informatiiComenzi.Add(new InformatiiComanda()
                                {
                                    idComanda = sqlDataReader["idComanda"].ToString(),
                                    dataComanda = sqlDataReader["dataComanda"].ToString(),
                                    numeVin = sqlDataReader["numeVin"].ToString(),
                                    anRecolta = sqlDataReader["anRecolta"].ToString(),
                                    cantitate = sqlDataReader["cantitate"].ToString(),
                                    numeClient = sqlDataReader["numeClient"].ToString(),
                                    numeAngajat = sqlDataReader["numeAngajat"].ToString(),
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

    public class InformatiiComanda
    {
        public String idComanda;
        public String dataComanda;
        public String numeVin;
        public String anRecolta;
        public String cantitate;
        public String numeClient;
        public String numeAngajat;
    }

}
