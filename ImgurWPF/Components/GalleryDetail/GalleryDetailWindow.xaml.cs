using System.Windows;

namespace ImgurWPF.Components.GalleryDetail
{
    /// <summary>
    /// GalleryDetailWindow.xaml 的互動邏輯
    /// </summary>
    public partial class GalleryDetailWindow : Window
    {
        public GalleryDetailWindow(GalleryDetailViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}