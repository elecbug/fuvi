namespace FuVi
{
    public struct Axis
    {
        private decimal _startX;
        private decimal _startY;
        private decimal _endX;
        private decimal _endY;

        public decimal StartX { readonly get => _startX; set => _startX = Math.Min(value, EndX); }
        public decimal StartY { readonly get => _startY; set => _startY = Math.Min(value, EndY); }
        public decimal EndX { readonly get => _endX; set => _endX = Math.Max(value, StartX); }
        public decimal EndY { readonly get => _endY; set => _endY = Math.Max(value, StartY); }
    }
}
