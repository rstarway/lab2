using BusinessLogic;
using Model;
using Ninject;

ILogic logic;
RecordValidator validator;
try
{
    IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
    logic = ninjectKernel.Get<ILogic>();
    validator = ninjectKernel.Get<RecordValidator>();
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
            Print(r);
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
                Record newRecord = AskRecord();
                if (newRecord == null)
                    break;
                if (logic.Add(newRecord.Name, newRecord.Age, newRecord.Date, newRecord.Cause, newRecord.Cell))
                    Console.WriteLine("Добавлено");
                else
                    Console.WriteLine("Камера занята или нет такой");
                break;

            case "2":
                int? id = AskNumber("id: ", "id должен быть числом");
                if (id == null)
                    break;
                Record changed = AskRecord();
                if (changed == null)
                    break;
                if (logic.Edit(id.Value, changed.Name, changed.Age, changed.Date, changed.Cause, changed.Cell))
                    Console.WriteLine("Изменено");
                else
                    Console.WriteLine("Не получилось");
                break;

            case "3":
                int? deleteId = AskNumber("id: ", "id должен быть числом");
                if (deleteId == null)
                    break;
                if (logic.Delete(deleteId.Value))
                    Console.WriteLine("Удалено");
                else
                    Console.WriteLine("Нет такой записи");
                break;

            case "4":
                Console.Write("Причина: ");
                string str = Console.ReadLine();
                if (str == null)
                    str = "";
                List<Record> found = logic.FindByCause(str.Trim());
                if (found.Count == 0)
                    Console.WriteLine("Ничего не найдено");
                foreach (Record r in found)
                    Print(r);
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

void Print(Record r)
{
    Console.WriteLine(r.ID + " | " + r.Name + " | " + r.Age + " | " + r.Date.ToShortDateString() + " | " + r.Cause + " | камера " + r.Cell);
}

// Спрашивает число. Если ввели не число, пишет ошибку и возвращает null.
int? AskNumber(string question, string error)
{
    Console.Write(question);
    int n;
    if (!int.TryParse(Console.ReadLine(), out n))
    {
        Console.WriteLine(error);
        return null;
    }
    return n;
}

// Спрашивает все поля записи. Если что-то введено неверно, пишет ошибку и возвращает null.
Record AskRecord()
{
    Record r = new Record();
    Console.Write("ФИО: ");
    r.Name = Console.ReadLine();

    int? age = AskNumber("Возраст: ", "Возраст должен быть числом");
    if (age == null)
        return null;
    r.Age = age.Value;

    Console.Write("Дата (дд.мм.гггг): ");
    DateTime date;
    if (!DateTime.TryParse(Console.ReadLine(), out date))
    {
        Console.WriteLine("Неправильная дата");
        return null;
    }
    r.Date = date;

    Console.Write("Причина смерти: ");
    r.Cause = Console.ReadLine();

    int? cell = AskNumber("Камера (1-" + logic.Cells + "): ", "Камера должна быть числом");
    if (cell == null)
        return null;
    r.Cell = cell.Value;

    string err = validator.Check(r.Name, r.Age, r.Date, r.Cause);
    if (err != "")
    {
        Console.WriteLine(err);
        return null;
    }
    r.Name = r.Name.Trim();
    r.Cause = r.Cause.Trim();
    return r;
}
