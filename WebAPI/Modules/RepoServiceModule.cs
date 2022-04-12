using Autofac;
using Caching;
using Core.Repositorys.Interface;
using Core.Services.Interface;
using Core.UnitOfWork.Interface;
using Data;
using Data.Repository;
using Data.UnirtOfWork;
using Service.Mapping;
using Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace WebAPI.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            var apiAssmbley = Assembly.GetExecutingAssembly();
            var RepoAssmbley = Assembly.GetAssembly(typeof(Context));
            var serviceAssmbley = Assembly.GetAssembly(typeof(MapProfile));
            

            builder.RegisterAssemblyTypes(apiAssmbley,RepoAssmbley,serviceAssmbley).Where(x=>x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssmbley,RepoAssmbley,serviceAssmbley).Where(x=>x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();



        }
    }
}
