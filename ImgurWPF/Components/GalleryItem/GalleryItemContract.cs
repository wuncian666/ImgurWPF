using ImgurWPF.Components.Vote;

namespace ImgurWPF.Components.GalleryItem
{
    public class GalleryItemContract
    {
        public interface IGalleryItemView
        {
            void OnVotedResponse();
        }

        public interface IGalleryItemPresenter
        {
            void SendVoteRequest(VoteParams voteParams);
        }
    }
}