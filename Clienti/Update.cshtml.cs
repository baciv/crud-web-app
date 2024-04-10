using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Clienti
{
    public class UpdateModel : PageModel
    {
        public InformatiiClient informatiiClient = new InformatiiClient();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String idClient = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT * FROM Clienti WHERE idClient = @idClient";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@idClient", idClient);
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                informatiiClient.idClient = sqlDataReader["idClient"].ToString();
                                informatiiClient.numeClient = sqlDataReader["numeClient"].ToString();
                                informatiiClient.adresa = sqlDataReader["adresa"].ToString();
                                informatiiClient.telefon = sqlDataReader["telefon"].ToString();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            informatiiClient.idClient = Request.Form["idClient"];
            informatiiClient.numeClient = Request.Form["Nume"];
            informatiiClient.adresa = Request.Form["adresa"];
            informatiiClient.telefon = Request.Form["telefon"];

            if (informatiiClient.numeClient == "" || informatiiClient.adresa == "" || informatiiClient.telefon == "")
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
                    String query = "UPDATE Clienti SET numeClient = @numeClient, adresa = @adresa, telefon = @telefon WHERE idClient = @idClient";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@idClient", informatiiClient.idClient);
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
            successMessage = "Clientul a fost modificat cu succes!";

           
        }
    }
}
