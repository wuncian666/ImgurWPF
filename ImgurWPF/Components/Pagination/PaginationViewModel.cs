using ImgurApp.Components.Pagination;
using ImgurWPF.Models;
using IOCContainer;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TodoList;
using static ImgurWPF.Components.Pagination.PaginationContract;

namespace ImgurWPF.Components.Pagination
{
    public class PaginationViewModel : IPaginationView
    {
        // 顯示 page index
        public ObservableCollection<PageIndex> Pages { get; set; }

        public int CurrentPageIndex
        { set { presenter.CurrentPageIndex = value; } }

        public ICommand SelectPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand ParentBindingCommand { get; set; }

        private readonly IPaginationPresenter presenter;

        public PaginationViewModel()
        {
            IMVPFactory factory = App.Services.GetService<IMVPFactory>();
            this.presenter = factory.Create<IPaginationView, IPaginationPresenter>(this);
            this.Pages = new ObservableCollection<PageIndex>();

            this.SelectPageCommand = new RelayCommand<int>(pageNumber =>
            {
                this.presenter.SelectPage(pageNumber);
            });

            this.NextPageCommand = new RelayCommand(() =>
            {
                if (this.presenter.TotalItems <= 0)
                {
                    return;
                }

                this.presenter.NextPage();
            });

            this.PreviousPageCommand = new RelayCommand(() =>
            {
                if (this.presenter.TotalItems <= 0)
                {
                    return;
                }

                this.presenter.PreviousPage();
            });
        }

        public void SetTotalCount(int total)
        {
            this.presenter.TotalItems = total;

            if (total <= 0)
            {
                this.Pages.Clear();
            }
        }

        public void OnPaginationChanged(List<PageIndex> pages)
        {
            this.Pages.Clear();
            foreach (var page in pages)
            {
                this.Pages.Add(page);
            }
        }

        public void OnPageChanged(int currentIndex)
        {
            if (this.Pages == null || this.Pages.Count == 0)
            {
                return;
            }

            // 更新 pages index 顏色
            foreach (var item in this.Pages)
            {
                item.IsActive = item.Index == currentIndex;
            }

            PageInfo info = new PageInfo(currentIndex, this.presenter.ItemPerPage);
            // call command trigger main window command
            ParentBindingCommand?.Execute(info);
            //this.View.PageDisplay(info);
        }
    }
}