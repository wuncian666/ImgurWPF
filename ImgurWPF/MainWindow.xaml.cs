using ImgurWPF.ViewModels;
using System.Windows;
using static ImgurWPF.Contracts.GalleryContract;

namespace ImgurWPF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IGalleryView galleryViewModel)
        {
            InitializeComponent();
            DataContext = galleryViewModel;
        }
    }
}