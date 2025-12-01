

namespace Cloudy
{

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SignUp), typeof(SignUp));

        }
    }
}
