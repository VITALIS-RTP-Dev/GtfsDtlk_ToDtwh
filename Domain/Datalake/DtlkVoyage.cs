namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkVoyage
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
}