using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Third
{
	public class Program
	{
		public static int Function(int x) => (x ^ 1) ^ (2 * (x &(1 + 2*x) & (3 + 4*4) & (7 + 8*x) & (15 + 16*x) & (31 + 32*x) & (63 + 64*x))) ^ (4 * ((int)Math.Pow(x, 2) + 5));
	
		public static void CheckTransitivity(Func<int, int> function, int p)
		{
			var list = new HashSet<int>();
			var elem = 0;

			foreach (int i in Enumerable.Range(0, p))
			{
				elem = function(elem);

				if (!list.Contains(elem))
				{
					list.Add(elem);
				}
				else
				{
					if (list.Count == p)
					{
						Console.WriteLine("Функция транзитивна");
					}
					else
					{
						Console.WriteLine("Функция не транзитивна");
					}

				}
			}
		}

		static void Main(string[] args)
		{
			CheckTransitivity((int x) => Function(x), 256);
		}
	}
}
