using GtfsDtlk_ToDtwh.Domain.Datalake;
using GtfsDtlk_ToDtwh.Domain.Datawarehouse;
using Serilog.Core;

namespace GtfsDtlk_ToDtwh.Persistence;

/// <summary>
///     Class for manage all SQL query
/// </summary>
public static class DtwhSqlManager
{
    //***** RESEAU *****//

    #region RESEAU

    /// <summary>
    ///     Gets the reseau by code.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public static DtwhReseau GetReseauByCode(PostgreSqlContext context, Logger log, long code)
    {
        var result = DtwhReseau.Empty();

        try
        {
            var query =
                $"SELECT \"Id\",\"Code\",\"Nom\",\"Url\",\"Timezone\",\"Telephone\" FROM \"reseau\" WHERE \"Code\" = '{code}' LIMIT 1;";

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
    ///     Gets the all reseaus.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtwhReseau> GetAllReseaux(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtwhReseau>();

        try
        {
            const string query =
                "SELECT \"Id\",\"Code\",\"Nom\",\"Url\",\"Timezone\",\"Telephone\" FROM \"reseau\" ORDER BY \"Id\";";

            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtwhReseau(Convert.ToInt64(row[0]),
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
    ///     Creates the type of the ligne.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="agence">The agence object.</param>
    /// <returns></returns>
    public static DtwhReseau CreateReseau(PostgreSqlContext context, Logger log, DtlkAgence agence)
    {
        var result = DtwhReseau.Empty();
        try
        {
            var query = $"INSERT INTO \"reseau\"(\"Code\",\"Nom\",\"Url\",\"Timezone\",\"Telephone\") " +
                        $"VALUES({agence.Id}," +
                        $"'{agence.Nom.Replace("'", "''")}'," +
                        $"'{agence.Url}'," +
                        $"'{agence.Timezone}'," +
                        $"'{agence.Telephone}');";

            context.CommandQuery(query, log);
            Thread.Sleep(50);
            var code = agence.Id;
            result = GetReseauByCode(context, log, code);
            log.Information($"Création du Reseau {result.Id} - {result.Nom}");
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    ///     Updates the type of the ligne.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="ligneTypeId">The reseau identifier.</param>
    /// <param name="agence">The agence.</param>
    public static void UpdateReseau(PostgreSqlContext context, Logger log, long ligneTypeId, DtlkAgence agence)
    {
        try
        {
            var query = $"UPDATE \"reseau\" SET " +
                        $"\"Code\"={agence.Id}," +
                        $"\"Nom\"='{agence.Nom.Replace("'", "''")}'," +
                        $"\"Url\"='{agence.Url}'," +
                        $"\"Timezone\"='{agence.Timezone}'," +
                        $"\"Telephone\"='{agence.Telephone}' " +
                        $"WHERE \"Id\"={ligneTypeId};";

            context.CommandQuery(query, log);
            log.Information($"Mise à jour du reseau {ligneTypeId} - {agence.Nom}");
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
    ///     Gets the ligne by identifier.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public static DtwhLigne GetLigneById(PostgreSqlContext context, Logger log, long id)
    {
        var result = DtwhLigne.Empty();

        try
        {
            var query =
                $"SELECT  \"Id\",\"Reseau_Id\",\"Libelle1\",\"Libelle2\",\"Description\",\"Type\",\"Couleur\",\"Couleur_Texte\" FROM \"reseau_ligne\" WHERE \"Id\" = {id} LIMIT 1;";

            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                foreach (var row in rows)
                    result = new DtwhLigne(Convert.ToInt64(row[0]),
                        Convert.ToInt32(row[1]),
                        row[2] ?? string.Empty,
                        row[3] ?? string.Empty,
                        row[4] ?? string.Empty,
                        Convert.ToInt32(row[5]),
                        row[6] ?? string.Empty,
                        row[7] ?? string.Empty);
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    ///     Gets all lignes.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtwhLigne> GetAllLignes(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtwhLigne>();

        try
        {
            const string query =
                "SELECT \"Id\",\"Reseau_Id\",\"Libelle1\",\"Libelle2\",\"Description\",\"Type\",\"Couleur\",\"Couleur_Texte\" FROM \"reseau_ligne\" ORDER BY \"Id\";";

            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtwhLigne(Convert.ToInt64(row[0]),
                    Convert.ToInt64(row[1]),
                    row[2] ?? string.Empty,
                    row[3] ?? string.Empty,
                    row[4] ?? string.Empty,
                    Convert.ToInt32(row[5]),
                    row[6] ?? string.Empty,
                    row[7] ?? string.Empty)
                ));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return results;
    }

    /// <summary>
    ///     Creates the ligne.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="ligne">The ligne.</param>
    public static DtwhLigne CreateLigne(PostgreSqlContext context, Logger log, DtlkLigne ligne)
    {
        var result = DtwhLigne.Empty();
        try
        {
            var query =
                $"INSERT INTO \"reseau_ligne\" (\"Id\"," +
                $"\"Reseau_Id\"," +
                $"\"Libelle1\"," +
                $"\"Libelle2\"," +
                $"\"Description\"," +
                $"\"Type\"," +
                $"\"Couleur\"," +
                $"\"Couleur_Texte\") " +
                $"VALUES({ligne.Id}," +
                $"{ligne.AgenceId}," +
                $"'{ligne.Libelle1.Replace("'", "''")}'," +
                $"'{ligne.Libelle2.Replace("'", "''")}'," +
                $"'{ligne.Description.Replace("'", "''")}'," +
                $"'{ligne.Type}'," +
                $"'{ligne.Couleur.Replace("'", "''")}'," +
                $"'{ligne.CouleurTexte.Replace("'", "''")}');";

            context.CommandQuery(query, log);
            log.Information($"Création de la Ligne {ligne.Libelle1} - {ligne.Libelle2}");
            Thread.Sleep(5);
            result = GetLigneById(context, log, ligne.Id);
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    ///     Updates the ligne.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="ligne">The ligne.</param>
    public static void UpdateLigne(PostgreSqlContext context, Logger log, DtlkLigne ligne)
    {
        try
        {
            var query = $"UPDATE \"reseau_ligne\" SET " +
                        $"\"Reseau_Id\"={ligne.AgenceId}," +
                        $"\"Libelle1\"='{ligne.Libelle1.Replace("'", "''")}'," +
                        $"\"Libelle2\"='{ligne.Libelle2.Replace("'", "''")}'," +
                        $"\"Description\"='{ligne.Description.Replace("'", "''")}'," +
                        $"\"Type\"='{ligne.Type}'," +
                        $"\"Couleur\"='{ligne.Couleur.Replace("'", "''")}'," +
                        $"\"Couleur_Texte\"='{ligne.CouleurTexte.Replace("'", "''")}' WHERE \"Id\" = {ligne.Id};";

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

    /// <summary>
    ///     Gets the arret by code.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public static DtwhArret GetArretById(PostgreSqlContext context, Logger log, long id)
    {
        var result = new DtwhArret();
        try
        {
            var query =
                $"SELECT \"Id\",\"Libelle\",\"Description\",\"Latitude\",\"Longitude\",\"Type\",\"Parent_Arret_Id\" " +
                $"FROM \"reseau_arret\" WHERE \"Id\"='{id}' LIMIT 1;";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                foreach (var row in rows)
                {
                    result.Id = Convert.ToInt64(row[0]);
                    result.Libelle = row[1] ?? string.Empty;
                    result.Description = row[2] ?? string.Empty;
                    result.Latitude = row[3] ?? string.Empty;
                    result.Longitude = row[4] ?? string.Empty;
                    result.Type = Convert.ToInt32(row[5]);
                    result.ParentArretId = Convert.ToInt64(row[6]);
                }
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    ///     Gets all arrets.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtwhArret> GetAllArrets(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtwhArret>();
        try
        {
            const string query =
                "SELECT \"Id\",\"Libelle\",\"Description\",\"Latitude\",\"Longitude\",\"Type\",\"Parent_Arret_Id\" " +
                "FROM \"reseau_arret\";";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtwhArret
                {
                    Id = Convert.ToInt64(row[0]),
                    Libelle = row[1] ?? string.Empty,
                    Description = row[2] ?? string.Empty,
                    Latitude = row[3] ?? string.Empty,
                    Longitude = row[4] ?? string.Empty,
                    Type = Convert.ToInt32(row[5]),
                    ParentArretId = Convert.ToInt64(row[6])
                }));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return results;
    }

    /// <summary>
    ///     Creates the arret.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="arret">The arret.</param>
    /// <returns></returns>
    public static DtwhArret CreateArret(PostgreSqlContext context, Logger log, DtlkArret arret)
    {
        var result = DtwhArret.Empty();
        try
        {
            var query =
                $"INSERT INTO \"reseau_arret\"(\"Id\",\"Libelle\",\"Description\",\"Latitude\",\"Longitude\",\"Type\",\"Parent_Arret_Id\") " +
                $"VALUES('{arret.Id}'," +
                $"'{arret.Libelle.Replace("'", "''")}'," +
                $"'{arret.Description.Replace("'", "''")}'," +
                $"'{arret.Latitude}'," +
                $"'{arret.Longitude}'," +
                $"'{arret.Type}'," +
                $"'{arret.ParentArretId}');";

            context.CommandQuery(query, log);
            log.Information($"Création de l'arret {arret.Id} - {arret.Code}");
            Thread.Sleep(5);
            result = GetArretById(context, log, arret.Id);
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    ///     Updates the arret.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="arret">The arret.</param>
    public static void UpdateArret(PostgreSqlContext context, Logger log, DtlkArret arret)
    {
        try
        {
            var query = $"UPDATE \"reseau_arret\" SET " +
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

    //***** COURSES *****//

    #region COURSES

    /// <summary>
    ///     Gets all courses.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtwhCourse> GetAllCourses(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtwhCourse>();
        try
        {
            const string query =
                "SELECT \"Id\",\"Ligne_Id\",\"Direction\",\"Libelle\" FROM \"reseau_course\";";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtwhCourse
                {
                    Id = Convert.ToInt64(row[0]),
                    LigneId = Convert.ToInt64(row[1]),
                    Direction = Convert.ToInt32(row[2]),
                    Libelle = row[3] ?? string.Empty
                }));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return results;
    }

    /// <summary>
    ///     Gets the course by identifier.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="courseId">The course identifier.</param>
    /// <returns></returns>
    public static DtwhCourse GetCourseById(PostgreSqlContext context, Logger log, long courseId)
    {
        var result = new DtwhCourse();
        try
        {
            var query =
                $"SELECT \"Id\",\"Ligne_Id\",\"Direction\",\"Libelle\" FROM \"reseau_course\" WHERE \"Id\"='{courseId}' LIMIT 1;";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                foreach (var row in rows)
                {
                    result.Id = Convert.ToInt64(row[0]);
                    result.LigneId = Convert.ToInt64(row[1]);
                    result.Direction = Convert.ToInt32(row[2]);
                    result.Libelle = row[3] ?? string.Empty;
                }
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    ///     Creates the course.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="voyage">The voyage.</param>
    /// <returns></returns>
    public static DtwhCourse CreateCourse(PostgreSqlContext context, Logger log, DtlkVoyage voyage)
    {
        var result = new DtwhCourse();
        try
        {
            var query = $"INSERT INTO \"reseau_course\"(\"Id\"," +
                        $"\"Ligne_Id\"," +
                        $"\"Direction\"," +
                        $"\"Libelle\",\"Creer\") " +
                        $"VALUES('{voyage.Id}'," +
                        $"'{voyage.LigneId}'," +
                        $"'{voyage.DirectionId}'," +
                        $"'{voyage.LibelleAffichage.Replace("'", "''")}','{DateTime.Now}');";
            context.CommandQuery(query, log);
            log.Information($"Création de la course {voyage.Id} - {voyage.LibelleAffichage}");
            Thread.Sleep(5);
            result = GetCourseById(context, log, voyage.Id);
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    ///     Updates the course.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="voyage">The voyage.</param>
    public static void UpdateCourse(PostgreSqlContext context, Logger log, DtlkVoyage voyage)
    {
        try
        {
            var query = $"UPDATE \"reseau_course\" SET " +
                        $"\"Ligne_Id\"='{voyage.LigneId}'," +
                        $"\"Libelle\"='{voyage.LibelleAffichage.Replace("'", "''")}'," +
                        $"\"Direction\"='{voyage.DirectionId}',\"Modifier\"='{DateTime.Now}' " +
                        $" WHERE \"Id\" = {voyage.Id};";

            context.CommandQuery(query, log);
            log.Information($"Mise à jour de la course {voyage.Id} - {voyage.LibelleAffichage}");
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
    }

    #endregion

    //***** STRUCTURE *****//

    #region STRUCTURE

    /// <summary>
    ///     Gets all structures.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <returns></returns>
    public static List<DtwhStructure> GetAllStructures(PostgreSqlContext context, Logger log)
    {
        var results = new List<DtwhStructure>();
        try
        {
            const string query =
                "SELECT \"Course_Id\",\"Arret_Id\",\"Sequence\",\"Sequence_Distance_Cumul\" FROM \"reseau_structure\";";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                results.AddRange(rows.Select(row => new DtwhStructure
                {
                    CourseId = Convert.ToInt64(row[0]),
                    ArretId = Convert.ToInt64(row[1]),
                    Sequence = Convert.ToInt32(row[2]),
                    Distance = Convert.ToInt32(row[3])
                }));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return results;
    }

    /// <summary>
    ///     Gets the structure by ids.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="courseId">The course identifier.</param>
    /// <param name="arretId">The arret identifier.</param>
    /// <param name="sequence">The sequence.</param>
    /// <returns></returns>
    public static DtwhStructure GetStructureByIds(PostgreSqlContext context, Logger log, long courseId, long arretId,
        int sequence)
    {
        var result = new DtwhStructure();
        try
        {
            var query =
                $"SELECT \"Course_Id\",\"Arret_Id\",\"Sequence\",\"Sequence_Distance_Cumul\" FROM \"reseau_structure\" " +
                $"WHERE \"Course_Id\"='{courseId}' AND " +
                $"\"Arret_Id\"='{arretId}' AND  " +
                $"\"Sequence\"= '{sequence}' LIMIT 1;";
            var rows = context.SelectQuery(query, log).Result;
            if (rows.Count != 0)
                foreach (var row in rows)
                {
                    result.CourseId = Convert.ToInt64(row[0]);
                    result.ArretId = Convert.ToInt64(row[1]);
                    result.Sequence = Convert.ToInt32(row[2]);
                    result.Distance = Convert.ToInt32(row[3]);
                }
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    ///     Creates the structure.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="habillage">The habillage.</param>
    /// <returns></returns>
    public static DtwhStructure CreateStructure(PostgreSqlContext context, Logger log, DtlkHabillage habillage)
    {
        var result = new DtwhStructure();
        try
        {
            var query = $"INSERT INTO \"reseau_structure\"(" +
                        $"\"Course_Id\"," +
                        $"\"Arret_Id\"," +
                        $"\"Sequence\"," +
                        $"\"Sequence_Distance_Cumul\"," +
                        $"\"Creer\") " +
                        $"VALUES('{habillage.VoyageId}'," +
                        $"'{habillage.ArretId}'," +
                        $"'{habillage.SequenceArret}'," +
                        $"'{habillage.DistanceVoyage}','{DateTime.Now}');";
            context.CommandQuery(query, log);
            log.Information(
                $"Création de la structure {habillage.VoyageId} - {habillage.ArretId} - {habillage.SequenceArret}");
            Thread.Sleep(5);
            result = GetStructureByIds(context, log, habillage.VoyageId, habillage.ArretId,
                Convert.ToInt32(habillage.SequenceArret));
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }

        return result;
    }

    /// <summary>
    ///     Updates the structure.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="log">The log.</param>
    /// <param name="habillage">The habillage.</param>
    public static void UpdateStructure(PostgreSqlContext context, Logger log, DtlkHabillage habillage)
    {
        try
        {
            var query = $"UPDATE \"reseau_structure\" SET " +
                        $"\"Course_Id\"='{habillage.VoyageId}'," +
                        $"\"Arret_Id\"='{habillage.ArretId}'," +
                        $"\"Sequence\"='{habillage.SequenceArret}'," +
                        $"\"Sequence_Distance_Cumul\"=''," +
                        $"\"Modifier\"='{DateTime.Now}' " +
                        $" WHERE \"Course_Id\" = '{habillage.VoyageId}' AND " +
                        $"\"Arret_Id\"='{habillage.ArretId}' AND " +
                        $"\"Sequence\"='{habillage.SequenceArret}';";

            context.CommandQuery(query, log);
            log.Information(
                $"Mise à jour de la structure {habillage.VoyageId} - {habillage.ArretId} - {habillage.SequenceArret}");
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
        }
    }

    #endregion
}