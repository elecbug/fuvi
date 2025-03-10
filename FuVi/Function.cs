namespace FuVi
{
    public class Function
    {
        private Func<decimal, decimal> _func;

        public Function(Func<decimal, decimal> func)
        {
            _func = func;
        }

        public FuviPoint[] Sampling(decimal start, decimal end, decimal interval)
        {
            List<FuviPoint> points = new List<FuviPoint>();

            for (decimal i = start; i <= end; i += interval)
            {
                try
                {
                    FuviPoint point = new FuviPoint(i, _func(i));
                    points.Add(point);
                }
                catch (DivideByZeroException)
                {
                    FuviPoint point = new FuviPoint(0, null);
                    points.Add(point);
                }
            }

            return points.ToArray();
        }
    }
}
