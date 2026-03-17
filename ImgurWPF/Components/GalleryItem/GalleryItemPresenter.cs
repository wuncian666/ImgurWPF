using ImgurWPF.Components.Vote;
using static ImgurWPF.Components.GalleryItem.GalleryItemContract;

namespace ImgurWPF.Components.GalleryItem
{
    public class GalleryItemPresenter : IGalleryItemPresenter
    {
        private readonly IGalleryItemView _view;

        private readonly ImgurAPI.ImgurContext _context;

        public GalleryItemPresenter(
            IGalleryItemView view,
            ImgurAPI.ImgurContext context)
        {
            this._view = view;
            _context = context;
        }

        public void SendVoteRequest(VoteParams voteParams)
        {
            string voteStr = voteParams.Vote.ToString().ToLower();
            _context.Gallery.AlbumImageVoting(voteParams.Id, voteStr);

            this._view.OnVotedResponse();
        }
    }
}