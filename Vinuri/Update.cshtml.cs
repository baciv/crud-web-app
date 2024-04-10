using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Vinuri
{
    public class UpdateModel : PageModel
    {
        public InformatiiVin informatiiVin = new InformatiiVin();
        public String errorMessage = "";
        public String successMessage = "";


        public void OnGet()
        {
            String idVin = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=DESKTOP-COOR4AG;Initial Catalog=FermaViticola;Integrated Security=True;Encrypt=False";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String query = "SELECT * FROM Vinuri WHERE idVin = @idVin";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@idVin", idVin);
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                informatiiVin.idVin = sqlDataReader["idVin"].ToString();
                                informatiiVin.numeVin = sqlDataReader["numeVin"].ToString();
                                informatiiVin.anRecolta = sqlDataReader["anRecolta"].ToString();
                                informatiiVin.tipVin = sqlDataReader["tipVin"].ToString();
                                informatiiVin.pretLitru = sqlDataReader["pretLitru"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            informatiiVin.idVin = Request.Form["idVin"];
            informatiiVin.numeVin = Request.Form["numeVin"];
            informatiiVin.anRecolta = Request.Form["anRecolta"];
            informatiiVin.tipVin = Request.Form["tipVin"];
            informatiiVin.pretLitru = Request.Form["pretLitru"];

            if (informatiiVin.numeVin == "" || informatiiVin.anRecolta == "" || informatiiVin.tipVin == "" || informatiiVin.pretLitru == "")
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
                    String query = "UPDATE Vinuri SET numeVin = @numeVin, anRecolta = @anRecolta, tipVin = @tipVin, pretLitru = @pretLitru WHERE idVin = @idVin";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@idVin", informatiiVin.idVin);
                        sqlCommand.Parameters.AddWithValue("@numeVin", informatiiVin.numeVin);
                        sqlCommand.Parameters.AddWithValue("@anRecolta", informatiiVin.anRecolta);
                        sqlCommand.Parameters.AddWithValue("@tipVin", informatiiVin.tipVin);
                        sqlCommand.Parameters.AddWithValue("@pretLitru", informatiiVin.pretLitru);
                        sqlCommand.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            
            informatiiVin.numeVin = "";
            informatiiVin.anRecolta = "";
            informatiiVin.tipVin = "";
            informatiiVin.pretLitru = "";
            successMessage = "Vinul a fost actualizat cu succes!";
        }
    }
}
