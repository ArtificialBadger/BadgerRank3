using BadgerRank.Heart;
using SimpleInjector;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BadgerRank
{
    class Program
    {
        private static readonly Container container;

        static Program()
        {
            container = new Container();

            Registrations.Register(container);
        }

        public static async Task Main(string[] args)
        {
            var ranker = Program.container.GetInstance<Ranker>();

            await ranker.Rank();
        }
    }
}
