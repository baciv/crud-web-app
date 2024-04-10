using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Clienti
{
    public class InsertModel : PageModel
    {
        public InformatiiClient informatiiClient = new InformatiiClient();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            informatiiClient.numeClient = Request.Form["Nume"];
            informatiiClient.adresa = Request.Form["adresa"];
            informatiiClient.telefon = Request.Form["telefon"];

            if(informatiiClient.numeClient == "" || informatiiClient.adresa == "" || informatiiClient.telefon == "")
            {
                errorMessage = "Toate campurile sunt obligatorii!";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "INSERT INTO Clienti(numeClient, adresa, telefon) VALUES(@numeClient, @adresa, @telefon)";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@numeClient", informatiiClient.numeClient);
                        sqlCommand.Parameters.AddWithValue("@adresa", informatiiClient.adresa);
                        sqlCommand.Parameters.AddWithValue("@telefon", informatiiClient.telefon);
                        sqlCommand.ExecuteNonQuery();
                    }
         
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            informatiiClient.numeClient = "";
            informatiiClient.adresa = "";
            informatiiClient.telefon = "";
            successMessage = "Clientul a fost adaugat cu succes!";

        }
    }
}
