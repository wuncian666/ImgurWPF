using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImgurWPF.Components.Search
{
    /// <summary>
    /// SearchComponent.xaml 的互動邏輯
    /// </summary>
    public partial class SearchComponent : UserControl
    {
        public SearchViewModel SearchViewModel { get; set; }

        public SearchComponent()
        {
            InitializeComponent();
            //SearchViewModel = new SearchViewModel(this);
            //DataContext = SearchViewModel;
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
                typeof(SearchComponent),
                new PropertyMetadata((d, e) =>
                {
                    var control = (SearchComponent)d;
                    var dataContext = (SearchViewModel)control.DataContext;
                    dataContext.ParentBindingComment = (ICommand)e.NewValue;
                }));
    }
}