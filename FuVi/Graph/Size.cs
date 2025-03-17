namespace FuVi.Graph
{
    public struct Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Size()
        {
            Width = 0;
            Height = 0;
        }
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
