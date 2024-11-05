using Newtonsoft.Json;
using System;
using Fractions;

namespace Second
{
	public class Program
	{
		private static int? m;

		private record Settings
		{
			public int? m { get; set; }
		}

		static Program()
		{
			var text = File.ReadAllText("..\\..\\..\\config.json");
			var settings = JsonConvert.DeserializeObject<Settings>(text);
			m = settings?.m;
		}

		static void Main(string[] args)
		{

			
			var fx = new List<(int coefficient, int degree)>()
			{
				(12, 0),
				(3, 1),
				(-14,2)
			};

			var gx = new List<(Fraction coefficient, int degree)>()
			{
				(new Fraction(9,7), 1),
				(new Fraction(7,11), 0),
			};

			var gDeg = new List<Fraction>()
			{
				new Fraction(9,7),
				new Fraction(7,11),
			};

			var fMod4 = fx.Select( item =>
			{
				if (item.coefficient > 0)
				{
					item.coefficient = item.coefficient % 4;
				}
				else
				{
					item.coefficient = 4 + item.coefficient % 4;
				}

				return item;
			});

			var fMod8 = fx.Select(item =>
			{
				if (item.coefficient > 0)
				{
					item.coefficient = item.coefficient % 8;
				}
				else
				{
					item.coefficient = 8 + item.coefficient % 8;
				}

				return item;
			});
				
			var gMod4 = gx.Select(item =>
			{
				int p = 2;
				var A = new List<int>{0 , 1};
				var coefficentZ2 = new List<int>();
				var resList = new Stack<Fraction>();
				resList.Push(item.coefficient);

				while (resList.Where(item => item == resList.Peek()).Count() != 2)
				{
					foreach (var a in A)
					{
						var res = resList.Peek() - a;

						if ((int)res.Denominator % 2 == 0)
						{
							coefficentZ2.Add(a);
							resList.Push(res);
							break;
						}
					}
				}

				var value = 0;

				for (int i = 0; i < 2; i++) {
					if (coefficentZ2[i] == 1)
					{
						value += (int)MathF.Pow(2, i);
					}
				}

				item.coefficient = value;

				return item;
			});


			var gMod8 = gx.Select(item =>
			{
				if ((int)item.coefficient.Numerator % (int)item.coefficient.Denominator == 0)
				{
					item.coefficient = (int)item.coefficient.Numerator % (int)item.coefficient.Denominator;
					return item;
				}

				//P is 2
				//r is Numerator
				//s is Denumerator
				//R is Numerator % 2 == 0
				//A is 1 or 0

				var A = new List<int> { 0, 1 };
				var coefficentZ2 = new List<int>();
				var resList = new Stack<Fraction>();
				resList.Push(item.coefficient);

				while (resList.Where(item => item == resList.Peek()).Count() != 2)
				{
					foreach (var a in A)
					{
						var res = resList.Peek() - a;

						if ((int)res.Numerator % 2 == 0)
						{
							coefficentZ2.Add(a);
							resList.Push(res);
							break;
						}
					}
				}

				var value = 0;

				for (int i = 0; i < 3; i++)
				{
					if (coefficentZ2[i] == 1)
					{
						value += (int)MathF.Pow(2, i);
					}
				}

				item.coefficient = value;

				return item;
			});


			foreach(var num in Enumerable.Range(0,8))
			{
				var resultList = new List<int>();

				foreach (var item in fMod4)
				{
					resultList.Add((item.coefficient * (int)MathF.Pow(num, item.degree))%4);
				}

				if(resultList.Distinct().Count() == 4)
				{
					Console.WriteLine("f(x) Биективна");
				}

				resultList.Clear();

				foreach (var item in gMod4)
				{
					resultList.Add(((int)item.coefficient * (int)MathF.Pow(num, item.degree)) % 8);
				}

				if (resultList.Distinct().Count() == 8)
				{
					Console.WriteLine("g(x) Транзитивна");
				}
			}

			foreach(var num in Enumerable.Range(0,8))
			{
				var resultList = new List<int>();

				foreach (var item in fMod8)
				{
					resultList.Add((item.coefficient * (int)MathF.Pow(num, item.degree)) % 4);
				}

				if (resultList.Distinct().Count() == 4)
				{
					Console.WriteLine("f(x) Биективна");
				}

				resultList.Clear();

				foreach (var item in gMod8)
				{
					resultList.Add(((int)item.coefficient * (int)MathF.Pow(num, item.degree)) % 4);
				}

				if (resultList.Distinct().Count() == 8)
				{
					Console.WriteLine("g(x) Транзитивна");
				}
			}
		}
	}
}
