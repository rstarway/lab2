namespace Model
{
    /// <summary>
    /// Запись о поступлении тела в морг.
    /// </summary>
    public class Record : IDomainObject
    {
        /// <summary>
        /// Номер записи.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// ФИО.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Дата поступления.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Причина смерти.
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// Номер холодильной камеры.
        /// </summary>
        public int Cell { get; set; }
    }
}
