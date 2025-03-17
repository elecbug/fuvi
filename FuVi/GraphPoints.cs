using System.Drawing;

namespace FuVi
{
    public class GraphPoints : List<Point>
    {
        public GraphPoints(List<Point> collection) : base(collection) 
        {
            Color = ColorList[ColorIndex];
            ColorIndex = (ColorIndex + 1) % ColorList.Count;
        }

        private static int ColorIndex = 0;
        private static readonly List<string> ColorList = ["#0000FF", "#00FF00", "#FF0000", "#00FFFF", "#FF00FF", "#FFFF00"];

        public string Color { get; set; }
    }
}
