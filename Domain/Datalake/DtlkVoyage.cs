using System.Text.Json.Serialization;

namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkVoyage
{
    public DtlkVoyage()
    {
        Id = string.Empty;
        LigneId = string.Empty;
        ServiceId = string.Empty;
        LibelleAffichage = string.Empty;
        DirectionId = string.Empty;
        BlocId = string.Empty;
        GraphicageId = string.Empty;
    }

    [JsonConstructor]
    public DtlkVoyage(string id, string ligneId, string serviceId, string libelleAffichage, string directionId,
        string blocId, string graphicageId)
    {
        Id = id;
        LigneId = ligneId;
        ServiceId = serviceId;
        LibelleAffichage = libelleAffichage;
        DirectionId = directionId;
        BlocId = blocId;
        GraphicageId = graphicageId;
    }

    public string Id { get; set; }
    public string LigneId { get; set; }
    public string ServiceId { get; set; }
    public string LibelleAffichage { get; set; }
    public string DirectionId { get; set; }
    public string BlocId { get; set; }
    public string GraphicageId { get; set; }


    [JsonIgnore] public string Creer { get; set; }

    [JsonIgnore] public string Modifier { get; set; }
}