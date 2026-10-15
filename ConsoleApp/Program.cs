using BusinessLogic;
using Model;

Logic logic = new Logic();

while (true)
{
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

    if (s == null || s == "0")
        break;

    switch (s.Trim())
    {
        case "1":
            Console.Write("ФИО: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("ФИО не может быть пустым");
                break;
            }
            Console.Write("Возраст: ");
            int age;
            if (!int.TryParse(Console.ReadLine(), out age) || age < 0 || age > 120)
            {
                Console.WriteLine("Возраст должен быть числом от 0 до 120");
                break;
            }
            Console.Write("Дата (дд.мм.гггг): ");
            DateTime date;
            if (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Неправильная дата");
                break;
            }
            Console.Write("Причина смерти: ");
            string cause = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cause))
            {
                Console.WriteLine("Причина не может быть пустой");
                break;
            }
            Console.Write("Камера (1-" + logic.cells + "): ");
            int cell;
            if (!int.TryParse(Console.ReadLine(), out cell))
            {
                Console.WriteLine("Камера должна быть числом");
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
            if (string.IsNullOrWhiteSpace(name2))
            {
                Console.WriteLine("ФИО не может быть пустым");
                break;
            }
            Console.Write("Возраст: ");
            int age2;
            if (!int.TryParse(Console.ReadLine(), out age2) || age2 < 0 || age2 > 120)
            {
                Console.WriteLine("Возраст должен быть числом от 0 до 120");
                break;
            }
            Console.Write("Дата (дд.мм.гггг): ");
            DateTime date2;
            if (!DateTime.TryParse(Console.ReadLine(), out date2))
            {
                Console.WriteLine("Неправильная дата");
                break;
            }
            Console.Write("Причина смерти: ");
            string cause2 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cause2))
            {
                Console.WriteLine("Причина не может быть пустой");
                break;
            }
            Console.Write("Камера: ");
            int cell2;
            if (!int.TryParse(Console.ReadLine(), out cell2))
            {
                Console.WriteLine("Камера должна быть числом");
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
