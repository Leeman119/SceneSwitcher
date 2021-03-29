using System.Windows.Forms;

namespace SceneSwitcher
{
    public partial class Scene : Form
    {
        public Scene()
        {
            InitializeComponent();
        }

        public void ChangeScene(string sceneName)
        {
            // Change the name of the hidden (opacity=0%) active window so that the OBS "Automatic Scene Switcher" will trigger.
            Text = sceneName;
            WindowState = FormWindowState.Normal;
            Focus();
            // Sleep time must be longer than what is set in OBS' "Automatic Scene Switcher" setting.
            System.Threading.Thread.Sleep(310);
            WindowState = FormWindowState.Minimized;
        }
    }
}
