using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ImageMagick;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace HEICViewer
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            ImageLoad();
        }
        void ImageLoad()
        {
            string[] ARGS = Environment.GetCommandLineArgs();
            Console.WriteLine("OUT: " + ARGS[1]);
            MagickImage image = new MagickImage(ARGS[1]);
            image.Format = MagickFormat.Jpeg;
            MemoryStream tmpstream = new MemoryStream();
            image.Write(tmpstream);
            tmpstream.Position = 0;
            Avalonia.Media.Imaging.Bitmap amib = new Avalonia.Media.Imaging.Bitmap(tmpstream);
            Image img = this.Get<Image>("ImageView");
            img.Source = amib;
        }
    }
}
