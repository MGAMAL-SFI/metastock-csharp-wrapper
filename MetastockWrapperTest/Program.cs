using MetastockWrapper;

namespace MetastockWrapperTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var security = new MetastockSecurity(@"C:\MetaStock Data\INDUST\", "SHP");
            security.Fill();

        }
    }
}
