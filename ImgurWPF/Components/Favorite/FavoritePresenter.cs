using static ImgurWPF.Components.Favorite.FavoriteContract;

namespace ImgurWPF.Components.Favorite
{
    public class FavoritePresenter : IFavoritePresenter
    {
        private readonly IFavoriteView _view;

        public FavoritePresenter(IFavoriteView view)
        {
            _view = view;
        }
    }
}