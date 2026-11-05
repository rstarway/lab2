using DataAccessLayer;
using Model;

namespace BusinessLogic
{
    /// <summary>
    /// Бизнес-логика учета поступлений: работает с записями через репозиторий.
    /// </summary>
    public class Logic
    {
        IRepository<Record> repository;
        CellService cellService;
        RecordValidator validator;

        /// <summary>
        /// Получает репозиторий и классы проверки из контейнера.
        /// </summary>
        /// <param name="repository">Репозиторий записей</param>
        /// <param name="cellService">Проверка и подсчет камер</param>
        /// <param name="validator">Проверка полей записи</param>
        public Logic(IRepository<Record> repository, CellService cellService, RecordValidator validator)
        {
            this.repository = repository;
            this.cellService = cellService;
            this.validator = validator;
        }

        /// <summary>
        /// Общее количество холодильных камер (номера от 1 до Cells).
        /// </summary>
        public int Cells { get; } = 10;

        /// <summary>
        /// Добавляет новую запись о поступлении.
        /// </summary>
        /// <param name="name">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="date">Дата поступления</param>
        /// <param name="cause">Причина смерти</param>
        /// <param name="cell">Номер камеры</param>
        /// <returns>true, если запись добавлена.</returns>
        public bool Add(string name, int age, DateTime date, string cause, int cell)
        {
            if (validator.Check(name, age, date, cause) != "")
                return false;
            if (!cellService.CanUse(cell, 0, Cells))
                return false;

            Record r = new Record();
            r.Name = name;
            r.Age = age;
            r.Date = date;
            r.Cause = cause;
            r.Cell = cell;
            return repository.Add(r);
        }

        /// <summary>
        /// Возвращает список всех записей.
        /// </summary>
        public List<Record> GetAll()
        {
            return repository.ReadAll();
        }

        /// <summary>
        /// Возвращает запись с указанным id или null, если такой нет.
        /// </summary>
        public Record GetById(int id)
        {
            return repository.ReadById(id);
        }

        /// <summary>
        /// Изменяет запись с указанным id.
        /// </summary>
        /// <param name="id">Номер изменяемой записи</param>
        /// <param name="name">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="date">Дата поступления</param>
        /// <param name="cause">Причина смерти</param>
        /// <param name="cell">Номер камеры</param>
        /// <returns>true, если запись изменена.</returns>
        public bool Edit(int id, string name, int age, DateTime date, string cause, int cell)
        {
            if (validator.Check(name, age, date, cause) != "")
                return false;
            if (!cellService.CanUse(cell, id, Cells))
                return false;

            Record r = repository.ReadById(id);
            if (r == null)
                return false;
            r.Name = name;
            r.Age = age;
            r.Date = date;
            r.Cause = cause;
            r.Cell = cell;
            return repository.Update(r);
        }

        /// <summary>
        /// Удаляет запись с указанным id.
        /// </summary>
        /// <param name="id">Номер удаляемой записи</param>
        /// <returns>true, если запись удалена.</returns>
        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        /// <summary>
        /// Бизнес-функция 1: поиск записей по причине смерти (часть слова, без учета регистра).
        /// </summary>
        /// <param name="cause">Текст для поиска</param>
        /// <returns>Список найденных записей.</returns>
        public List<Record> FindByCause(string cause)
        {
            if (cause == null)
                cause = "";
            List<Record> list = repository.ReadAll();
            List<Record> res = new List<Record>();
            foreach (Record r in list)
            {
                if (r.Cause != null && r.Cause.ToLower().Contains(cause.ToLower()))
                    res.Add(r);
            }
            return res;
        }

        /// <summary>
        /// Бизнес-функция 2: номера занятых камер по возрастанию.
        /// </summary>
        public List<int> BusyCells()
        {
            return cellService.BusyCells();
        }

        /// <summary>
        /// Бизнес-функция 2: номера свободных камер по возрастанию.
        /// </summary>
        public List<int> FreeCells()
        {
            return cellService.FreeCells(Cells);
        }
    }
}
