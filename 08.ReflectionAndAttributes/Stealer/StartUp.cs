using Stealer;

Spy spy = new Spy();
string result = spy.StealFieldInfo("Stealer.Hacker", "username", "password");
Console.WriteLine(result);
Console.WriteLine("--------------------");

result = spy.AnalyzeAccessModifiers("Stealer.Hacker");
Console.WriteLine(result);
Console.WriteLine("--------------------");

result = spy.RevealPrivateMethods("Stealer.Hacker");
Console.WriteLine(result);
Console.WriteLine("--------------------");

result = spy.CollectGettersAndSetters("Stealer.Hacker");
Console.WriteLine(result);