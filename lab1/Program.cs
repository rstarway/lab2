namespace lab1
{
    /// <summary>
    /// Точка входа приложения с формами.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Запускает главное окно. Если база не открылась, показывает сообщение и выходит.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ShowError;

            Form1 f;
            try
            {
                f = new Form1();
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось открыть базу: " + e.Message, "Ошибка");
                return;
            }
            Application.Run(f);
        }

        static void ShowError(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Что-то пошло не так: " + e.Exception.Message, "Ошибка");
        }
    }
}
