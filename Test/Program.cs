using FuVi;
using System.Drawing;

Function[] funcs =
{
    new Function(x => x * x),
    new Function(x => 1 / x),
    new Function(x => (decimal)Math.Sin((double)x)),
    new Function(x => (decimal)Math.Pow(2, (double)x)),
};

for (int i = 0; i < funcs.Length; i++)
{
    FuVi.Point[] points = funcs[i].Sampling(-10, 10, 0.001m);

    Workspace workspace = new Workspace(points)
    {
        Size = new Size(2000, 2000),
        Padding = 100,
        Axis = new Axis
        {
            StartX = -10,
            StartY = -10,
            EndX = 10,
            EndY = 10,
        }
    };

    Svg svg = workspace.Draw();

    svg.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"function_{i}.svg"));
}