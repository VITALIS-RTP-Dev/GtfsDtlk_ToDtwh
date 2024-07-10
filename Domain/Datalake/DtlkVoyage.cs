using GtfsDtlk_ToDtwh.Domain.Datawarehouse;

namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkVoyage : IEquatable<DtwhCourse>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkVoyage" /> class.
    /// </summary>
    public DtlkVoyage()
    {
        Id = 0;
        LigneId = 0;
        ServiceId = 0;
        LibelleAffichage = string.Empty;
        DirectionId = string.Empty;
        BlocId = string.Empty;
        GraphicageId = 0;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkVoyage" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="ligneId">The ligne identifier.</param>
    /// <param name="serviceId">The service identifier.</param>
    /// <param name="libelleAffichage">The libelle affichage.</param>
    /// <param name="directionId">The direction identifier.</param>
    /// <param name="blocId">The bloc identifier.</param>
    /// <param name="graphicageId">The graphicage identifier.</param>
    public DtlkVoyage(long id, long ligneId, long serviceId, string libelleAffichage, string directionId,
        string blocId, long graphicageId)
    {
        Id = id;
        LigneId = ligneId;
        ServiceId = serviceId;
        LibelleAffichage = libelleAffichage;
        DirectionId = directionId;
        BlocId = blocId;
        GraphicageId = graphicageId;
    }

    public long Id { get; set; }
    public long LigneId { get; set; }
    public long ServiceId { get; set; }
    public string LibelleAffichage { get; set; }
    public string DirectionId { get; set; }
    public string BlocId { get; set; }
    public long GraphicageId { get; set; }

    /// <summary>
    ///     Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///     <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public bool Equals(DtwhCourse other)
    {
        return Id == other.Id &&
               LigneId == other.LigneId &&
               LibelleAffichage == other.Libelle &&
               DirectionId == other.Direction.ToString();
    }

    /// <summary>
    ///     Equalses the specified other.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns></returns>
    protected bool Equals(DtlkVoyage other)
    {
        return Id == other.Id && LigneId == other.LigneId && ServiceId == other.ServiceId &&
               LibelleAffichage == other.LibelleAffichage && DirectionId == other.DirectionId &&
               BlocId == other.BlocId && GraphicageId == other.GraphicageId;
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
        return Equals((DtlkVoyage)obj);
    }

    /// <summary>
    ///     Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, LigneId, ServiceId, LibelleAffichage, DirectionId, BlocId, GraphicageId);
    }
}