namespace FuVi
{
    public struct FuviPoint
    {
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        
        public FuviPoint()
        {
            X = null;
            Y = null;
        }
        public FuviPoint(decimal? x, decimal? y)
        {
            X = x;
            Y = y;
        }
    }
}
