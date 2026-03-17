using System.Windows.Input;
using PropertyChanged;
using TodoList;

namespace ImgurWPF.Components.Vote
{
    [AddINotifyPropertyChangedInterface]
    public class VoteViewModel
    {
        public int score { get; set; }

        public int oldscore { get; set; }

        // up down button Binding 根據 vote = up || down || 顯示顏色
        public VoteEnums Vote { get; set; }

        public VoteEnums OldVote { get; set; }

        public ICommand UpVoteCommand { get; set; }
        public ICommand DownVoteCommand { get; set; }

        public ICommand ParentBindingCommand { get; set; }

        //private VoteComponent VoteComponent { get; set; }

        public VoteViewModel()
        {
            //this.VoteComponent = voteComponent;

            // 初始化時以控制項目前的 DP 值為準，避免所有項目都使用預設 UP/DOWN 顏色狀態
            //Vote = this.VoteComponent.Vote;
            OldVote = Vote;

            // 記住原始值
            oldscore = score;

            UpVoteCommand = new RelayCommand(() =>
            {
                VoteEnums v;
                if (Vote == VoteEnums.UP)
                {
                    v = VoteEnums.VETO;
                    score = oldscore;
                    Vote = VoteEnums.VETO;
                }
                else
                {
                    v = VoteEnums.UP;
                    score = score + 1;
                    Vote = VoteEnums.UP;
                }

                //this.VoteComponent.Vote = Vote;

                // params
                //this.VoteComponent.OnVoted(v);
                this.ParentBindingCommand.Execute(v);
            });

            DownVoteCommand = new RelayCommand(() =>
            {
                VoteEnums v;
                if (Vote == VoteEnums.DOWN)
                {
                    v = VoteEnums.VETO;
                    score = oldscore;
                    Vote = VoteEnums.VETO;
                }
                else
                {
                    v = VoteEnums.DOWN;
                    score = score - 1;
                    Vote = VoteEnums.DOWN;
                }

                //this.VoteComponent.Vote = Vote;

                //this.VoteComponent.OnVoted(v);
                this.ParentBindingCommand.Execute(v);
            });
        }
    }
}