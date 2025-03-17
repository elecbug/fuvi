using FuVi.Data;

namespace FuVi.Graph
{
    public class GraphPoints : List<Point>
    {
        public GraphPoints(List<Point> collection) : base(collection)
        {
            Color = ColorList[ColorIndex];
            ColorIndex = (ColorIndex + 1) % ColorList.Count;
        }

        private static int ColorIndex = 0;
        private static readonly List<string> ColorList =
        [
            "#0000FF", "#00FF00", "#FF0000", "#00FFFF", "#FF00FF", "#FFFF00",
            "#0000BB", "#00BB00", "#BB0000", "#00BBFF", "#00FFBB", "#00BBBB", "#BB00FF", "#FF00BB", "#BB00BB", "#BBFF00", "FFBB00", "#BBBB00",
            "#000077", "#007700", "#770000", "#007777", "#770077", "#777700",
            "#000033", "#003300", "#330000", "#003333", "#330033", "#333300",
        ];

        public string Color { get; set; }
    }
}
