using GtfsDtlk_ToDtwh.Domain.Datawarehouse;

namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkHabillage : IEquatable<DtwhStructure>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkHabillage" /> class.
    /// </summary>
    public DtlkHabillage()
    {
        VoyageId = 0;
        Arrivee = string.Empty;
        Depart = string.Empty;
        ArretId = 0;
        SequenceArret = string.Empty;
        TypeMontee = string.Empty;
        TypeDescente = string.Empty;
        DistanceVoyage = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkHabillage" /> class.
    /// </summary>
    /// <param name="voyageId">The voyage identifier.</param>
    /// <param name="arrivee">The arrivee.</param>
    /// <param name="depart">The depart.</param>
    /// <param name="arretId">The arret identifier.</param>
    /// <param name="sequenceArret">The sequence arret.</param>
    /// <param name="typeMontee">The type montee.</param>
    /// <param name="typeDescente">The type descente.</param>
    /// <param name="distanceVoyage">The distance voyage.</param>
    public DtlkHabillage(long voyageId, string arrivee, string depart, long arretId, string sequenceArret,
        string typeMontee, string typeDescente, string distanceVoyage)
    {
        VoyageId = voyageId;
        Arrivee = arrivee;
        Depart = depart;
        ArretId = arretId;
        SequenceArret = sequenceArret;
        TypeMontee = typeMontee;
        TypeDescente = typeDescente;
        DistanceVoyage = distanceVoyage;
    }

    public long VoyageId { get; set; }
    public string Arrivee { get; set; }
    public string Depart { get; set; }
    public long ArretId { get; set; }
    public string SequenceArret { get; set; }
    public string TypeMontee { get; set; }
    public string TypeDescente { get; set; }
    public string DistanceVoyage { get; set; }

    /// <summary>
    ///     Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///     <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public bool Equals(DtwhStructure other)
    {
        return VoyageId == other.CourseId &&
               ArretId == other.ArretId &&
               SequenceArret == other.Sequence.ToString() &&
               DistanceVoyage == other.Distance.ToString();
    }

    /// <summary>
    ///     Equalses the specified other.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns></returns>
    protected bool Equals(DtlkHabillage other)
    {
        return VoyageId == other.VoyageId && Arrivee.Equals(other.Arrivee) && Depart.Equals(other.Depart) &&
               ArretId == other.ArretId && SequenceArret == other.SequenceArret && TypeMontee == other.TypeMontee &&
               TypeDescente == other.TypeDescente && DistanceVoyage == other.DistanceVoyage;
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
        return Equals((DtlkHabillage)obj);
    }

    /// <summary>
    ///     Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(VoyageId, Arrivee, Depart, ArretId, SequenceArret, TypeMontee, TypeDescente,
            DistanceVoyage);
    }
}