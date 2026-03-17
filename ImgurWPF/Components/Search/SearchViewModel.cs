using ImgurAPI.Models;
using System;
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

        private readonly ISearchPresenter presenter;

        public SearchViewModel(SearchComponent view)
        {
            SearchCommand = new RelayCommand(() =>
            {
                var searchParams = new SearchParamsDTO(
                    this.Sort,
                    this.Window,
                    this.Page,
                    this.Query);
                view.SearchRequest(searchParams);
            });
        }

        public void OnResponse(GallerySearchModel response)
        {
            Console.WriteLine("do nothing...");
        }
    }
}