using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Example.Repository.Common;

namespace Example.Repository
{
    public class DefaultRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultRepository>().As<IDefaultRepository>();
        }
    }
}
