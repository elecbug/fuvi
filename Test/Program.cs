using FuVi;
using FuVi.Data;
using FuVi.File;
using FuVi.Graph;
using Math = FuVi.Data.Math;

Function[] funcs =
{
    new Function(x => (x - 3) * (x - 5) * (x + 7)),
    new Function(x => 1 / x),
    new Function(x => Math.Sin(x)),
};

Workspace workspace = new Workspace()
{
    Size = new Size(2000, 2000),
    Padding = 10,
    Axis = new Axis
    {
        StartX = -10,
        EndX = 10,
        StartY = -10,
        EndY = 10,
    }
};

for (int i = 0; i < funcs.Length; i++)
{
    GraphPoints points = funcs[i].Sampling(-10, 10, 0.001m);
    workspace.Points.Add(points);
}

Svg svg = workspace.Draw();
svg.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"function.svg"));