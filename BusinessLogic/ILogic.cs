using Model;

namespace BusinessLogic
{
    /// <summary>
    /// Бизнес-логика учета поступлений. Слой представления работает только с этим интерфейсом.
    /// </summary>
    public interface ILogic
    {
        /// <summary>
        /// Общее количество холодильных камер (номера от 1 до Cells).
        /// </summary>
        int Cells { get; }

        /// <summary>
        /// Добавляет новую запись о поступлении.
        /// </summary>
        /// <returns>true, если запись добавлена.</returns>
        bool Add(string name, int age, DateTime date, string cause, int cell);

        /// <summary>
        /// Возвращает список всех записей.
        /// </summary>
        List<Record> GetAll();

        /// <summary>
        /// Возвращает запись с указанным id или null, если такой нет.
        /// </summary>
        Record GetById(int id);

        /// <summary>
        /// Изменяет запись с указанным id.
        /// </summary>
        /// <returns>true, если запись изменена.</returns>
        bool Edit(int id, string name, int age, DateTime date, string cause, int cell);

        /// <summary>
        /// Удаляет запись с указанным id.
        /// </summary>
        /// <returns>true, если запись удалена.</returns>
        bool Delete(int id);

        /// <summary>
        /// Ищет записи по причине смерти (часть слова, без учета регистра).
        /// </summary>
        List<Record> FindByCause(string cause);

        /// <summary>
        /// Возвращает номера занятых камер по возрастанию.
        /// </summary>
        List<int> BusyCells();

        /// <summary>
        /// Возвращает номера свободных камер по возрастанию.
        /// </summary>
        List<int> FreeCells();
    }
}
