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

		private static IEnumerable<int> GetSeq(Func<int, int, int, int> evolution)
		{
			var ints = new List<int>();

			return ints;
		}

		private static int GetSeqT(IEnumerable<int> seq)
		{
			return 1;
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
				
				int t = 1;
				int n = (m.Value % 47) + 47;
				var x = Enumerable.Range(0, t).ToArray();
				foreach (var index in Enumerable.Range(1, t))
				{
					result += (x[index] * x[index]);
				}

			}
		}
	}
}
