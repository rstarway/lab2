using DataAccessLayer;
using Model;
using Ninject.Modules;

namespace BusinessLogic
{
    /// <summary>
    /// Настройки контейнера: какой репозиторий подставлять в Logic.
    /// </summary>
    public class ConfigModule : NinjectModule
    {
        /// <summary>
        /// Связывает интерфейс репозитория с реализацией. Объект репозитория один на всю программу (Singleton).
        /// </summary>
        public override void Load()
        {
            Bind<IRepository<Record>>().To<EntityRepository<Record>>().InSingletonScope();
            // Bind<IRepository<Record>>().To<DapperRepository>().InSingletonScope();
        }
    }
}
