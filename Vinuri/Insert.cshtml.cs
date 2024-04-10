using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FermaViticola.Pages.Vinuri
{
    public class InsertModel : PageModel
    {
        public InformatiiVin informatiiVin = new InformatiiVin();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
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
                    String query = "INSERT INTO Vinuri(numeVin, anRecolta, tipVin, pretLitru) VALUES(@numeVin, @anRecolta, @tipVin, @pretLitru)";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
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
        }
    }
}
