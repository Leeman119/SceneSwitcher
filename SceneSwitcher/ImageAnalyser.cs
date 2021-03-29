using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using AForge.Vision.Motion;

namespace SceneSwitcher
{
    public class ImageAnalyser
    {


        //To get the location the assembly normally resides on disk or the install directory
        public string Path { get; }
        public string Directory { get; }
        public bool IsRunning { get; set; } = false;
        public Bitmap Background { get; set; } = null;

        private string CurrentScene { get; set; } = "Scene1";

        public ImageAnalyser()
        {
            Path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            Directory = System.IO.Path.GetDirectoryName(Path).Remove(0, 6);
            Background = new Bitmap($"{Directory}\\background.bmp");

            SetScene("Scene1");
        }


        public Bitmap Capture()
        {
            //Set image coordinates
            int posX = Properties.Settings.Default.WatchPosX;
            int posY = Properties.Settings.Default.WatchPosY;
            int width = Properties.Settings.Default.WatchWidth;
            int height = Properties.Settings.Default.WatchHeight;

            //Creating a new Bitmap object
            Bitmap captureBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            //Creating a Rectangle object which will capture our Current Screen
            Rectangle captureRectangle = new Rectangle(posX, posY, width, height);

            //Creating a New Graphics Object
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);

            //Copying Image from The Screen
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

            //Return bitmap
            return captureBitmap;
        }

        public void SetBackground()
        {
            // Set the file that the Watcher will check against
            Background.Dispose();
            Background = new Bitmap(Capture());
            Background.Save($"{Directory}\\background.bmp", ImageFormat.Bmp);
        }

        public void UpdateSettings(int x, int y, int width, int height)
        {
            Properties.Settings.Default.WatchPosX = x;
            Properties.Settings.Default.WatchPosY = y;
            Properties.Settings.Default.WatchWidth = width;
            Properties.Settings.Default.WatchHeight = height;
            Properties.Settings.Default.Save();

            SetBackground();
        }

        public void Start()
        {
            Task.Run(() => RunWatcher());
        }

        private void RunWatcher()
        {
            try
            {
                IsRunning = true;
                double? PreviousDifference = null;

                // create motion detector
                CustomFrameDifferenceDetector detector = new CustomFrameDifferenceDetector();
                detector.SetBackgroundFrame(Background);
                MotionDetector mdetector = new MotionDetector(detector);

                // Feed video frames to motion detector in a loop. Limit loop to 10 then re-start function to allow for GC and prevent memory leaks.
                for (int i = 0; i < 10 && IsRunning; i++)
                {
                    System.Threading.Thread.Sleep(250);

                    // Process new video frame and check motion level
                    double Difference = mdetector.ProcessFrame(Capture());

                    bool SceneChanged = false;
                    if (Difference < 0.02 && PreviousDifference < 0.02 && CurrentScene != "Scene1")
                    {
                        SetScene("Scene1");
                        SceneChanged = true;
                    }
                    else if (Difference > 0.02 && PreviousDifference > 0.02 && CurrentScene != "Scene2")
                    {
                        SetScene("Scene2");
                        SceneChanged = true;
                    }

                    PreviousDifference = Difference;
                    if (SceneChanged) { break; }

                    MainWin.SuccessCount += 1;
                }
            }
            catch (Exception e)
            {
                MainWin.FailureCount += 1;
                ErrorLogger.Log($"Statistics since {MainWin.TimeStarted.ToLongTimeString()}...\r\n" +
                    $"Succesful: {MainWin.SuccessCount}  -  Failures: {MainWin.FailureCount}", e);
            }
            finally { Start(); }
        }

        private void SetScene(string sceneName)
        {
            CurrentScene = sceneName;
            Scene scene = new Scene { Text = CurrentScene };
            scene.Show(); scene.Focus();
            System.Threading.Thread.Sleep(300);
            scene.Close(); scene.Dispose();
        }
    }
}
