using System.Data.SQLite;

namespace DataAccessLayer
{
    /// <summary>
    /// Общие настройки базы SQLite для EF и Dapper.
    /// </summary>
    public static class DBConnection
    {
        static string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "lab1");
        static bool ready = false;

        /// <summary>
        /// Строка подключения к файлу %LOCALAPPDATA%\lab1\morg.db.
        /// </summary>
        public static string str = "Data Source=" + Path.Combine(folder, "morg.db");

        /// <summary>
        /// Создает папку, файл базы и таблицу Records, если их еще нет.
        /// </summary>
        public static void CreateTable()
        {
            if (ready)
                return;

            Directory.CreateDirectory(folder);
            using (SQLiteConnection con = new SQLiteConnection(str))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand("create table if not exists Records (ID integer primary key autoincrement, Name text, Age integer not null, Date datetime not null, Cause text, Cell integer not null)", con);
                cmd.ExecuteNonQuery();
            }
            ready = true;
        }
    }
}
