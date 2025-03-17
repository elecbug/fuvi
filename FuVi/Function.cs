namespace FuVi
{
    public class Function
    {
        private readonly Func<decimal, decimal> _func;

        public Function(Func<decimal, decimal> func)
        {
            _func = func;
        }

        public GraphPoints Sampling(decimal start, decimal end, decimal interval)
        {
            List<Point> points = [];

            for (decimal i = start; i <= end; i += interval)
            {
                try
                {
                    Point point = new Point(i, _func(i));
                    points.Add(point);
                }
                catch (DivideByZeroException)
                {
                    Point point = new Point(i, null);
                    points.Add(point);
                }
            }

            return new GraphPoints(points);
        }
    }
}
