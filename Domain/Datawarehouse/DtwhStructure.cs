namespace GtfsDtlk_ToDtwh.Domain.Datawarehouse;

public class DtwhStructure
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DtwhStructure" /> class.
    /// </summary>
    public DtwhStructure()
    {
        CourseId = 0;
        ArretId = 0;
        Sequence = 0;
        Distance = 0;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DtwhStructure" /> class.
    /// </summary>
    /// <param name="courseId">The course identifier.</param>
    /// <param name="arretId">The arret identifier.</param>
    /// <param name="sequence">The sequence.</param>
    /// <param name="distance">The distance.</param>
    public DtwhStructure(long courseId, long arretId, int sequence, int distance)
    {
        CourseId = courseId;
        ArretId = arretId;
        Sequence = sequence;
        Distance = distance;
    }

    public long CourseId { get; set; }
    public long ArretId { get; set; }
    public int Sequence { get; set; }
    public int Distance { get; set; }

    /// <summary>
    ///     Empties this instance.
    /// </summary>
    /// <returns></returns>
    public static DtwhStructure Empty()
    {
        return new DtwhStructure();
    }
}