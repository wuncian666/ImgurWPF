using ImgurWPF.Components.GalleryDetail;
using ImgurWPF.Components.Vote;
using IOCContainer;
using Microsoft.Extensions.DependencyInjection;
using PropertyChanged;
using System;
using System.Windows.Input;
using TodoList;
using static ImgurWPF.Components.GalleryItem.GalleryItemContract;

namespace ImgurWPF.Components.GalleryItem
{
    //[AddINotifyPropertyChangedInterface]
    public class GalleryItemViewModel : IGalleryItemView
    {
        public string id { get; set; }
        public string cover { get; set; }
        public string title { get; set; }
        public int comment_count { get; set; }
        public int views { get; set; }
        public object vote { get; set; }
        public int ups { get; set; }
        public int downs { get; set; }

        public object favorite { get; set; }
        public string[] imagesLinks { get; set; }

        public string account_url { get; set; }

        public VoteEnums Vote { get; set; }

        [DependsOn(nameof(ups), nameof(downs))]
        public int Score => ups - downs;

        public IGalleryItemPresenter Presenter;

        public ICommand VoteCommand { get; set; }

        // 顯示 detail form
        public ICommand DetailCommand { get; set; }

        public GalleryItemViewModel()
        {
            IMVPFactory factory = App.Services.GetService<IMVPFactory>();
            this.Presenter = factory.Create<IGalleryItemView, IGalleryItemPresenter>(this);

            // 被 vote component 觸發
            VoteCommand = new RelayCommand<VoteEnums>((vote) =>
            {
                Vote = vote;
                var voteParams = new VoteParams(this.id, vote);
                this.Presenter.SendVoteRequest(voteParams);
            });

            DetailCommand = new RelayCommand(() =>
            {
                var detailViewModel = new GalleryDetailViewModel(
                    this.id,
                    this.title,
                    this.account_url,
                    this.imagesLinks,
                    this.favorite,
                    this.ups,
                    this.downs,
                    this.Vote);
                var detail = new GalleryDetailWindow(detailViewModel);
                detail.Show();
            });
        }

        public void OnVotedResponse()
        {
            Console.WriteLine("vote response");
        }
    }
}