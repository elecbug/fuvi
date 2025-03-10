namespace FuVi
{
    public struct FuviPoint
    {
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        
        public FuviPoint()
        {
            X = 0;
            Y = 0;
        }
        public FuviPoint(decimal? x, decimal? y)
        {
            X = x;
            Y = y;
        }
    }
}
