using FuVi;
using System.Diagnostics;
using System.Text.Json;

namespace FuViTest
{
    [TestClass]
    public sealed class MainTest
    {
        [TestMethod]
        public void FunctionTest()
        {
            Function[] funcs =
            {
                new Function(x => x * x),
                new Function(x => 1 / x),
                new Function(x => (decimal)Math.Sin((double)x)),
            };

            for (int i = 0; i < funcs.Length; i++)
            {
                FuviPoint[] points = funcs[i].Sampling(0, 10, 0.01m);
                Debug.WriteLine("Points: " + JsonSerializer.Serialize(points));
            }
        }
    }
}
