using Module=Autofac.Module; 
using EnterpriseMagnet.Service.Concrete.Mapping;
using System.Reflection;
using Autofac;
using EnterpriseMagnet.Repository.Concrete;
using EnterpriseMagnet.Repository.Abstract;
using EnterpriseMagnet.Service.Concrete.Services;
using EnterpriseMagnet.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using EnterpriseMagnet.DataAccess.Abstract.UnitOfWorks;
using EnterpriseMagnet.Service.Abstract.Services;
using System;
using EnterpriseMagnet.Entities.Concrete;

namespace EnterpriseMagnet.WebAPI.Modules
{
    public class RepoServiceModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();


            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope(); 

            builder.RegisterType<UnitOfWork>().As<IUnitOfWorks>();


            var apiAssembly = Assembly.GetExecutingAssembly();

            var repoAssembly = Assembly.GetAssembly(typeof(EnterpriseMagnetDbContext));

            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
