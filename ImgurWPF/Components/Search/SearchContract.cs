using ImgurAPI.Models;

namespace ImgurWPF.Components.Search
{
    public class SearchContract
    {
        public interface ISearchView
        {
            void OnResponse(GallerySearchModel response);
        }

        public interface ISearchPresenter
        {
        }
    }
}