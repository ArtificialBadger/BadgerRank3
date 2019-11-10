using BadgerRank.Heart;
using BadgerRank.Heart.Drives;
using BadgerRank.Heart.Games;
using BadgerRank.Heart.Teams;
using BadgerRank.Heart.Test;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadgerRank
{
    public static class Registrations
    {
        public static void Register(Container container)
        {
            container.Options.DefaultLifestyle = Lifestyle.Transient;

            container.Register<IGameResolver, GameResolver>();
            container.Register<ITeamResolver, TeamResolver>();
            container.Register<IDriveResolver, DriveResolver>();
            container.Register<ICfbApiClientFactory, CfbApiClientFactory>();
            container.Register<IAbstraction, Implementation>();

            container.RegisterInstance<Func<IAbstraction>>(() => container.GetInstance<IAbstraction>());
        }
    }
}
