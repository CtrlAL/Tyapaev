using Fractions;
using System.Drawing;
using NPlot;

namespace Fourth
{
	public class Program
	{
		public static int Hx(int x) => (x ^ 1) ^ (2 * (x & (1 + 2 * x) & (3 + 4 * 4) & (7 + 8 * x) & (15 + 16 * x) & (31 + 32 * x) & (63 + 64 * x))) ^ (4 * ((int)Math.Pow(x, 2) + 5));
		public static int Fx(int x) => 12 + 3 * x - 14 * x * x;
		public static Fraction Gx(int x) => new Fraction(9,7) * x + new Fraction(7, 11)* x;

		static void Main(string[] args)
		{
			var k = Enumerable.Range(15, 5);

			var fXDotLists = new List<List<(decimal x, decimal y)>>();
			var hXOfDotLists = new List<List<(decimal x, decimal y)>>();

			foreach (var kVal in k)
			{
				fXDotLists.Add(GetDots(kVal, Fx));
				hXOfDotLists.Add(GetDots(kVal, Hx));
			}

			int i = 0;

			foreach (var list in fXDotLists)
			{
				var linePlot = new PointPlot { DataSource = list.Select(item => item.y).ToList() };
				linePlot.AbscissaData = list.Select(item => item.x).ToList();
				var surface = new NPlot.Bitmap.PlotSurface2D(400, 300);
				surface.BackColor = Color.White;
				surface.Add(linePlot);
				surface.Title = $"Scatter Plot from a Console Application";
				surface.Refresh();
				surface.Bitmap.Save($"C:\\Larcev\\Univesity\\Tyapaev\\4\\Plots\\nplot-fx{i}.png");
				i++;
			}

			i = 0;
			foreach (var list in hXOfDotLists)
			{
				var linePlot = new PointPlot { DataSource = list.Select(item => item.y).ToList() };
				linePlot.AbscissaData = list.Select(item => item.x).ToList();
				var surface = new NPlot.Bitmap.PlotSurface2D(400, 300);
				surface.BackColor = Color.White;
				surface.Add(linePlot);
				surface.Title = $"Scatter Plot from a Console Application";
				surface.Refresh();
				surface.Bitmap.Save($"C:\\Larcev\\Univesity\\Tyapaev\\4\\Plots\\nplot-hx{i}.png");
				i++;
			}
		}

		public static List<(decimal x, decimal y)> GetDots(int k, Func<int, int> func)
		{
			var dots = new List<(decimal x, decimal y)>();
			int rank = (int)MathF.Pow(2, k);


			foreach (var i in Enumerable.Range(0, rank))
			{
				decimal xValue = i / (decimal)rank;
				decimal yValue = (func(i) % rank) / (decimal)rank;
				dots.Add((xValue, yValue));
			}

			return dots;
		}
	}
}
