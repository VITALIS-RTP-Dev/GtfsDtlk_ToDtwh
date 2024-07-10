namespace GtfsDtlk_ToDtwh.Domain.Datawarehouse;

public class DtwhCourse
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtwhCourse" /> class.
    /// </summary>
    public DtwhCourse()
    {
        Id = 0;
        LigneId = 0;
        Direction = 0;
        Libelle = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DtwhCourse" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="ligneId">The ligne identifier.</param>
    /// <param name="direction">The direction.</param>
    /// <param name="libelle">The libelle.</param>
    public DtwhCourse(long id, long ligneId, int direction, string libelle)
    {
        Id = id;
        LigneId = ligneId;
        Direction = direction;
        Libelle = libelle;
    }

    public long Id { get; set; }
    public long LigneId { get; set; }
    public int Direction { get; set; }
    public string Libelle { get; set; }

    /// <summary>
    ///     Empties this instance.
    /// </summary>
    /// <returns></returns>
    public static DtwhCourse Empty()
    {
        return new DtwhCourse();
    }
}