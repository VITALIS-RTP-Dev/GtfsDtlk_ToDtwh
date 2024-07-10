using GtfsDtlk_ToDtwh.Domain.Datawarehouse;

namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkAgence : IEquatable<DtwhReseau>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkAgence" /> class.
    /// </summary>
    public DtlkAgence()
    {
        Id = 0;
        Nom = string.Empty;
        Url = string.Empty;
        Timezone = string.Empty;
        Langue = string.Empty;
        Telephone = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkAgence" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="nom">The nom.</param>
    /// <param name="url">The URL.</param>
    /// <param name="timezone">The timezone.</param>
    /// <param name="langue">The langue.</param>
    /// <param name="telephone">The telephone.</param>
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

    /// <summary>
    ///     Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///     <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public bool Equals(DtwhReseau? other)
    {
        return other != null
               && Id == other.Code
               && Nom == other.Nom
               && Url == other.Url
               && Timezone == other.Timezone
               && Telephone == other.Telephone;
    }

    /// <summary>
    ///     Equalses the specified other.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns></returns>
    protected bool Equals(DtlkAgence other)
    {
        return Id == other.Id && Nom == other.Nom && Url == other.Url && Timezone == other.Timezone &&
               Langue == other.Langue && Telephone == other.Telephone;
    }

    /// <summary>
    ///     Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    ///     <see langword="true" /> if the specified object  is equal to the current object; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DtlkAgence)obj);
    }

    /// <summary>
    ///     Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nom, Url, Timezone, Langue, Telephone);
    }
}