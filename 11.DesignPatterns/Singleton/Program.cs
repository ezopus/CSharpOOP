using SingletonPattern;

//calling instance 4 times but is only initialized once
var db = SingletonDataContainer.Instance;

var db2 = SingletonDataContainer.Instance;

var db3 = SingletonDataContainer.Instance;

var db4 = SingletonDataContainer.Instance;

Console.WriteLine(db.GetPopulation("Sofia"));

Console.WriteLine(db4.GetPopulation("Washington"));

