using static ImgurWPF.Components.Search.SearchContract;

namespace ImgurWPF.Components.Search
{
    public class SearchPresenter : ISearchPresenter
    {
        private ISearchView _view;

        public SearchPresenter(ISearchView view)
        {
            this._view = view;
        }
    }
}