using FuVi;
using FuVi.Data;
using FuVi.File;
using FuVi.Graph;

Function[] funcs =
{
    new Function(x => x * x),
    new Function(x => x * x * x),
    new Function(x => x * x * x * x),
    new Function(x => 1 / x),
    new Function(x => 1 / x / x),
    new Function(x => x * x / x),
    new Function(x => (decimal)Math.Sin((double)x)),
    new Function(x => (decimal)Math.Pow(2, (double)x)),
};

Workspace workspace = new Workspace()
{
    Size = new Size(2000, 2000),
    Padding = 100,
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