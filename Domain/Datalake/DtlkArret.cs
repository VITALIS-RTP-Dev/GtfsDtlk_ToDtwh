using System.Text.Json.Serialization;

namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkArret
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DtlkArret"/> class.
    /// </summary>
    public DtlkArret()
    {
        Id = 0;
        Code = string.Empty;
        Libelle = string.Empty;
        Description = string.Empty;
        Latitude = string.Empty;
        Longitude = string.Empty;
        Type = string.Empty;
        ParentArretId = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DtlkArret"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="code">The code.</param>
    /// <param name="libelle">The libelle.</param>
    /// <param name="description">The description.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="type">The type.</param>
    /// <param name="parentArretId">The parent arret identifier.</param>
    public DtlkArret(long id, string code, string libelle, string description, string latitude, string longitude,
        string type, long parentArretId)
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
    public string Type { get; set; }
    public long ParentArretId { get; set; }

    /// <summary>
    /// Empties this instance.
    /// </summary>
    /// <returns></returns>
    public static DtlkArret Empty()
    {
        return new DtlkArret();
    }
}