
namespace FuVi
{
    public class Workspace
    {
        private FuviPoint[] _samples = new FuviPoint[0];

        public FuviPoint[] Samples { get=>_samples; set { _samples = value; Redraw();} }

        private void Redraw()
        {
            // Draw function image by sameple point data
        }

        public static Workspace GenerateEmptyWorkspcae()
        {
            Workspace workspace = new Workspace();
            workspace.Samples = new FuviPoint[0];
         
            return workspace;
        }

        public static Workspace GenerateBy(FuviPoint[] samples)
        {
            Workspace workspace = new Workspace();
            workspace.Samples = samples;

            return workspace;
        }

    }
}
