using System.Globalization;
using GtfsDtlk_ToDtwh.Domain.Datalake;
using GtfsDtlk_ToDtwh.Domain.Datawarehouse;
using Serilog;
using Serilog.Core;

namespace GtfsDtlk_ToDtwh.Persistence;

/// <summary>
///     Class for manage all SQL query
/// </summary>
public static class DtwhSqlManager
{
    //***** LIGNE TYPE*****//
    #region LIGNE TYPE

    /// <summary>
    /// Gets the ligne type by code.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public static DtwhLigneType GetLigneTypeByCode(PostgreSqlContext context, Logger log, long code)
    {
        var result = DtwhLigneType.Empty();

        try
        {
            var query = $"SELECT \"Id\",\"Code\",\"Nom\",\"Url\",\"Timezone\",\"Telephone\" FROM \"ligne_type\" WHERE \"Code\" = '{code}' LIMIT 1;";

            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                foreach (var row in rows)
                {
                    result.Id = Convert.ToInt64(row[0]);
                    result.Code = Convert.ToInt64(row[1]);
                    result.Nom = row[2];
                    result.Url = row[3];
                    result.Timezone = row[4];
                    result.Telephone = row[5];
                }
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    /// Gets the all ligne types.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtwhLigneType> GetAllLigneTypes(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtwhLigneType>();

        try
        {
            const string query = $"SELECT \"Id\",\"Code\",\"Nom\",\"Url\",\"Timezone\",\"Telephone\" FROM \"ligne_type\" ORDER BY \"Id\";";

            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtwhLigneType(Convert.ToInt64(row[0]),
                    Convert.ToInt64(row[1]),
                    row[2],
                    row[3],
                    row[4],
                    row[5])
                ));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return results;
    }

    /// <summary>
    /// Creates the type of the ligne.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="agence">The agence object.</param>
    /// <returns></returns>
    public static DtwhLigneType CreateLigneType(PostgreSqlContext context, Logger log, DtlkAgence agence)
    {
        var result = DtwhLigneType.Empty();
        try
        {
            var query = $"INSERT INTO \"ligne_type\"(\"Code\",\"Nom\",\"Url\",\"Timezone\",\"Telephone\") " +
                        $"VALUES({agence.Id}," +
                        $"'{agence.Nom.Replace("'","''")}'," +
                        $"'{agence.Url}'," +
                        $"'{agence.Timezone}'," +
                        $"'{agence.Telephone}');";

            context.CommandQuery(query, log);
            Thread.Sleep(50);
            var code = agence.Id;
            result = GetLigneTypeByCode(context, log, code );
            log.Information($"Création de la Ligne Type {result.Id} - {result.Nom}");
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Updates the type of the ligne.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="ligneType">Type of the ligne.</param>
    public static void UpdateLigneType(PostgreSqlContext context, Logger log, DtwhLigneType ligneType)
    {
        try
        {
            var query = $"UPDATE \"ligne_type\" SET " +
                        $"\"Code\"={ligneType.Code}," +
                        $"\"Nom\"='{ligneType.Nom.Replace("'","''")}'," +
                        $"\"Url\"='{ligneType.Url}'," +
                        $"\"Timezone\"='{ligneType.Timezone}'," +
                        $"\"Telephone\"='{ligneType.Telephone}' " +
                        $"WHERE \"Id\"={ligneType.Id};";

            context.CommandQuery(query, log);
            log.Information($"Mise à jour de la Ligne Type {ligneType.Id} - {ligneType.Nom}");
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
    }

    #endregion

    //***** LIGNES *****//
    #region LIGNES      
    /// <summary>
    /// Gets the ligne by code.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public static DtwhLigne GetLigneByCode(PostgreSqlContext context, Logger log, long code)
    {
        var result = DtwhLigne.Empty();

        try
        {
            var query = $"SELECT  \"Id\",\"Code\",\"Ligne_Type_Id\",\"Libelle1\",\"Libelle2\",\"Description\",\"Type\",\"Couleur\",\"Couleur_Texte\" FROM \"ligne\" WHERE \"Code\" = {code} LIMIT 1;";
            
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
            {
                foreach (var row in rows)
                {
                    result = new DtwhLigne(Convert.ToInt64(row[0]),
                        Convert.ToInt32(row[1]),
                        Convert.ToInt64(row[2]),
                        row[3],
                        row[4],
                        row[5],
                        row[6],
                        row[7],
                        row[8]);
                }
            }
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    /// Gets all lignes.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtwhLigne> GetAllLignes(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtwhLigne>();

        try
        {
            const string query = $"SELECT \"Id\",\"Code\",\"Ligne_Type_Id\",\"Libelle1\",\"Libelle2\",\"Description\",\"Type\",\"Couleur\",\"Couleur_Texte\" FROM \"ligne\" ORDER BY \"Id\";";

            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtwhLigne(Convert.ToInt64(row[0]),
                    Convert.ToInt64(row[1]),
                    Convert.ToInt64(row[2]),
                    row[3],
                    row[4],
                    row[5],
                    row[6],
                    row[7],
                    row[8])
                ));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return results;
    }

    /// <summary>
    /// Creates the ligne.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="ligne">The ligne.</param>
    /// <returns></returns>
    public static DtwhLigne CreateLigne(PostgreSqlContext context, Logger log, DtlkLigne ligne,long dtwhLigneTypeId)
    {
        var result = DtwhLigne.Empty();
        try
        {
            var query =
                $"INSERT INTO \"ligne\" (\"Code\"," +
                $"\"Ligne_Type_Id\"," +
                $"\"Libelle1\"," +
                $"\"Libelle2\"," +
                $"\"Description\"," +
                $"\"Type\"," +
                $"\"Couleur\"," +
                $"\"Couleur_Texte\") " +
                $"VALUES({ligne.Id}," +
                $"{dtwhLigneTypeId}," +
                $"'{ligne.Libelle1.Replace("'", "''")}'," +
                $"'{ligne.Libelle2.Replace("'", "''")}'," +
                $"'{ligne.Description.Replace("'", "''")}'," +
                $"'{ligne.Type.Replace("'", "''")}'," +
                $"'{ligne.Couleur.Replace("'", "''")}'," +
                $"'{ligne.CouleurTexte.Replace("'", "''")}');";

            context.CommandQuery(query, log);
            Thread.Sleep(50);
            result = GetLigneByCode(context, log, ligne.Id);
            log.Information($"Création de la Ligne {result.Libelle1} - {result.Libelle2}");
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Updates the ligne.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="ligne">The ligne.</param>
    public static void UpdateLigne(PostgreSqlContext context, Logger log, DtwhLigne ligne)
    {
        try
        {
            var query = $"UPDATE \"ligne\" SET " +
                        $"\"Code\"={ligne.Code}," +
                        $"\"Ligne_Type_Id\"={ligne.LigneTypeId}," +
                        $"\"Libelle1\"='{ligne.Libelle1.Replace("'","''")}'," +
                        $"\"Libelle2\"='{ligne.Libelle2.Replace("'","''")}'," +
                        $"\"Description\"='{ligne.Description.Replace("'","''")}'," +
                        $"\"Type\"='{ligne.Type.Replace("'","''")}'," +
                        $"\"Couleur\"='{ligne.Couleur.Replace("'","''")}'," +
                        $"\"Couleur_Texte\"='{ligne.CouleurTexte.Replace("'","''")}' WHERE \"Id\" = {ligne.Id};";

            context.CommandQuery(query, log);
            log.Information($"Mise à jour de la Ligne {ligne.Libelle1} - {ligne.Libelle2}");
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
    }
    #endregion

    //***** ARRETS *****//
    #region ARRETS
    public static DtwhArret GetArretByCode(PostgreSqlContext context, Logger log, string code)
    {
        var result = new DtwhArret();
        try
        {
            var query = $"SELECT \"Id\",\"Code\",\"Libelle\",\"Description\",\"Latitude\",\"Longitude\",\"Type\",\"Parent_Arret_Id\" FROM \"arret\" WHERE \"Code\"='{code}' LIMIT 1;";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                foreach (var row in rows)
                {
                    result.Id = Convert.ToInt64(row[0]);
                    result.Code = row[1];
                    result.Libelle = row[2];
                    result.Description = row[3];
                    result.Latitude = row[4];
                    result.Longitude = row[5];
                    result.Type = Convert.ToInt32(row[6]);
                    result.ParentArretId = Convert.ToInt64(row[7]);
                }
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
        return result;
    }

    public static List<DtwhArret> GetAllArrets(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtwhArret>();
        try
        {
            const string query = $"SELECT \"Id\",\"Code\",\"Libelle\",\"Description\",\"Latitude\",\"Longitude\",\"Type\",\"Parent_Arret_Id\" FROM \"arret\";";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtwhArret
                {
                    Id = Convert.ToInt64(row[0]),
                    Code = row[1],
                    Libelle = row[2],
                    Description = row[3],
                    Latitude = row[4],
                    Longitude = row[5],
                    Type = Convert.ToInt32(row[6]),
                    ParentArretId = Convert.ToInt64(row[7])
                }));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
        return results;
    }

    public static DtwhArret CreateArret(PostgreSqlContext context, Logger log,DtlkArret arret,long parentId)
    {
        var result = DtwhArret.Empty();
        try
        {
            var query = $"INSERT INTO \"arret\"(\"Code\",\"Libelle\",\"Description\",\"Latitude\",\"Longitude\",\"Type\",\"Parent_Arret_Id\") " +
                        $"VALUES('{arret.Code}'," +
                        $"'{arret.Libelle.Replace("'", "''")}'," +
                        $"'{arret.Description.Replace("'","''")}'," +
                        $"'{arret.Latitude}'," +
                        $"'{arret.Longitude}'," +
                        $"'{arret.Type}'," +
                        $"'{parentId}') WHERE \"Id\"='{arret.Id}';";

            context.CommandQuery(query, log);
            Thread.Sleep(50);
            result = GetArretByCode(context, log, arret.Code);
            log.Information($"Création de l'arret {result.Id} - {result.Code}");
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
        return result;
    }

    public static void UpdateArret(PostgreSqlContext context, Logger log, DtwhArret arret)
    {
        try
        {
            var query = $"UPDATE \"arret\" SET " +
                        $"\"Code\"='{arret.Code}'," +
                        $"\"Libelle\"='{arret.Libelle.Replace("'", "''")}'," +
                        $"\"Description\"='{arret.Description.Replace("'", "''")}'," +
                        $"\"Type\"='{arret.Type}'," +
                        $"\"Parent_Arret_Id\"='{arret.ParentArretId}'," +
                        $"\"Latitude\"='{arret.Latitude.Replace("'", "''")}'," +
                        $"\"Longitude\"='{arret.Longitude.Replace("'", "''")}' WHERE \"Id\" = {arret.Id};";

            context.CommandQuery(query, log);
            log.Information($"Mise à jour de l'arret {arret.Code} - {arret.Libelle}");
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
    }
    #endregion
}