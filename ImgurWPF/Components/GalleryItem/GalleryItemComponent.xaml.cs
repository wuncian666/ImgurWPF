using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImgurWPF.Components.GalleryItem
{
    /// <summary>
    /// GalleryItemComponent.xaml 的互動邏輯
    /// </summary>
    public partial class GalleryItemComponent : UserControl
    {
        public GalleryItemComponent()
        {
            InitializeComponent();
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(GalleryItemComponent),
                new PropertyMetadata(null));
    }
}