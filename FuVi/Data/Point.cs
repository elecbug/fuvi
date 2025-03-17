namespace FuVi.Data
{
    public struct Point
    {
        public decimal? X { get; set; }
        public decimal? Y { get; set; }

        public Point()
        {
            X = null;
            Y = null;
        }
        public Point(decimal? x, decimal? y)
        {
            X = x;
            Y = y;
        }

        public static Point ZeroPoint() => new Point(0, 0);

        public override readonly string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
