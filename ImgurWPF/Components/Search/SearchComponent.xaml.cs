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
            SearchViewModel = new SearchViewModel(this);
            DataContext = SearchViewModel;
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
                new PropertyMetadata(null));

        public void SearchRequest(SearchParamsDTO searchParams)
        {
            // xaml 綁定的 command 會執行
            Command?.Execute(searchParams);
        }
    }
}