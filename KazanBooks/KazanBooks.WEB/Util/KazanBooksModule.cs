using Ninject.Modules;
using KazanBooks.BAL.Services;
using KazanBooks.BAL.Interfaces;

namespace KazanBooks.WEB.Util
{
    public class KazanBooksModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IKazanBooksService>().To<KazanBooksService>();
        }
    }
}
