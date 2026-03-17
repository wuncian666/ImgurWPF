using ImgurAPI;
using System;
using System.Threading.Tasks;
using static ImgurWPF.Components.Comment.CommentContract;

namespace ImgurWPF.Components.Comment
{
    public class CommentPresenter : ICommentPresenter
    {
        private ICommentView View;

        private readonly ImgurContext context;

        public CommentPresenter(
            ICommentView view,
            ImgurContext context)
        {
            this.View = view;
            this.context = context;
        }

        public async Task SubmitRelpy(string imageId, string parentId, string comment)
        {
            var response = await context.Comment.ReplyCreation(imageId, parentId, comment);
            long.TryParse(imageId, out var longImageId);
            long.TryParse(parentId, out var longParentId);

            long commentId = (response == null) ?
                new Random().Next() : response.data.id;

            var commentDto = new CommentDTO(
                longImageId, comment, "current_user", longParentId, commentId);

            this.View.OnReplyCommnet(commentDto);
        }
    }
}