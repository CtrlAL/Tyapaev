using Fractions;
using ZedGraph;

namespace Forms4
{
	internal static class Program
	{
		public static int Hx(int x) => (x ^ 1) ^ (2 * (x & (1 + 2 * x) & (3 + 4 * 4) & (7 + 8 * x) & (15 + 16 * x) & (31 + 32 * x) & (63 + 64 * x))) ^ (4 * ((int)Math.Pow(x, 2) + 5));
		public static int Fx(int x) => 12 + 3 * x - 14 * x * x;
		public static Fraction Gx(int x) => new Fraction(9, 7) * x + new Fraction(7, 11) * x;
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(new Form1());


			var k = Enumerable.Range(15, 20);

			var fXDotLists = new List<PointPairList>();
			var hXOfDotLists = new List<PointPairList>();

			foreach (var kVal in k)
			{
				fXDotLists.Add(GetDots(kVal, Fx));
				hXOfDotLists.Add(GetDots(kVal, Hx));
			}

			int i = 0;
			foreach (var list in fXDotLists)
			{
				var zedGraph = new ZedGraphControl();
				GraphPane pane = zedGraph.GraphPane;
				var curve = pane.AddCurve($"fx: {i}", list, Color.Green, SymbolType.None);
				curve.Line.IsVisible = false;
				pane.AxisChange();
				zedGraph.Refresh();
			}

			i = 0;
			foreach (var list in hXOfDotLists)
			{
				var zedGraph = new ZedGraphControl();
				GraphPane pane = new GraphPane();
				var curve = pane.AddCurve($"fx: {i}", list, Color.Green, SymbolType.None).Line.IsVisible = false;
				pane.AxisChange();
				zedGraph.Refresh();
			}
		}



		public static PointPairList GetDots(int k, Func<int, int> func)
		{
			var dots = new PointPairList();
			int rank = (int)MathF.Pow(2, k);


			foreach (var i in Enumerable.Range(0, rank))
			{
				double xValue = i / rank;
				double yValue = (func(i) % rank) % rank;
				dots.Add(new PointPair(xValue, yValue));
			}

			return dots;
		}
	}
}