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

		private static IEnumerable<int> GetSeq(Func<int, int, int, int> evolution, int a, int n)
		{
			var ints = Enumerable.Range(1, n).ToList();

			foreach (var num in ints)
			{
				if (GCD(num, n) != 1)
				{
					ints.Remove(num);
				}
			}

			ints = ints.Select(x => evolution(a,x,n)).ToList();

			return ints;
		}

		private static Func<int, int, int, int> Evolution = (a, x, n) => a * (x % n); 
	
		static Program(){
			var text = File.ReadAllText("..\\..\\..\\config.json");
			var settings = JsonConvert.DeserializeObject<Settings>(text);
			m = settings?.m;
		}
		static void Main(string[] args)
		{
			if (m != null)
			{
				
				decimal result = default;
				int n = (m.Value % 47) + 47;
				var seq = GetSeq(Evolution, 1, n).ToList();

				foreach (var x in seq)
				{
					result += x * x;
				}
				//Quad sum divide by Fi(n); Is Correct , now is trash
				Console.WriteLine(result * seq.Count);
			}
		}
	}
}
