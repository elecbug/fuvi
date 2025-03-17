using FuVi.File;
using FuVi.Graph;
using System.Xml.Linq;

namespace FuVi
{
    public class Workspace
    {
        private int _padding;

        public List<GraphPoints> Points { get; private set; }
        public int Padding { get => _padding; set => _padding = value >= 0 ? value : throw new InvalidDataException($"Workspace.Padding must over than zero: {value}"); }
        public Size Size { get; set; }
        public Axis Axis { get; set; }

        public Workspace()
        {
            Size = new Size(300, 300);
            Axis = new Axis() 
            {
                StartX = -10,
                StartY = -10,
                EndX = 10,
                EndY = 10,
            };
            Padding = 10;
            Points = [];
        }
        public Workspace(params GraphPoints[] points) : base()
        {
            Points = points.ToList();
        }

        public Svg Draw()
        {
            if (Axis.StartX >= Axis.EndX)
            {
                throw new InvalidDataException($"Workspace.Axis's X must be valid: {Axis.StartX}~{Axis.EndX}");
            }
            if (Axis.StartY >= Axis.EndY)
            {
                throw new InvalidDataException($"Workspace.Axis's Y must be valid: {Axis.StartY}~{Axis.EndY}");
            }
            if (Size.Width <= 0)
            {
                throw new InvalidDataException($"Workspace.Size's Width must over than zero: {Size.Width}");
            }
            if (Size.Height <= 0)
            {
                throw new InvalidDataException($"Workspace.Size's Height must over than zero: {Size.Height}");
            }

            return new Svg(CreateSvg());
        }

        private string CreateSvg()
        {
            XNamespace svgNs = "http://www.w3.org/2000/svg";
            XElement svg = CreateBaseline(svgNs);
            List<XElement>[] polylines = new List<XElement>[Points.Count];

            Parallel.For(0, polylines.Length, i =>
            {
                polylines[i] = CreatePolylines(svgNs, Points[i]);
            });

            svg.Add(polylines);
            svg.Add(CreateZeroPoint(svgNs));

            return svg.ToString();
        }

        private XElement CreateBaseline(XNamespace svgNs)
        {
            return new XElement(svgNs + "svg",
                new XAttribute(XNamespace.Xmlns + "svg", svgNs),
                new XAttribute("width", Size.Width),
                new XAttribute("height", Size.Height),

                new XElement(svgNs + "line",
                    new XAttribute("x1", Padding), new XAttribute("y1", Padding),
                    new XAttribute("x2", Padding), new XAttribute("y2", Size.Height - Padding),
                    new XAttribute("stroke", "black"),
                    new XAttribute("stroke-width", "2")),

                new XElement(svgNs + "line",
                    new XAttribute("x1", Padding), new XAttribute("y1", Size.Height - Padding),
                    new XAttribute("x2", Size.Width - Padding), new XAttribute("y2", Size.Height - Padding),
                    new XAttribute("stroke", "black"),
                    new XAttribute("stroke-width", "2")),

                new XElement(svgNs + "line",
                    new XAttribute("x1", Size.Width - Padding), new XAttribute("y1", Size.Height - Padding),
                    new XAttribute("x2", Size.Width - Padding), new XAttribute("y2", Padding),
                    new XAttribute("stroke", "black"),
                    new XAttribute("stroke-width", "2")),

                new XElement(svgNs + "line",
                    new XAttribute("x1", Size.Width - Padding), new XAttribute("y1", Padding),
                    new XAttribute("x2", Padding), new XAttribute("y2", Padding),
                    new XAttribute("stroke", "black"),
                    new XAttribute("stroke-width", "2"))
            );
        }
        private List<XElement> CreatePolylines(XNamespace svgNs, GraphPoints pls)
        {
            List<XElement> elements = [];
            List<string> currentPolylinePoints = [];

            decimal axisWidth = Axis.EndX - Axis.StartX;
            decimal axisHeight = Axis.EndY - Axis.StartY;
            decimal svgWidth = Size.Width - 2 * Padding;
            decimal svgHeight = Size.Height - 2 * Padding;

            /// Draw graph polylines
            foreach (var point in pls)
            {
                if (point.X == null || point.Y == null)
                {
                    if (currentPolylinePoints.Count > 0)
                    {
                        elements.Add(new XElement(svgNs + "polyline",
                            new XAttribute("fill", "none"),
                            new XAttribute("stroke", pls.Color),
                            new XAttribute("stroke-width", "2"),
                            new XAttribute("points", string.Join(" ", currentPolylinePoints))));

                        currentPolylinePoints.Clear();
                    }
                    continue;
                }
                else if (point.X >= Axis.StartX && point.X <= Axis.EndX &&
                         point.Y >= Axis.StartY && point.Y <= Axis.EndY)
                {
                    decimal x = point.X.Value;
                    decimal y = point.Y.Value;

                    decimal mappedX = ((x - Axis.StartX) / axisWidth) * svgWidth + Padding;
                    decimal mappedY = (svgHeight + 2 * Padding) - (((y - Axis.StartY) / axisHeight) * svgHeight + Padding);

                    currentPolylinePoints.Add($"{mappedX},{mappedY}");
                }
            }

            if (currentPolylinePoints.Count > 0)
            {
                elements.Add(new XElement(svgNs + "polyline",
                    new XAttribute("fill", "none"),
                    new XAttribute("stroke", pls.Color),
                    new XAttribute("stroke-width", "2"),
                    new XAttribute("points", string.Join(" ", currentPolylinePoints))));
            }

            return elements;
        }
        private List<XElement> CreateZeroPoint(XNamespace svgNs)
        {
            List<XElement> elements = [];
            List<string> currentPolylinePoints = [];

            decimal axisWidth = Axis.EndX - Axis.StartX;
            decimal axisHeight = Axis.EndY - Axis.StartY;
            decimal svgWidth = Size.Width - 2 * Padding;
            decimal svgHeight = Size.Height - 2 * Padding;

            decimal zeroPointSize = 2.5m;

            /// Draw zero point
            if (Axis.StartX <= 0 && Axis.EndX >= 0 && Axis.StartY <= 0 && Axis.EndY >= 0)
            {
                decimal zeroX = ((0 - Axis.StartX) / axisWidth) * svgWidth + Padding;
                decimal zeroY = (svgHeight + 2 * Padding) - (((0 - Axis.StartY) / axisHeight) * svgHeight + Padding);

                elements.Add(new XElement(svgNs + "line",
                    new XAttribute("x1", Padding), new XAttribute("y1", zeroY),
                    new XAttribute("x2", Size.Width - Padding), new XAttribute("y2", zeroY),
                    new XAttribute("stroke", "gray"),
                    new XAttribute("stroke-width", "1")));

                elements.Add(new XElement(svgNs + "line",
                    new XAttribute("x1", zeroX), new XAttribute("y1", Padding),
                    new XAttribute("x2", zeroX), new XAttribute("y2", Size.Height - Padding),
                    new XAttribute("stroke", "gray"),
                    new XAttribute("stroke-width", "1")));

                elements.Add(new XElement(svgNs + "line",
                    new XAttribute("x1", zeroX - zeroPointSize), new XAttribute("y1", zeroY - zeroPointSize),
                    new XAttribute("x2", zeroX + zeroPointSize), new XAttribute("y2", zeroY + zeroPointSize),
                    new XAttribute("stroke", "black"),
                    new XAttribute("stroke-width", "1")));

                elements.Add(new XElement(svgNs + "line",
                    new XAttribute("x1", zeroX + zeroPointSize), new XAttribute("y1", zeroY - zeroPointSize),
                    new XAttribute("x2", zeroX - zeroPointSize), new XAttribute("y2", zeroY + zeroPointSize),
                    new XAttribute("stroke", "black"),
                    new XAttribute("stroke-width", "1")));
            }

            return elements;
        }
    }
}
