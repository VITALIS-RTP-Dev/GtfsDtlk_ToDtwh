namespace GtfsDtlk_ToDtwh.Domain.Datawarehouse;

public class DtwhLigne
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtwhLigne" /> class.
    /// </summary>
    public DtwhLigne()
    {
        Id = 0;
        ReseauId = 0;
        Libelle1 = string.Empty;
        Libelle2 = string.Empty;
        Description = string.Empty;
        Type = 0;
        Couleur = string.Empty;
        CouleurTexte = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DtwhLigne" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="reseauId">The ligne type identifier.</param>
    /// <param name="libelle1">The libelle1.</param>
    /// <param name="libelle2">The libelle2.</param>
    /// <param name="description">The description.</param>
    /// <param name="type">The type.</param>
    /// <param name="couleur">The couleur.</param>
    /// <param name="couleurTexte">The couleur texte.</param>
    public DtwhLigne(long id, long reseauId, string libelle1, string libelle2, string description, int type,
        string couleur, string couleurTexte)
    {
        Id = id;
        ReseauId = reseauId;
        Libelle1 = libelle1;
        Libelle2 = libelle2;
        Description = description;
        Type = type;
        Couleur = couleur;
        CouleurTexte = couleurTexte;
    }

    public long Id { get; set; }
    public long ReseauId { get; set; }
    public string Libelle1 { get; set; }
    public string Libelle2 { get; set; }
    public string Description { get; set; }
    public int Type { get; set; }
    public string Couleur { get; set; }
    public string CouleurTexte { get; set; }

    /// <summary>
    ///     Empties this instance.
    /// </summary>
    /// <returns></returns>
    public static DtwhLigne Empty()
    {
        return new DtwhLigne();
    }
}