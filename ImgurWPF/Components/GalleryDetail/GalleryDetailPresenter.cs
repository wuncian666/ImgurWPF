using ImgurAPI;
using ImgurAPI.Models;
using ImgurWPF.Components.Vote;
using ImgurWPF.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static ImgurWPF.Components.GalleryDetail.GalleryDetailContract;

namespace ImgurWPF.Components.GalleryDetail
{
    public class GalleryDetailPresenter : IGalleryDetailPresenter
    {
        private readonly IGalleryDetailView _view;

        private readonly ImgurContext context;

        public GalleryDetailPresenter(
            IGalleryDetailView view,
            ImgurContext context)
        {
            _view = view;
            this.context = context;
        }

        public async Task AddFavoriteAsync(string albumId)
        {
            BasicResponse response = await context.Album.AlbumImageFavorite(albumId);
            if (response.success)
            {
                _view.OnFavoriteAdd();
            }
        }

        public void InsertImage()
        {
            throw new NotImplementedException();
        }

        public async void LoadComments(string galleryId)
        {
            try
            {
                CommentsModel response =
                    await context.Gallery.GetComments(galleryId);
                this._view.OnCommentsResponse(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task PostCommentAsync(string imageId, string comment, string parentId)
        {
            parentId = null;
            _ = await context.Comment.CommentCreation(imageId, comment, parentId);

            var commentViewModel = new CommentViewModel
            {
                ID = 0,
                Author = "",
                Comment = comment,
                Ups = 0,
                Downs = 0,
                ParentID = 0,
                Vote = VoteEnums.VETO,
                Children = new ObservableCollection<CommentViewModel>()
            };
            // view add comment
            this._view.OnPostCommentResponse(commentViewModel);
        }
    }
}