namespace FuVi
{
    public class Function
    {
        private Func<decimal, decimal> _func;

        public Function(Func<decimal, decimal> func)
        {
            _func = func;
        }

        public FuviPoint[] Sampling(decimal start, decimal end, decimal interval)
        {

        }
    }
}
