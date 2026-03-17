using ImgurAPI.Models;
using ImgurAPI.Models.Params;
using ImgurWPF.Components.GalleryItem;
using ImgurWPF.Components.Search;
using IOCContainer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static ImgurWPF.Contracts.GalleryContract;

namespace ImgurWPF.Presenters
{
    public class GalleryPresenter : IGalleryPresenter
    {
        public GallerySearchModel.Datum[] Response { get; set; }

        private readonly IGalleryView _view;
        private readonly IMVPFactory _mvpFactory;
        private readonly ImgurAPI.ImgurContext _imgurContext;

        public GalleryPresenter(
            IGalleryView view,
            ImgurAPI.ImgurContext imgurContext)
        {
            this._view = view;
            this._mvpFactory = App.Services.GetService<IMVPFactory>();
            _imgurContext = imgurContext;
        }

        public async Task SearchGalleryAsync(SearchParamsDTO searchParams)
        {
            try
            {
                var parameters = new GallerySearchParam(
                    searchParams.Sort,
                    searchParams.Window,
                    searchParams.Page,
                    searchParams.Query);

                GallerySearchModel response =
                    await _imgurContext.Gallery.GallerySearch((GallerySearchParam)parameters);

                this.Response = response.data;

                Collection<GalleryItemViewModel> items = new Collection<GalleryItemViewModel>();

                foreach (var rawItem in Response)
                {
                    var images = rawItem.images;
                    var imagesLength = (rawItem.images) != null ? rawItem.images.Length : 0;
                    string[] imagesLinks = new string[imagesLength];
                    for (int i = 0; i < imagesLength; i++)
                    {
                        imagesLinks[i] = images[i].link;
                    }

                    var item = new GalleryItemViewModel()
                    {
                        id = rawItem.id,
                        title = rawItem.title,
                        comment_count = rawItem.comment_count,
                        vote = rawItem.vote,
                        views = rawItem.views,
                        ups = rawItem.ups,
                        downs = rawItem.downs,
                        account_url = rawItem.account_url,
                        cover = $"https://imgur.com/{rawItem.cover}.jpg",
                        imagesLinks = imagesLinks,// for detail
                        favorite = rawItem.favorite,
                    };

                    items.Add(item);
                }

                this._view.OnResponse(items);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}