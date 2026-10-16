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

        /// <summary>
        /// Создает репозиторий для работы с записями.
        /// </summary>
        public Logic()
        {
            repository = new EntityRepository<Record>();
            // repository = new DapperRepository();
        }

        /// <summary>
        /// Общее количество холодильных камер (номера от 1 до cells).
        /// </summary>
        public int cells = 10;

        /// <summary>
        /// Добавляет новую запись о поступлении.
        /// </summary>
        /// <param name="name">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="date">Дата поступления</param>
        /// <param name="cause">Причина смерти</param>
        /// <param name="cell">Номер камеры</param>
        /// <returns>true, если запись добавлена; false, если камера занята или такой камеры нет.</returns>
        public bool Add(string name, int age, DateTime date, string cause, int cell)
        {
            if (cell < 1 || cell > cells)
                return false;
            List<Record> list = repository.ReadAll();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Cell == cell)
                    return false;
            }

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
        /// Изменяет запись с указанным id.
        /// </summary>
        /// <param name="id">Номер изменяемой записи</param>
        /// <param name="name">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="date">Дата поступления</param>
        /// <param name="cause">Причина смерти</param>
        /// <param name="cell">Номер камеры</param>
        /// <returns>true, если запись изменена; false, если записи нет, камера занята другой записью или такой камеры нет.</returns>
        public bool Edit(int id, string name, int age, DateTime date, string cause, int cell)
        {
            if (cell < 1 || cell > cells)
                return false;
            List<Record> list = repository.ReadAll();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Cell == cell && list[i].ID != id)
                    return false;
            }

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
        /// <returns>true, если запись удалена; false, если такой записи нет.</returns>
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
            List<Record> list = repository.ReadAll();
            List<Record> res = new List<Record>();
            foreach (Record r in list)
            {
                if (r.Cause.ToLower().Contains(cause.ToLower()))
                    res.Add(r);
            }
            return res;
        }

        /// <summary>
        /// Бизнес-функция 2: номера занятых камер по возрастанию.
        /// </summary>
        public List<int> BusyCells()
        {
            List<Record> list = repository.ReadAll();
            List<int> res = new List<int>();
            foreach (Record r in list)
                res.Add(r.Cell);
            res.Sort();
            return res;
        }

        /// <summary>
        /// Бизнес-функция 2: номера свободных камер по возрастанию.
        /// </summary>
        public List<int> FreeCells()
        {
            List<Record> list = repository.ReadAll();
            List<int> res = new List<int>();
            for (int i = 1; i <= cells; i++)
            {
                bool busy = false;
                foreach (Record r in list)
                {
                    if (r.Cell == i)
                        busy = true;
                }
                if (!busy)
                    res.Add(i);
            }
            return res;
        }
    }
}
