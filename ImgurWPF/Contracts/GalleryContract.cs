using ImgurAPI.Models;
using ImgurWPF.Components.GalleryItem;
using ImgurWPF.Components.Search;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ImgurWPF.Contracts
{
    public class GalleryContract
    {
        public interface IGalleryView
        {
            void OnResponse(Collection<GalleryItemViewModel> items);
        };

        public interface IGalleryPresenter
        {
            GallerySearchModel.Datum[] Response { get; set; }

            Task SearchGalleryAsync(SearchParamsDTO searchParams);
        };
    }
}