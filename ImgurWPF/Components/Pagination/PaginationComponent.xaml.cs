using ImgurWPF.Models;
using IOCContainer;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImgurWPF.Components.Pagination
{
    /// <summary>
    /// Pagination.xaml 的互動邏輯
    /// </summary>
    public partial class PaginationComponent : UserControl
    {
        public PaginationViewModel PaginationViewModel { get; set; }

        public PaginationComponent()
        {
            InitializeComponent();
            this.PaginationViewModel = new PaginationViewModel();
            DataContext = PaginationViewModel;
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
                typeof(PaginationComponent),
                new PropertyMetadata((d, e) =>
                {
                    var control = (PaginationComponent)d;
                    var dataContext = (PaginationViewModel)control.DataContext;
                    dataContext.ParentBindingCommand = (ICommand)e.NewValue;
                }));

        // 外部綁定用：總筆數
        public int TotalItemsCount
        {
            get => (int)GetValue(TotalItemsCountProperty);
            set => SetValue(TotalItemsCountProperty, value);
        }

        public static readonly DependencyProperty TotalItemsCountProperty =
            DependencyProperty.Register(
                nameof(TotalItemsCount),
                typeof(int),
                typeof(PaginationComponent),
                new PropertyMetadata((d, e) =>
                {
                    var control = (PaginationComponent)d;
                    control.PaginationViewModel.SetTotalCount((int)e.NewValue);
                }));

        //public void PageDisplay(PageInfo info)
        //{
        //    Command?.Execute(info);
        //}
    }
}