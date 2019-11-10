using BadgerRank.Heart;
using BadgerRank.Heart.Games;
using BadgerRank.Heart.Test;
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

            //var abstractionFactory = Program.container.GetInstance<IAbstraction>();
            //Console.WriteLine(abstractionFactory.Gib().ToString());
            //Console.WriteLine(abstractionFactory.Gib().ToString());

            //var abstractionFactory2 = Program.container.GetInstance<IAbstraction>();
            //Console.WriteLine(abstractionFactory2.Gib().ToString());
            //Console.WriteLine(abstractionFactory2.Gib().ToString());

            var output = await ranker.Rank();

            Console.WriteLine(output);
            Console.ReadLine();
        }
    }
}
