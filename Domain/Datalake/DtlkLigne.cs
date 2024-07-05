using System.Text.Json.Serialization;

namespace GtfsDtlk_ToDtwh.Domain.Datalake;

public class DtlkLigne
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DtlkLigne"/> class.
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
    /// Initializes a new instance of the <see cref="DtlkLigne"/> class.
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

}