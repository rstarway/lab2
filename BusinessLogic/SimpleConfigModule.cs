using DataAccessLayer;
using Model;
using Ninject.Modules;

namespace BusinessLogic
{
    /// <summary>
    /// Настройки контейнера: какой репозиторий подставлять в Logic.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Связывает интерфейс репозитория с реализацией. Объект репозитория один на всю программу (Singleton). Чтобы перейти на Dapper, замените EntityRepository<Record> на DapperRepository.
        /// </summary>
        public override void Load()
        {
            Bind<IRepository<Record>, IReadRepository<Record>>().To<EntityRepository<Record>>().InSingletonScope();
        }
    }
}
