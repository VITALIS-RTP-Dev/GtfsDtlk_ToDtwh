using GtfsDtlk_ToDtwh.Domain.Datalake;

namespace GtfsDtlk_ToDtwh.Domain.Datawarehouse;

public class DtwhLigne
{
    /// <summary>
    /// Empties this instance.
    /// </summary>
    /// <returns></returns>
    public static DtwhLigne Empty()
    {
        return new DtwhLigne(0,0, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DtwhLigne"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="ligneTypeId">The ligne type identifier.</param>
    /// <param name="libelle1">The libelle1.</param>
    /// <param name="libelle2">The libelle2.</param>
    /// <param name="description">The description.</param>
    /// <param name="type">The type.</param>
    /// <param name="couleur">The couleur.</param>
    /// <param name="couleurTexte">The couleur texte.</param>
    public DtwhLigne(long id, long code,long ligneTypeId, string libelle1, string libelle2, string description, string type,
        string couleur, string couleurTexte)
    {
        Id = id;
        Code = code;
        LigneTypeId = ligneTypeId;
        Libelle1 = libelle1;
        Libelle2 = libelle2;
        Description = description;
        Type = type;
        Couleur = couleur;
        CouleurTexte = couleurTexte;
    }

    public long Id { get; set; }
    public long Code { get; set; }
    public long LigneTypeId { get; set; }
    public string Libelle1 { get; set; }
    public string Libelle2 { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Couleur { get; set; }
    public string CouleurTexte { get; set; }

    /// <summary>
    /// Compares and copy.
    /// </summary>
    /// <param name="ligne">The ligne.</param>
    public void CompareAndCopy(DtlkLigne ligne)
    {
        if (this.Code != ligne.Id) this.Code = ligne.Id;
        if (this.LigneTypeId != ligne.AgenceId) this.LigneTypeId = ligne.AgenceId;
        if (this.Libelle1 != ligne.Libelle1) this.Libelle1 = ligne.Libelle1;
        if (this.Libelle2 != ligne.Libelle2) this.Libelle2 = ligne.Libelle2;
        if (this.Description != ligne.Description) this.Description = ligne.Description;
        if (this.Type != ligne.Type) this.Type = ligne.Type;
        if (this.Couleur != ligne.Couleur) this.Couleur = ligne.Couleur;
        if (this.CouleurTexte != ligne.CouleurTexte) this.CouleurTexte = ligne.CouleurTexte;
    }

}