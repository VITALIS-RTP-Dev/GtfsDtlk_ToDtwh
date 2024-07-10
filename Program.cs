using System.Configuration;
using System.Text;
using GtfsDtlk_ToDtwh.Domain.Datawarehouse;
using GtfsDtlk_ToDtwh.Persistence;
using Serilog;
using Serilog.Events;

public static class Program
{
    public static void Main(string[] args)
    {
        var currentDir = AppDomain.CurrentDomain.BaseDirectory;

        /***** Create directory *****/
        Directory.CreateDirectory(currentDir + @"\logs");

        /***** Fichier Configuration *****/
        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        var dtwhServer = config.AppSettings.Settings["DTWH_PostgreSQL_Server"].Value;
        var dtwhDatabase = config.AppSettings.Settings["DTWH_PostgreSQL_Database"].Value;
        var dtwhPort = config.AppSettings.Settings["DTWH_PostgreSQL_Port"].Value;
        var dtwhUsername =
            Encoding.UTF8.GetString(
                Convert.FromBase64String(config.AppSettings.Settings["DTWH_PostgreSQL_Username"].Value));
        var dtwhPassword =
            Encoding.UTF8.GetString(
                Convert.FromBase64String(config.AppSettings.Settings["DTWH_PostgreSQL_Password"].Value));

        var dtlkServer = config.AppSettings.Settings["DTLK_PostgreSQL_Server"].Value;
        var dtlkDatabase = config.AppSettings.Settings["DTLK_PostgreSQL_Database"].Value;
        var dtlkPort = config.AppSettings.Settings["DTLK_PostgreSQL_Port"].Value;
        var dtlkUsername =
            Encoding.UTF8.GetString(
                Convert.FromBase64String(config.AppSettings.Settings["DTLK_PostgreSQL_Username"].Value));
        var dtlkPassword =
            Encoding.UTF8.GetString(
                Convert.FromBase64String(config.AppSettings.Settings["DTLK_PostgreSQL_Password"].Value));

        var logFilePath = config.AppSettings.Settings["Log_FilePath"];

        if (string.IsNullOrEmpty(logFilePath.Value)) logFilePath.Value = currentDir + @"\logs\log.log";
        config.Save(ConfigurationSaveMode.Modified);

        /***** LOGS *****/
        var log = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(logFilePath.Value, rollingInterval: RollingInterval.Day)
            .WriteTo.Console(LogEventLevel.Information)
            .CreateLogger();

        // Start process
        try
        {
            /***** CONTEXTES *****/
            var dtwhDbSettings = new DbSettings
            {
                Database = dtwhDatabase,
                Password = dtwhPassword,
                Port = dtwhPort,
                Server = dtwhServer,
                Username = dtwhUsername
            };
            var dtwhContext = new PostgreSqlContext(dtwhDbSettings);

            var dtlkDbSettings = new DbSettings
            {
                Database = dtlkDatabase,
                Password = dtlkPassword,
                Port = dtlkPort,
                Server = dtlkServer,
                Username = dtlkUsername
            };
            var dtlkContext = new PostgreSqlContext(dtlkDbSettings);

            log.Information($"///***** Started cycle : {DateTime.Now} *****///");

            //***** AGENCES *****//
            log.Information("Gestion des lignes types");
            // récupération de la liste du datalake et du datawarehouse
            var agencesFromDtlk = DtlkSqlManager.GetAllAgences(dtlkContext, log);
            var ligneTypesFromDb = DtwhSqlManager.GetAllReseaux(dtwhContext, log);

            foreach (var agenceFromDtlk in agencesFromDtlk)
            {
                // check if exist in datawarehouse
                var ligneTypeFromDb =
                    ligneTypesFromDb.Find(x => x.Code.Equals(agenceFromDtlk.Id)) ?? DtwhReseau.Empty();
                // if not exist > Create
                if (ligneTypeFromDb.Id == 0)
                {
                    // CREATE
                    DtwhSqlManager.CreateReseau(dtwhContext, log, agenceFromDtlk);
                }
                // if exist > Update
                else
                {
                    if (!agenceFromDtlk.Equals(ligneTypeFromDb))
                        // UPDATE
                        DtwhSqlManager.UpdateReseau(dtwhContext, log, ligneTypeFromDb.Id, agenceFromDtlk);
                }
            }

            // Reload Db
            ligneTypesFromDb = DtwhSqlManager.GetAllReseaux(dtwhContext, log);

            //***** LIGNES *****//
            log.Information("Gestion des lignes");
            // récupération de la liste du datalake
            var lignesFromDtlk = DtlkSqlManager.GetAllLignes(dtlkContext, log);
            var lignesFromDb = DtwhSqlManager.GetAllLignes(dtwhContext, log);

            foreach (var ligneFromDtlk in lignesFromDtlk)
            {
                // check if exist in datawarehouse
                var ligneFromDb = lignesFromDb.Find(x => x.Id == ligneFromDtlk.Id) ?? DtwhLigne.Empty();
                // if not exist > Create
                if (ligneFromDb.Id == 0)
                {
                    // CREATE
                    lignesFromDb.Add(DtwhSqlManager.CreateLigne(dtwhContext, log, ligneFromDtlk));
                }
                // if exist > Update
                else
                {
                    if (!ligneFromDtlk.Equals(ligneFromDb)) DtwhSqlManager.UpdateLigne(dtwhContext, log, ligneFromDtlk);
                }
            }

            //Reload DB
            lignesFromDb = DtwhSqlManager.GetAllLignes(dtwhContext, log);

            //***** ARRETS *****
            log.Information("Gestion des arrets");
            // récupération de la liste du datalake
            var arretsFromDtlk = DtlkSqlManager.GetAllArrets(dtlkContext, log);
            var arretsFromDb = DtwhSqlManager.GetAllArrets(dtwhContext, log);

            // boucle sur la liste du datalake
            foreach (var arretFromDtlk in arretsFromDtlk)
            {
                // check if exist in datawarehouse
                var arretFromDb = arretsFromDb.Find(x => x.Id.Equals(arretFromDtlk.Id)) ?? DtwhArret.Empty();

                // if not exist > Create
                if (arretFromDb.Id == 0)
                {
                    // CREATE
                    arretsFromDb.Add(DtwhSqlManager.CreateArret(dtwhContext, log, arretFromDtlk));
                }
                // if exist > Update
                else
                {
                    // UPDATE
                    if (!arretFromDtlk.Equals(arretFromDb)) DtwhSqlManager.UpdateArret(dtwhContext, log, arretFromDtlk);
                }
            }

            // reload arret
            arretsFromDb = DtwhSqlManager.GetAllArrets(dtwhContext, log);

            //***** COURSE *****
            log.Information("Gestion des voyages");
            // récupération de la liste du datalake
            var voyagesFromDtlk = DtlkSqlManager.GetAllVoyages(dtlkContext, log);
            var coursesFromDb = DtwhSqlManager.GetAllCourses(dtwhContext, log);

            foreach (var voyageFromDtlk in voyagesFromDtlk)
            {
                // check if exist in datawarehouse
                var courseFromDb = coursesFromDb.Find(x => x.Id.Equals(voyageFromDtlk.Id)) ?? DtwhCourse.Empty();

                // if not exist > Create
                if (courseFromDb.Id == 0)
                {
                    // Recherche de la ligne
                    if (lignesFromDb.Exists(x => x.Id.Equals(voyageFromDtlk.LigneId)))
                        // CREATE
                        coursesFromDb.Add(DtwhSqlManager.CreateCourse(dtwhContext, log, voyageFromDtlk));
                }
                // if exist > Update
                else
                {
                    // UPDATE
                    if (!voyageFromDtlk.Equals(courseFromDb))
                        DtwhSqlManager.UpdateCourse(dtwhContext, log, voyageFromDtlk);
                }
            }

            //***** STRUCTURE *****
            log.Information("Gestion des habillages");
            // récupération de la liste du datalake
            var habillagesFromDtlk = DtlkSqlManager.GetAllHabillages(dtlkContext, log);
            var structuresFromDb = DtwhSqlManager.GetAllStructures(dtwhContext, log);

            foreach (var habillageFromDtlk in habillagesFromDtlk)
            {
                var structureFromDb = structuresFromDb.Find(x => x.CourseId.Equals(habillageFromDtlk.VoyageId) &&
                                                                 x.ArretId.Equals(habillageFromDtlk.ArretId) &&
                                                                 x.Sequence.Equals(
                                                                     Convert.ToInt32(habillageFromDtlk
                                                                         .SequenceArret))) ?? DtwhStructure.Empty();
                // if not exist > Create
                if (structureFromDb.ArretId == 0)
                {
                    // Recherche de la course et de l'arret
                    if (coursesFromDb.Exists(x => x.Id.Equals(habillageFromDtlk.VoyageId)) &&
                        arretsFromDb.Exists(x => x.Id.Equals(habillageFromDtlk.ArretId)))
                        // CREATE
                        structuresFromDb.Add(DtwhSqlManager.CreateStructure(dtwhContext, log, habillageFromDtlk));
                }
                // if exist > Update
                else
                {
                    // UPDATE
                    if (!habillageFromDtlk.Equals(structureFromDb))
                        DtwhSqlManager.UpdateStructure(dtwhContext, log, habillageFromDtlk);
                }
            }
        }
        catch (Exception ex)
        {
            log.Fatal(ex, $"ERROR : {ex.Message}");
        }
        finally
        {
            log.Information("Fin de traitement");
        }
    }
}