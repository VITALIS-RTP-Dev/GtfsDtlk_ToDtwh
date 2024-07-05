using System.Globalization;
using GtfsDtlk_ToDtwh.Domain.Datalake;
using GtfsDtlk_ToDtwh.Domain.Datawarehouse;
using Serilog.Core;

namespace GtfsDtlk_ToDtwh.Persistence;

/// <summary>
///     Class for manage all SQL query
/// </summary>
public static class DtlkSqlManager
{

    //***** AGENCES ****//
    #region AGENCES

    /// <summary>
    /// Gets all agences.
    /// </summary>
    /// <param name="context">The DTL context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtlkAgence> GetAllAgences(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtlkAgence>();

        try
        {
            const string query = $"SELECT DISTINCT \"Id\",\"Nom\",\"Url\",\"Timezone\",\"Langue\",\"Telephone\" FROM gtfs_agence ORDER BY \"Id\";";

            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtlkAgence()
                {
                    Id = Convert.ToInt64(row[0]),
                    Nom = row[1],
                    Url = row[2],
                    Timezone = row[3],
                    Langue = row[4],
                    Telephone = row[5]
                }));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return results;
    }
    #endregion

    //***** LIGNES *****//
    #region LIGNES    
    /// <summary>
    /// Gets all lignes.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtlkLigne> GetAllLignes(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtlkLigne>();

        try
        {
            const string query = $"SELECT DISTINCT \"Id\",\"Agence_Id\",\"Libelle1\",\"Libelle2\",\"Description\",\"Type\",\"Couleur\",\"CouleurTexte\" FROM \"gtfs_ligne\" ORDER BY \"Id\";";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtlkLigne()
                {
                    Id = Convert.ToInt64(row[0]),
                    AgenceId = Convert.ToInt64(row[1]),
                    Libelle1 = row[2],
                    Libelle2 = row[3],
                    Description = row[4],
                    Type = row[5],
                    Couleur = row[6],
                    CouleurTexte = row[7]
                }));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
        
        return results;
    }
    #endregion


    //***** ARRETS *****//
    #region ARRETS    
    /// <summary>
    /// Gets all arrets.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtlkArret> GetAllArrets(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtlkArret>();

        try
        {
            const string query = $"SELECT DISTINCT \"Id\",\"Code\",\"Libelle\",\"Description\",\"Latitude\",\"Longitude\",\"Type\",\"Parent_Arret\" FROM \"gtfs_arret\" ORDER BY \"Parent_Arret\" DESC;";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtlkArret()
                {
                    Id = Convert.ToInt64(row[0]),
                    Code = row[1],
                    Libelle = row[2],
                    Description = row[3],
                    Latitude = row[4],
                    Longitude = row[5],
                    Type = row[6],
                    ParentArretId = Convert.ToInt64(row[7])
                }));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return results;
    }

    public static DtlkArret GetArretByCode(PostgreSqlContext context, Logger log,string code)
    {
        var result = new DtlkArret();
        try
        {
            var query = $"SELECT \"Id\",\"Code\",\"Libelle\",\"Description\",\"Latitude\",\"Longitude\",\"Type\",\"Parent_Arret\" FROM \"arret\" WHERE \"Code\"='{code}' LIMIT 1;";
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
                    result.Type = row[6];
                    result.ParentArretId = Convert.ToInt64(row[7]);
                }
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
        return result;
    }
    #endregion
}