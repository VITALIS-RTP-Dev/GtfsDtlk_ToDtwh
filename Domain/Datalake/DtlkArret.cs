using GtfsDtlk_ToDtwh.Domain.Datawarehouse;

namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkArret : IEquatable<DtwhArret>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkArret" /> class.
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
    ///     Initializes a new instance of the <see cref="DtlkArret" /> class.
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
    ///     Empties this instance.
    /// </summary>
    /// <returns></returns>
    public static DtlkArret Empty()
    {
        return new DtlkArret();
    }

    /// <summary>
    /// Equalses the specified other.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns></returns>
    protected bool Equals(DtlkArret other)
    {
        return Id == other.Id && Code == other.Code && Libelle == other.Libelle && Description == other.Description && Latitude == other.Latitude && Longitude == other.Longitude && Type == other.Type && ParentArretId == other.ParentArretId;
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(DtwhArret other)
    {
        return Id == other.Id && Code == other.Code && Libelle == other.Libelle && Description == other.Description && Latitude == other.Latitude && Longitude == other.Longitude && Type == other.Type && ParentArretId == other.ParentArretId;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((DtlkArret)obj);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Code, Libelle, Description, Latitude, Longitude, Type, ParentArretId);
    }
}