using ImgurAPI.Models;
using ImgurWPF.Models;
using System.Threading.Tasks;

namespace ImgurWPF.Components.GalleryDetail
{
    public class GalleryDetailContract
    {
        public interface IGalleryDetailView
        {
            void OnCommentsResponse(CommentsModel comments);

            void OnPostCommentResponse(CommentViewModel comment);

            void OnFavoriteAdd();

            void OnReplyComment();
        }

        public interface IGalleryDetailPresenter
        {
            void LoadComments(string galleryId);

            Task PostCommentAsync(string imageId, string comment, string parentId);

            void InsertImage();

            Task AddFavoriteAsync(string albumId);
        }
    }
}