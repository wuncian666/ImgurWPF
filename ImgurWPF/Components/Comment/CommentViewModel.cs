using ImgurWPF.Components.Comment;
using ImgurWPF.Components.Vote;
using IOCContainer;
using Microsoft.Extensions.DependencyInjection;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TodoList;
using static ImgurWPF.Components.Comment.CommentContract;

namespace ImgurWPF.Models
{
    [AddINotifyPropertyChangedInterface]
    public class CommentViewModel : ICommentView
    {
        public long ID { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
        public int Ups { get; set; }
        public int Downs { get; set; }
        public long ParentID { get; set; }
        public object Vote { get; set; }

        [DependsOn(nameof(Ups), nameof(Downs))]
        public int Score => Ups - Downs;

        //public CommentViewModel[] Children { get; set; }

        public ObservableCollection<CommentViewModel> Children { get; set; }

        public bool IsChildrenExpanded { get; set; }

        public bool IsReplyExpanded { get; set; }

        public string ReplyInput { get; set; }

        public bool HasChildren => Children != null && Children.Any();

        public ICommand VoteCommand { get; set; }

        public ICommand ToggleChildrenCommand { get; set; }

        public ICommand ToggleReplyCommand { get; set; }

        public ICommand SubmitReplyCommand { get; set; }

        public ICommentPresenter Presenter { get; set; }

        public CommentViewModel()
        {
            IMVPFactory factory = App.Services.GetService<IMVPFactory>();
            this.Presenter = factory.Create<ICommentView, ICommentPresenter>(this);

            this.VoteCommand = new RelayCommand<VoteEnums>((voteEnum) =>
            {
                Vote = voteEnum;
                var voteParams = new VoteParams(this.ID.ToString(), voteEnum);
            });

            this.ToggleChildrenCommand = new RelayCommand(() =>
            {
                IsChildrenExpanded = !IsChildrenExpanded;
            });

            this.ToggleReplyCommand = new RelayCommand(() =>
            {
                IsReplyExpanded = !IsReplyExpanded;
            });

            this.SubmitReplyCommand = new RelayCommand(() =>
            {
                var id = this.ID.ToString();
                var parentId = this.ParentID.ToString();
                this.Presenter.SubmitRelpy(id, parentId, this.ReplyInput);
                ReplyInput = string.Empty;
                IsReplyExpanded = false;
            });
        }

        public void OnReplyCommnet(CommentDTO comment)
        {
            var viewModel = new CommentViewModel
            {
                ID = comment.Id,
                Author = comment.Author,
                Comment = comment.Comment,
                Ups = 0,
                Downs = 0,
                ParentID = comment.ParentId,
                Vote = VoteEnums.VETO,
                Children = new ObservableCollection<CommentViewModel>()
            };

            this.Children.Add(viewModel);
        }
    }
}