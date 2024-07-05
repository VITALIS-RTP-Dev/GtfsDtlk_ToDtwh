using System.Configuration;
using System.Net.Http.Headers;
using System.Runtime;
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
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
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
            log.Information($"Gestion des lignes types");
            // récupération de la liste du datalake et du datawarehouse
            var dtlkAgences = DtlkSqlManager.GetAllAgences(dtlkContext, log);
            var dtwhLigneTypes = DtwhSqlManager.GetAllLigneTypes(dtwhContext, log);
            // boucle sur la liste du datalake
            foreach (var dtlkAgence in dtlkAgences)
            {
                // check if exist in datawarehouse
                var dtwhLigneType = dtwhLigneTypes.Find(x => x.Code.Equals(dtlkAgence.Id))??DtwhLigneType.Empty();
                // if exist > Update
                if (dtwhLigneType.Id != 0)
                {
                    dtwhLigneType.CompareAndCopy(dtlkAgence);
                    DtwhSqlManager.UpdateLigneType(dtwhContext,log,dtwhLigneType);
                }
                // if not exist > Create
                else
                {
                    dtwhLigneTypes.Add(DtwhSqlManager.CreateLigneType(dtwhContext, log, dtlkAgence));
                }
            }


            //***** LIGNES *****//
            log.Information($"Gestion des lignes");
            // récupération de la liste du datalake
            var dtlkLignes = DtlkSqlManager.GetAllLignes(dtlkContext,log);
            var dtwhLignes = DtwhSqlManager.GetAllLignes(dtwhContext, log);
            // boucle sur la liste du datalake
            foreach (var dtlkLigne in dtlkLignes)
            {
                // check if exist in datawarehouse
                var dtwhLigne = dtwhLignes.Find(x => x.Code.Equals(dtlkLigne.Id))??DtwhLigne.Empty();
                // if exist > Update
                if (dtwhLigne.Id != 0)
                {
                    dtwhLigne.CompareAndCopy(dtlkLigne);
                    DtwhSqlManager.UpdateLigne(dtwhContext,log,dtwhLigne);
                }
                // if not exist > Create
                else
                {
                    dtwhLignes.Add(DtwhSqlManager.CreateLigne(dtwhContext,log,dtlkLigne,dtwhLigneTypes.Find(x => x.Code.Equals(dtlkLigne.AgenceId))!.Id));
                }
            }

            //***** ARRETS *****
            log.Information($"Gestion des arrets");
            // récupération de la liste du datalake
            var dtlkArrets = DtlkSqlManager.GetAllArrets(dtlkContext, log);
            var dtwhArrets = DtwhSqlManager.GetAllArrets(dtwhContext, log);
         // boucle sur la liste du datalake
            foreach (var dtlkArret in dtlkArrets)
            {
                var parentDtlkArret = dtlkArrets.Find(arret => arret.Id.Equals(dtlkArret.ParentArretId))??DtlkArret.Empty();
                var parentDtwhArret = dtwhArrets.Find(arret =>
                    arret.Code.Equals(parentDtlkArret.Code, StringComparison.Ordinal))??DtwhArret.Empty();
                // check if exist in datawarehouse
                var dtwhArret = dtwhArrets.Find(x => x.Code.Equals(dtlkArret.Code)) ?? DtwhArret.Empty();
                // if exist > Update
                if (dtwhArret.Id != 0)
                {
                    dtwhArret.CompareAndCopy(dtlkArret);
                    dtwhArret.ParentArretId = parentDtwhArret.Id;
                    DtwhSqlManager.UpdateArret(dtwhContext, log, dtwhArret);
                }
                // if not exist > Create
                else
                {
                    var newArret = DtwhSqlManager.CreateArret(dtwhContext, log, dtlkArret, parentDtwhArret.Id);
                        dtwhArrets.Add(newArret);
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