using System.Configuration;
using System.Text;
using GtfsDtlk_ToDtwh.Domain.Datalake;
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
            var ligneTypesFromDb = DtwhSqlManager.GetAllLigneTypes(dtwhContext, log);

            foreach (var agenceFromDtlk in agencesFromDtlk)
            {
                // check if exist in datawarehouse
                var ligneTypeFromDb = ligneTypesFromDb.Find(x => x.Code.Equals(agenceFromDtlk.Id)) ?? DtwhLigneType.Empty();
                // if not exist > Create
                if (ligneTypeFromDb.Id == 0)
                {
                    // CREATE
                    DtwhSqlManager.CreateLigneType(dtwhContext, log, agenceFromDtlk);
                }
                // if exist > Update
                else
                {
                    if (!agenceFromDtlk.Equals(ligneTypeFromDb))
                    {
                        // UPDATE
                        DtwhSqlManager.UpdateLigneType(dtwhContext, log, ligneTypeFromDb.Id, agenceFromDtlk);
                    }
                }
            }

            // Reload Db
            ligneTypesFromDb = DtwhSqlManager.GetAllLigneTypes(dtwhContext, log);

            //***** LIGNES *****//
            log.Information("Gestion des lignes");
            // récupération de la liste du datalake
            var lignesFromDtlk = DtlkSqlManager.GetAllLignes(dtlkContext, log);
            var lignesFromDb = DtwhSqlManager.GetAllLignes(dtwhContext, log);

            foreach (var ligneFromDtlk in lignesFromDtlk)
            {
                // check if exist in datawarehouse
                var ligneFromDb = lignesFromDb.Find(x => x.Code.Equals(ligneFromDtlk.Id)) ?? DtwhLigne.Empty();
                // if not exist > Create
                if (ligneFromDb.Id == 0)
                {
                    var ligneTypeId = ligneTypesFromDb.Find(x => x.Code.Equals(ligneFromDtlk.Id))!.Id;
                    // CREATE
                    DtwhSqlManager.CreateLigne(dtwhContext, log, ligneFromDtlk, ligneTypeId);
                }
                // if exist > Update
                else
                {
                    if (!ligneFromDtlk.Equals(ligneFromDb))
                    {
                        DtwhSqlManager.UpdateLigne(dtwhContext, log, ligneFromDb.Id, ligneFromDb.LigneTypeId, ligneFromDtlk);
                    }
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
                var parentArretFromDtlk = arretsFromDtlk.Find(arret => arret.Id.Equals(arretFromDtlk.ParentArretId)) ??
                                      DtlkArret.Empty();
                var parentDtwhArret = arretsFromDb.Find(arret =>
                    arret.Code.Equals(parentArretFromDtlk.Code, StringComparison.Ordinal)) ?? DtwhArret.Empty();

                // check if exist in datawarehouse
                var arretFromDb = arretsFromDb.Find(x => x.Code.Equals(arretFromDtlk.Code)) ?? DtwhArret.Empty();
                // if not exist > Create
                if (arretFromDb.Id == 0)
                {
                    // CREATE
                    arretsFromDb.Add(DtwhSqlManager.CreateArret(dtwhContext, log, arretFromDtlk, parentDtwhArret.Id));
                }
                // if exist > Update
                else
                {
                    // UPDATE
                    arretFromDb.CompareAndCopy(arretFromDtlk);
                    arretFromDb.ParentArretId = parentDtwhArret.Id;
                    DtwhSqlManager.UpdateArret(dtwhContext, log, arretFromDb);
                }

            }
            // Manage Parents ID


            //***** VOYAGE *****
            log.Information("Gestion des voyages");
            // récupération de la liste du datalake
            var dtlkVoyages = DtlkSqlManager.GetAllVoyages(dtlkContext, log);


            //***** HABILLAGE *****
            log.Information("Gestion des habillages");
            // récupération de la liste du datalake
            var dtlkHabillages = DtlkSqlManager.GetAllHabillages(dtlkContext, log);
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