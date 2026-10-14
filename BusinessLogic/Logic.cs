namespace Model
{
    public class Logic
    {
        List<Record> list = new List<Record>();
        int next = 1;

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
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Cell == cell)
                    return false;
            }

            Record r = new Record();
            r.Id = next;
            r.Name = name;
            r.Age = age;
            r.Date = date;
            r.Cause = cause;
            r.Cell = cell;
            list.Add(r);
            next++;
            return true;
        }

        /// <summary>
        /// Возвращает список всех записей.
        /// </summary>
        public List<Record> GetAll()
        {
            return list;
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
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Cell == cell && list[i].Id != id)
                    return false;
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == id)
                {
                    list[i].Name = name;
                    list[i].Age = age;
                    list[i].Date = date;
                    list[i].Cause = cause;
                    list[i].Cell = cell;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Удаляет запись с указанным id.
        /// </summary>
        /// <param name="id">Номер удаляемой записи</param>
        /// <returns>true, если запись удалена; false, если такой записи нет.</returns>
        public bool Delete(int id)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == id)
                {
                    list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Бизнес-функция 1: поиск записей по причине смерти (часть слова, без учета регистра).
        /// </summary>
        /// <param name="cause">Текст для поиска</param>
        /// <returns>Список найденных записей.</returns>
        public List<Record> FindByCause(string cause)
        {
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
