using ImgurWPF.Components.GalleryDetail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImgurWPF.Components.Favorite
{
    /// <summary>
    /// FavoriteComponent.xaml 的互動邏輯
    /// </summary>
    public partial class FavoriteComponent : UserControl
    {
        public FavoriteViewModel FavoriteViewModel { get; set; }

        public FavoriteComponent()
        {
            InitializeComponent();
            this.FavoriteViewModel = new FavoriteViewModel();
            DataContext = this.FavoriteViewModel;
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
                typeof(FavoriteComponent),
                new PropertyMetadata((d, e) =>
                {
                    var control = (FavoriteComponent)d;
                    var dataContext = (FavoriteViewModel)control.DataContext;
                    // 把 favorite view model 的 command 變成 detail 綁的 command
                    dataContext.ParentBindingCommand = (ICommand)e.NewValue;
                }));

        // 外部綁定用
        public bool IsFavorite
        {
            get => (bool)GetValue(IsFavoriteProperty);
            set => SetValue(IsFavoriteProperty, value);
        }

        public static readonly DependencyProperty IsFavoriteProperty =
            DependencyProperty.Register(
                nameof(IsFavorite),
                typeof(bool),
                typeof(FavoriteComponent),
                new PropertyMetadata((d, e) =>
                {
                    var control = (FavoriteComponent)d;
                    var dataContext = (FavoriteViewModel)control.DataContext;
                    dataContext.IsFavorite = ((bool)e.NewValue);
                }));
    }
}