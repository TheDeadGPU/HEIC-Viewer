using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HEICViewer
{
    public class MainWindow : Window
    {
        MenuItem item;
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
            item = this.Get<MenuItem>("OpenFile");
            item.Click += Item_Click;
            string[] ARGS = Environment.GetCommandLineArgs();
            if (ARGS.Length > 1)
            {
                ImageLoad(ARGS[1]);
            }
        }
        void ImageLoad(string path)
        {
            MagickImage image = new MagickImage(path);
            image.Format = MagickFormat.Jpeg;
            MemoryStream tmpstream = new MemoryStream();
            image.Write(tmpstream);
            tmpstream.Position = 0;
            Avalonia.Media.Imaging.Bitmap amib = new Avalonia.Media.Imaging.Bitmap(tmpstream);
            Image img = this.Get<Image>("ImageView");
            img.Source = amib;
        }
        private void Item_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            OpenHEICFileDialog();
        }
        private async Task OpenHEICFileDialog()
        {
            var dlg = new OpenFileDialog();
            dlg.Filters.Add(new FileDialogFilter() { Name = "HEIC (.heic)", Extensions = { "heic" } });
            dlg.Filters.Add(new FileDialogFilter() { Name = "All Files", Extensions = { "*" } });
            dlg.AllowMultiple = true;

            var result = await dlg.ShowAsync(this);
            if (result != null)
            {
                string[] fileNames = result;
                ImageLoad(fileNames[0]);
            }
        }
    }
}
