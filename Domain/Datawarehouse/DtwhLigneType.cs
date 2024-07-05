using System.Runtime.CompilerServices;
using GtfsDtlk_ToDtwh.Domain.Datalake;

namespace GtfsDtlk_ToDtwh.Domain.Datawarehouse;

/// <summary>
///     Classe of Datawarehouse object : Ligne Type
/// </summary>
public class DtwhLigneType
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtwhLigneType" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="code">The code</param>
    /// <param name="nom">The nom.</param>
    /// <param name="url">The URL.</param>
    /// <param name="timezone">The timezone.</param>
    /// <param name="telephone">The telephone.</param>
    public DtwhLigneType(long id, long code, string nom, string url, string timezone, string telephone)
    {
        Id = id;
        Code = code;
        Nom = nom;
        Url = url;
        Timezone = timezone;
        Telephone = telephone;
    }


    public long Id { get; set; }
    public long Code { get; set; }
    public string Nom { get; set; }
    public string Url { get; set; }
    public string Timezone { get; set; }
    public string Telephone { get; set; }

    /// <summary>
    ///     Empties this instance.
    /// </summary>
    /// <returns></returns>
    public static DtwhLigneType Empty()
    {
        return new DtwhLigneType(0, 0, string.Empty, string.Empty, string.Empty, string.Empty);
    }

    /// <summary>
    /// Compares and copy.
    /// </summary>
    /// <param name="agence">The agence.</param>
    public void CompareAndCopy(DtlkAgence agence)
    {
        if (this.Code != agence.Id) this.Code = agence.Id;
        if (this.Nom != agence.Nom) this.Nom = agence.Nom;
        if (this.Telephone != agence.Telephone) this.Telephone = agence.Telephone;
        if (this.Timezone != agence.Timezone) this.Timezone = agence.Timezone;
        if (this.Url != agence.Url) this.Url = agence.Url;

    }
}