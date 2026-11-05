namespace BusinessLogic
{
    /// <summary>
    /// Проверяет обязательные поля записи и возраст.
    /// </summary>
    public class RecordValidator
    {
        /// <summary>
        /// Проверяет, что ФИО и причина заполнены, а возраст от 0 до 120.
        /// </summary>
        /// <returns>Текст ошибки или пустая строка, если все верно.</returns>
        public string Check(string name, int age, string cause)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Заполни ФИО";
            if (string.IsNullOrWhiteSpace(cause))
                return "Заполни причину смерти";
            if (age < 0 || age > 120)
                return "Возраст должен быть от 0 до 120";
            return "";
        }
    }
}
