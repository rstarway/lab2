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
    }
}
