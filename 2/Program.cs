using Newtonsoft.Json;
using System;
using Fractions;

namespace Second
{
	public class Program
	{
		public static void CheckBiectivivy(IEnumerable<(int coefficient, int degree)> polly)
		{
			var resultList = new List<int>();

			foreach (var num in Enumerable.Range(0, 3))
			{
				int result = 0;

				foreach (var item in polly)
				{
					result += item.coefficient * (int)MathF.Pow(num, item.degree);
					result %= 4;
				}

				resultList.Add(result);
			}

			if (resultList.Distinct().Count() == 4)
			{
				Console.WriteLine("f(x) Биективна");
			}
			else
			{
				Console.WriteLine("f(x) Не Биективна");
			}
		}
		public static void CheckTransitivity(IEnumerable<(int coefficient, int degree)> polly)
		{
			var resultList = new List<int>();

			foreach (var num in Enumerable.Range(0, 7))
			{
				int result = 0;

				foreach (var item in polly)
				{
					result += item.coefficient * (int)MathF.Pow(num, item.degree);
					result %= 8;
				}

				resultList.Add(result);
			}

			if (resultList.Distinct().Count() == 8)
			{
				Console.WriteLine("f(x) Транзитивна");
			}
			else
			{
				Console.WriteLine("f(x) Не Транизитивна");
			}
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

						if ((int)res.Numerator % 2 == 0)
						{
							coefficentZ2.Add(a);
							resList.Push(res);
							break;
						}
					}
				}

				var value = 0;

				for (int i = 0; i < 2; i++) {
					if (coefficentZ2[i % coefficentZ2.Count()] == 1)
					{
						value += (int)MathF.Pow(2, i);
					}
				}

				item.coefficient = value;

				return item;
			});


			var gMod8 = gx.Select(item =>
			{
				int p = 2;
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
					if (coefficentZ2[i%coefficentZ2.Count()] == 1)
					{
						value += (int)MathF.Pow(2, i);
					}
				}

				item.coefficient = value;

				return item;
			});


			CheckBiectivivy(fMod4);
			CheckBiectivivy(gMod4.Select(item =>
			{
				return ((int)item.coefficient, item.degree);
			}));

			CheckTransitivity(fMod8);
			CheckTransitivity(gMod8.Select(item =>
			{
				return ((int)item.coefficient, item.degree);
			}));

		}
	}
}
