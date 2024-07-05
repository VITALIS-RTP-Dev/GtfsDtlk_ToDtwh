using GtfsDtlk_ToDtwh.Domain.Datalake;

namespace GtfsDtlk_ToDtwh.Domain.Datawarehouse;

public class DtwhArret
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtwhArret" /> class.
    /// </summary>
    public DtwhArret()
    {
        Id = 0;
        Code = string.Empty;
        Libelle = string.Empty;
        Description = string.Empty;
        Latitude = string.Empty;
        Longitude = string.Empty;
        Type = 0;
        ParentArretId = 0;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DtwhArret" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="code">The code.</param>
    /// <param name="libelle">The libelle.</param>
    /// <param name="description">The description.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="type">The type.</param>
    /// <param name="parentArretId">The parent arret identifier.</param>
    public DtwhArret(long id, string code, string libelle, string description, string latitude, string longitude,
        int type, long parentArretId)
    {
        Id = id;
        Code = code;
        Libelle = libelle;
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
        Type = type;
        ParentArretId = parentArretId;
    }

    public long Id { get; set; }
    public string Code { get; set; }
    public string Libelle { get; set; }
    public string Description { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public int Type { get; set; }
    public long ParentArretId { get; set; }

    /// <summary>
    ///     Empties this instance.
    /// </summary>
    /// <returns></returns>
    public static DtwhArret Empty()
    {
        return new DtwhArret();
    }

    public void CompareAndCopy(DtlkArret arret)
    {
        if (Code != arret.Code) Code = arret.Code;
        if (Libelle != arret.Libelle) Libelle = arret.Libelle;
        if (Description != arret.Description) Description = arret.Description;
        if (Type != Convert.ToInt32(arret.Type)) Type = Convert.ToInt32(arret.Type);
        if (Latitude != arret.Latitude) Latitude = arret.Latitude;
        if (Longitude != arret.Longitude) Longitude = arret.Latitude;
    }
}