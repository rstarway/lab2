using BusinessLogic;
using Model;

Logic logic;
RecordValidator validator;
try
{
    logic = IoC.GetLogic();
    validator = IoC.GetValidator();
}
catch (Exception e)
{
    Console.WriteLine("Не удалось открыть базу: " + e.Message);
    Console.ReadLine();
    return;
}

while (true)
{
    try
    {
        if (!Console.IsOutputRedirected)
            Console.Clear();
        Console.WriteLine("===== Учет поступлений в морг =====");
        Console.WriteLine();
        List<Record> list = logic.GetAll();
        if (list.Count == 0)
            Console.WriteLine("Записей нет");
        foreach (Record r in list)
            Console.WriteLine(r.ID + " | " + r.Name + " | " + r.Age + " | " + r.Date.ToShortDateString() + " | " + r.Cause + " | камера " + r.Cell);
        Console.WriteLine();
        Console.WriteLine("Свободные камеры: " + string.Join(", ", logic.FreeCells()));
        Console.WriteLine("===================================");
        Console.WriteLine("1 - добавить");
        Console.WriteLine("2 - изменить");
        Console.WriteLine("3 - удалить");
        Console.WriteLine("4 - поиск по причине");
        Console.WriteLine("0 - выход");
        Console.Write("> ");
        string s = Console.ReadLine();

        if (s == null || s.Trim() == "0")
            break;

        switch (s.Trim())
        {
            case "1":
                Console.Write("ФИО: ");
                string name = Console.ReadLine();
                Console.Write("Возраст: ");
                int age;
                if (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.WriteLine("Возраст должен быть числом");
                    break;
                }
                Console.Write("Дата (дд.мм.гггг): ");
                DateTime date;
                if (!DateTime.TryParse(Console.ReadLine(), out date) || date.Year < 1900)
                {
                    Console.WriteLine("Неправильная дата");
                    break;
                }
                Console.Write("Причина смерти: ");
                string cause = Console.ReadLine();
                Console.Write("Камера (1-" + logic.cells + "): ");
                int cell;
                if (!int.TryParse(Console.ReadLine(), out cell))
                {
                    Console.WriteLine("Камера должна быть числом");
                    break;
                }
                string err = validator.Check(name, age, cause);
                if (err != "")
                {
                    Console.WriteLine(err);
                    break;
                }
                if (logic.Add(name.Trim(), age, date, cause.Trim(), cell))
                    Console.WriteLine("Добавлено");
                else
                    Console.WriteLine("Камера занята или нет такой");
                break;

            case "2":
                Console.Write("id: ");
                int id;
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("id должен быть числом");
                    break;
                }
                Console.Write("ФИО: ");
                string name2 = Console.ReadLine();
                Console.Write("Возраст: ");
                int age2;
                if (!int.TryParse(Console.ReadLine(), out age2))
                {
                    Console.WriteLine("Возраст должен быть числом");
                    break;
                }
                Console.Write("Дата (дд.мм.гггг): ");
                DateTime date2;
                if (!DateTime.TryParse(Console.ReadLine(), out date2) || date2.Year < 1900)
                {
                    Console.WriteLine("Неправильная дата");
                    break;
                }
                Console.Write("Причина смерти: ");
                string cause2 = Console.ReadLine();
                Console.Write("Камера: ");
                int cell2;
                if (!int.TryParse(Console.ReadLine(), out cell2))
                {
                    Console.WriteLine("Камера должна быть числом");
                    break;
                }
                string err2 = validator.Check(name2, age2, cause2);
                if (err2 != "")
                {
                    Console.WriteLine(err2);
                    break;
                }
                if (logic.Edit(id, name2.Trim(), age2, date2, cause2.Trim(), cell2))
                    Console.WriteLine("Изменено");
                else
                    Console.WriteLine("Не получилось");
                break;

            case "3":
                Console.Write("id: ");
                int id2;
                if (!int.TryParse(Console.ReadLine(), out id2))
                {
                    Console.WriteLine("id должен быть числом");
                    break;
                }
                if (logic.Delete(id2))
                    Console.WriteLine("Удалено");
                else
                    Console.WriteLine("Нет такой записи");
                break;

            case "4":
                Console.Write("Причина: ");
                string str = Console.ReadLine();
                if (str == null)
                    str = "";
                List<Record> res = logic.FindByCause(str.Trim());
                if (res.Count == 0)
                    Console.WriteLine("Ничего не найдено");
                foreach (Record r in res)
                    Console.WriteLine(r.ID + " | " + r.Name + " | " + r.Age + " | " + r.Date.ToShortDateString() + " | " + r.Cause + " | камера " + r.Cell);
                break;

            default:
                Console.WriteLine("Нет такого пункта");
                break;
        }

        Console.WriteLine();
        Console.Write("Нажми Enter...");
        Console.ReadLine();
    }
    catch (Exception e)
    {
        Console.WriteLine();
        Console.WriteLine("Что-то пошло не так: " + e.Message);
        Console.Write("Enter - попробовать еще раз, 0 - выход: ");
        string a = Console.ReadLine();
        if (a == null || a.Trim() == "0")
            break;
    }
}
