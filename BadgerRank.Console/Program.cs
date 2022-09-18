using BadgerRank;
using BadgerRank.Heart;
using SimpleInjector;

var container = new Container();

Registrations.Register(container);
 
var ranker = container.GetInstance<Ranker>();

var output = await ranker.Rank();

Console.WriteLine(output);
Console.ReadLine();