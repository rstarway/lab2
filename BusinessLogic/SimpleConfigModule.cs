using DataAccessLayer;
using Model;
using Ninject.Modules;

namespace BusinessLogic
{
    /// <summary>
    /// Настройки контейнера: какие репозитории подставлять в Logic и CellService.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Связывает интерфейсы репозиториев с реализациями. Каждый объект один на всю программу (Singleton).
        /// Чтобы перейти на Dapper, замените EntityReadRepository и EntityWriteRepository на DapperReadRepository и DapperWriteRepository.
        /// </summary>
        public override void Load()
        {
            Bind<IReadRepository<Record>>().To<EntityReadRepository<Record>>().InSingletonScope();
            Bind<IWriteRepository<Record>>().To<EntityWriteRepository<Record>>().InSingletonScope();
            Bind<IRepository<Record>>().To<Repository<Record>>().InSingletonScope();
        }
    }
}
