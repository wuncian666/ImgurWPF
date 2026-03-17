using ImgurAPI.Models;
using ImgurWPF.Components.Vote;
using ImgurWPF.Models;
using ImgurWPF.Utility;
using IOCContainer;
using Microsoft.Extensions.DependencyInjection;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TodoList;
using static ImgurAPI.Models.CommentsModel;
using static ImgurWPF.Components.GalleryDetail.GalleryDetailContract;
using DependsOnAttribute = PropertyChanged.DependsOnAttribute;

namespace ImgurWPF.Components.GalleryDetail
{
    [AddINotifyPropertyChangedInterface]
    public class GalleryDetailViewModel : IGalleryDetailView
    {
        public string CommentInput { get; set; }

        public string Id { get; set; }
        public string Title { get; set; }
        public string AccountUrl { get; set; }
        public string[] ImgesLinks { get; set; }
        public bool Favorite { get; set; }
        public int Ups { get; set; }
        public int Downs { get; set; }
        public VoteEnums Vote { get; set; }

        [DependsOn(nameof(Ups), nameof(Downs))]
        public int Score => Ups - Downs;

        public IGalleryDetailPresenter Presenter { get; set; }

        public ICommand VoteCommand { get; set; }

        public ICommand FavoriteCommand { get; set; }

        public ICommand PostCommentCommand { get; set; }

        public ObservableCollection<CommentViewModel> Comments { get; set; }

        public GalleryDetailViewModel()
        {
        }

        public GalleryDetailViewModel(string id, string title, string accountUrl, string[] imgesLinks, object favorite, int ups, int downs, VoteEnums vote)
        {
            Id = id;
            Title = title;
            AccountUrl = accountUrl;
            ImgesLinks = imgesLinks;
            Favorite = favorite != null;
            Ups = ups;
            Downs = downs;
            Vote = vote;

            IMVPFactory factory = App.Services.GetService<IMVPFactory>();
            this.Presenter = factory.Create<IGalleryDetailView, IGalleryDetailPresenter>(this);

            this.Presenter.LoadComments(this.Id);
            this.Comments = new ObservableCollection<CommentViewModel>();

            VoteCommand = new RelayCommand<VoteEnums>((voteEnum) =>
            {
                Vote = voteEnum;
                var voteParams = new VoteParams(this.Id, voteEnum);
                //this.Presenter.SendVoteRequest(voteParams);
            });

            FavoriteCommand = new RelayCommand(() =>
            {
                // api
                this.Presenter.AddFavoriteAsync(this.Id);
            });

            PostCommentCommand = new RelayCommand(() =>
            {
                string comment = this.CommentInput;
                this.Presenter.PostCommentAsync(this.Id, comment, this.Id);
            });
        }

        public void OnCommentsResponse(CommentsModel comments)
        {
            for (int i = 0; i < comments.data.Length; i++)
            {
                var comment = comments.data[i];
                // dto
                // datum
                var viewModel = Mapper.Map<Datum, CommentViewModel>(comment, config =>
                config
                .ForMember(dest => dest.ID, source => source.MapFrom(x => x.id))
                .ForMember(dest => dest.Author, source => source.MapFrom(x => x.author))
                .ForMember(dest => dest.Comment, source => source.MapFrom(x => x.comment))
                .ForMember(dest => dest.Ups, source => source.MapFrom(x => x.ups))
                .ForMember(dest => dest.Downs, source => source.MapFrom(x => x.downs))
                .ForMember(dest => dest.ParentID, source => source.MapFrom(x => x.parent_id))
                .ForMember(dest => dest.Vote, source => source.MapFrom(x => x.vote))
                .ForMember(dest => dest.Children, source => source.MapFrom(x => x.children))
                );
                this.Comments.Add(viewModel);
            }
        }

        public void OnPostCommentResponse(CommentViewModel comment)
        {
            this.Comments.Add(comment);
        }

        public void OnFavoriteAdd()
        {
            Console.WriteLine("add");
            Favorite = !Favorite;
        }

        public void OnReplyComment()
        {
            throw new NotImplementedException();
        }
    }
}