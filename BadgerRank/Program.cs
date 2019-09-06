using BadgerRank.Heart;
using System;
using System.Text;

namespace BadgerRank
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameResolver = new GameResolver();
            var outputBuilder = new StringBuilder();

            var games = gameResolver.GetGamesForWeek(2019, 1);

            Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
}
