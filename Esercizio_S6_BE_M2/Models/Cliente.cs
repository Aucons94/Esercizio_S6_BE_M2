using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Esercizio_S6_BE_M2.Models
{
    public class Cliente
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo Cognome è obbligatorio.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il campo Luogo di Nascita è obbligatorio.")]
        public string LuogoDiNascita { get; set; }

        [Required(ErrorMessage = "Il campo Residenza è obbligatorio.")]
        public string Residenza { get; set; }

        [Required(ErrorMessage = "Il campo Data di Nascita è obbligatorio.")]
        public string DataDiNascita { get; set; }

        public bool IsAzienda { get; set; }

        [Required(ErrorMessage = "Il campo Codice Fiscale è obbligatorio.")]
        public string CodiceFiscale { get; set; }

        public string PartitaIVA { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(CodiceFiscale) && string.IsNullOrEmpty(PartitaIVA))
            {
                yield return new ValidationResult("Devi fornire o il Codice Fiscale o la Partita IVA.",
                                                    new[] { nameof(CodiceFiscale), nameof(PartitaIVA) });
            }
            else if (!string.IsNullOrEmpty(CodiceFiscale) && !string.IsNullOrEmpty(PartitaIVA))
            {
                yield return new ValidationResult("Puoi fornire solo il Codice Fiscale o la Partita IVA, non entrambi.",
                                                    new[] { nameof(CodiceFiscale), nameof(PartitaIVA) });
            }
        }

        public string IndirizzoSede { get; set; }

        public string CittaSede { get; set; }


        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString;
        }

        public static List<Cliente> ListaClienti()
        {
            List<Cliente> clienti = new List<Cliente>();

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                string query = "SELECT * FROM Clienti";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                Cognome = reader["Cognome"].ToString(),
                                LuogoDiNascita = reader["LuogoDiNascita"].ToString(),
                                Residenza = reader["Residenza"].ToString(),
                                DataDiNascita = reader["DataDiNascita"].ToString(),
                                IsAzienda = (bool)reader["IsAzienda"],
                                CodiceFiscale = reader["CodiceFiscale"].ToString(),
                                PartitaIVA = reader["PartitaIVA"].ToString(),
                                IndirizzoSede = reader["IndirizzoSede"].ToString(),
                                CittaSede = reader["CittaSede"].ToString(),
                            };

                            clienti.Add(cliente);
                        }
                    }
                }
            }
            return clienti;
        }
}