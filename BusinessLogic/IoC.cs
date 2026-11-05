using Ninject;

namespace BusinessLogic
{
    /// <summary>
    /// IoC-контейнер слоя бизнес-логики.
    /// </summary>
    public static class IoC
    {
        static IKernel kernel = new StandardKernel(new ConfigModule());

        /// <summary>
        /// Создает Logic и подставляет в него репозиторий из контейнера.
        /// </summary>
        public static Logic GetLogic()
        {
            return kernel.Get<Logic>();
        }

        /// <summary>
        /// Возвращает проверку полей записи для форм и консоли.
        /// </summary>
        public static RecordValidator GetValidator()
        {
            return kernel.Get<RecordValidator>();
        }
    }
}
