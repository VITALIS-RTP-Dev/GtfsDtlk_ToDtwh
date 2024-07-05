using System.Text.Json.Serialization;

namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkAgence
{
    public DtlkAgence()
    {
        Id = 0;
        Nom = string.Empty;
        Url = string.Empty;
        Timezone = string.Empty;
        Langue = string.Empty;
        Telephone = string.Empty;
    }

    [JsonConstructor]
    public DtlkAgence(long id, string nom, string url, string timezone, string langue, string telephone)
    {
        Id = id;
        Nom = nom;
        Url = url;
        Timezone = timezone;
        Langue = langue;
        Telephone = telephone;
    }

    public long Id { get; set; }
    public string Nom { get; set; }
    public string Url { get; set; }
    public string Timezone { get; set; }
    public string Langue { get; set; }
    public string Telephone { get; set; }
}