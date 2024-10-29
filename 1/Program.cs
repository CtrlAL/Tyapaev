using Newtonsoft.Json;

namespace First
{
	public class Program
	{
		private static int? m;

		private record Settings
		{
			public int? m { get; set; }
		}

		private static int GCD(int a, int b)
		{
			while (b != 0)
			{
				var temp = b;
				b = a % b;
				a = temp;
			}
			return a;
		}
		private static Func<int, int, int, int> Evolution = (a, x, n) => a * (x % n);
		private static IEnumerable<int> GetGroup(int n)
		{
			var ints = Enumerable.Range(1, n).ToList();

			foreach (var num in ints)
			{
				if (GCD(num, n) != 1)
				{
					ints.Remove(num);
				}
			}

			return ints;
		}

		private static IEnumerable<int> GetOrbit(Func<int, int, int, int> evolution, int a, int n)
		{
			var ints = new List<int>();

			List<int> Func(int x , List<int> ints)
			{
				ints.Append(a);
				x = evolution(a, x, n);

				if (!ints.Contains(x))
				{
					return Func(x, ints);				
				}
				return ints;
			}

			return Func(1, ints);
		}

		static Program(){
			var text = File.ReadAllText("..\\..\\..\\config.json");
			var settings = JsonConvert.DeserializeObject<Settings>(text);
			m = settings?.m;
		}
		static void Main(string[] args)
		{
			decimal S = default;
			decimal R = 0;
			if (m != null)
			{
				int n = (m.Value % 47) + 47;
				int a = default;
				var group = GetGroup(n).ToList();
				int t = group.Count();

				if (int.TryParse(Console.ReadLine(), out a))
				{
					if (GCD(a,n) == 1)
					{
						var orbit = GetOrbit(Evolution, a, n).ToList();
						int fn = orbit.Count();

						for (int i = 0; i < orbit.Count() -1; i++)
						{
							R += (group.IndexOf(orbit[i]) - group.IndexOf(orbit[i + 1])) 
								* (group.IndexOf(orbit[i]) - group.IndexOf(orbit[i + 1]));
						}

						R += (group.Count() - group.IndexOf(orbit.Last()))
								* (group.Count() - group.IndexOf(orbit.Last()));

						S = (R * t) / (fn * fn);
						Console.WriteLine(S);
					}
				}
			}
		}
	}
}
