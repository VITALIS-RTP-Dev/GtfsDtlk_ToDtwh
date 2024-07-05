namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkHabillage
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtlkHabillage" /> class.
    /// </summary>
    public DtlkHabillage()
    {
        VoyageId = 0;
        Arrivee = TimeOnly.MinValue;
        Depart = TimeOnly.MinValue;
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
    public DtlkHabillage(long voyageId, TimeOnly arrivee, TimeOnly depart, long arretId, string sequenceArret,
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
    public TimeOnly Arrivee { get; set; }
    public TimeOnly Depart { get; set; }
    public long ArretId { get; set; }
    public string SequenceArret { get; set; }
    public string TypeMontee { get; set; }
    public string TypeDescente { get; set; }
    public string DistanceVoyage { get; set; }
}