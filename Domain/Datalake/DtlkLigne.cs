using GtfsDtlk_ToDtwh.Domain.Datawarehouse;

namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkLigne : IEquatable<DtwhLigne>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkLigne" /> class.
    /// </summary>
    public DtlkLigne()
    {
        Id = 0;
        AgenceId = 0;
        Libelle1 = string.Empty;
        Libelle2 = string.Empty;
        Description = string.Empty;
        Type = string.Empty;
        Couleur = string.Empty;
        CouleurTexte = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkLigne" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="agenceId">The agence identifier.</param>
    /// <param name="libelle1">The libelle1.</param>
    /// <param name="libelle2">The libelle2.</param>
    /// <param name="description">The description.</param>
    /// <param name="type">The type.</param>
    /// <param name="couleur">The couleur.</param>
    /// <param name="couleurTexte">The couleur texte.</param>
    public DtlkLigne(long id, long agenceId, string libelle1, string libelle2, string description, string type,
        string couleur, string couleurTexte)
    {
        Id = id;
        AgenceId = agenceId;
        Libelle1 = libelle1;
        Libelle2 = libelle2;
        Description = description;
        Type = type;
        Couleur = couleur;
        CouleurTexte = couleurTexte;
    }

    public long Id { get; set; }
    public long AgenceId { get; set; }
    public string Libelle1 { get; set; }
    public string Libelle2 { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Couleur { get; set; }
    public string CouleurTexte { get; set; }

    /// <summary>
    /// Equalses the specified other.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns></returns>
    protected bool Equals(DtlkLigne other)
    {
        return Id == other.Id && AgenceId == other.AgenceId && Libelle1 == other.Libelle1 && Libelle2 == other.Libelle2 && Description == other.Description && Type == other.Type && Couleur == other.Couleur && CouleurTexte == other.CouleurTexte;
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(DtwhLigne other)
    {
        return Id == other.Code && Libelle1 == other.Libelle1 && Libelle2 == other.Libelle2 && Description == other.Description && Type == other.Type && Couleur == other.Couleur && CouleurTexte == other.CouleurTexte;

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
        return Equals((DtlkLigne)obj);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, AgenceId, Libelle1, Libelle2, Description, Type, Couleur, CouleurTexte);
    }
}