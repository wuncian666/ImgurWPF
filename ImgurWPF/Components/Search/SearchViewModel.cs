using ImgurAPI.Models;
using IOCContainer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;
using TodoList;
using static ImgurWPF.Components.Search.SearchContract;

namespace ImgurWPF.Components.Search
{
    public class SearchViewModel : ISearchView
    {
        public string[] SortParams { get; set; } =
                new string[] { "viral", "top", "time", "rising" };

        public string[] WindowParams { get; set; } =
        new string[] { "day", "week", "month", "year", "all" };

        public string Sort { get; set; } = "top";

        public string Window { get; set; } = "week";

        public int Page { get; set; } = 0;

        public string Query { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand ParentBindingComment { get; set; }
        private readonly ISearchPresenter presenter;

        public SearchViewModel()
        {
            IMVPFactory factory = App.Services.GetService<IMVPFactory>();
            this.presenter = factory.Create<ISearchView, ISearchPresenter>(this);

            SearchCommand = new RelayCommand(() =>
            {
                var searchParams = new SearchParamsDTO(
                    this.Sort,
                    this.Window,
                    this.Page,
                    this.Query);

                // 讓父元件去執行搜尋
                ParentBindingComment?.Execute(searchParams);
            });
        }

        public void OnResponse(GallerySearchModel response)
        {
            Console.WriteLine("do nothing...");
        }
    }
}