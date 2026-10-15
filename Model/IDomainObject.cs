namespace Model
{
    /// <summary>
    /// Объект предметной области, у которого есть номер ID.
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// Уникальный номер объекта (ключ в базе).
        /// </summary>
        int ID { get; set; }
    }
}
