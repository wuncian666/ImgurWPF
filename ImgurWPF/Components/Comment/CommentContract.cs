using System.Threading.Tasks;

namespace ImgurWPF.Components.Comment
{
    public class CommentContract
    {
        public interface ICommentView
        {
            void OnReplyCommnet(CommentDTO comment);
        }

        public interface ICommentPresenter
        {
            Task SubmitRelpy(string imageId, string parentId, string comment);
        }
    }
}