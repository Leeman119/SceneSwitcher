using System;
using System.Drawing;
using System.Windows.Forms;

namespace SceneSwitcher
{
    public partial class MainWin : Form
    {
        public static int FailureCount { get; set; } = 0;
        public static int SuccessCount { get; set; } = 0;
        public static DateTime TimeStarted { get; } = DateTime.Now;

        ImageAnalyser Watcher;

        public MainWin()
        {
            try
            {
                InitializeComponent();

                txt_PosX.Text = Properties.Settings.Default.WatchPosX.ToString();
                txt_PosY.Text = Properties.Settings.Default.WatchPosY.ToString();
                txt_Width.Text = Properties.Settings.Default.WatchWidth.ToString();
                txt_Height.Text = Properties.Settings.Default.WatchHeight.ToString();

                // For initial display, copy over the background file to keep from locking it down.
                Watcher = new ImageAnalyser();
                System.IO.File.Copy($"{Watcher.Directory}\\background.bmp", $"{Watcher.Directory}\\display.bmp", true);
                pictureBox1.Image = Image.FromFile($"{Watcher.Directory}\\display.bmp");

                Watcher.Start();
            }
            catch (Exception e)
            {
                FailureCount += 1;
                ErrorLogger.Log($"Statistics since {TimeStarted.ToLongTimeString()}...\r\n" +
                    $"Succesful: {SuccessCount}  -  Failures: {FailureCount}", e);
                RestartWatcher();
            }
        }

        private void UpdateSettings()
        {
            try
            {
                Watcher.IsRunning = false;
                System.Threading.Thread.Sleep(260);

                int x = Convert.ToInt32(txt_PosX.Text);
                int y = Convert.ToInt32(txt_PosY.Text);
                int w = Convert.ToInt32(txt_Width.Text);
                int h = Convert.ToInt32(txt_Height.Text);

                Watcher.UpdateSettings(x, y, w, h);
                pictureBox1.Image = Watcher.Background;
                Watcher.Start();
            }
            catch (Exception e)
            {
                FailureCount += 1;
                ErrorLogger.Log($"Statistics since {TimeStarted.ToLongTimeString()}...\r\n" +
                    $"Succesful: {SuccessCount}  -  Failures: {FailureCount}", e);
                RestartWatcher();
            }
        }

        private void txt_PosX_Leave(object sender, EventArgs e)
        {
            UpdateSettings();
        }
        private void txt_PosY_Leave(object sender, EventArgs e)
        {
            UpdateSettings();
        }
        private void txt_Width_Leave(object sender, EventArgs e)
        {
            UpdateSettings();
        }
        private void txt_Height_Leave(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        public void RestartWatcher()
        {
            Watcher.IsRunning = false;
            System.Threading.Thread.Sleep(500);

            Watcher = new ImageAnalyser();
            Watcher.Start();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txt_PosX.Enabled = false;
                txt_PosY.Enabled = false;
                txt_Width.Enabled = false;
                txt_Height.Enabled = false;
            }
            else
            {
                txt_PosX.Enabled = true;
                txt_PosY.Enabled = true;
                txt_Width.Enabled = true;
                txt_Height.Enabled = true;
            }
        }
    }
}
