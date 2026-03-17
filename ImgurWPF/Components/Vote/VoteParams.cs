namespace ImgurWPF.Components.Vote
{
    public class VoteParams
    {
        public VoteParams(string id, VoteEnums vote)
        {
            Id = id;
            Vote = vote;
        }

        public string Id { get; set; }

        public VoteEnums Vote { get; set; }
    }
}