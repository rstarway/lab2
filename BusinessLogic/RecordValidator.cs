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
        public bool Check(string name, int age, string cause)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(cause))
                return false;
            if (age < 0 || age > 120)
                return false;
            return true;
        }
    }
}
