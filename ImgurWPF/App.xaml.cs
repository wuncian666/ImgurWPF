using ImgurWPF.Components.Comment;
using ImgurWPF.Components.GalleryDetail;
using ImgurWPF.Components.GalleryItem;
using ImgurWPF.Components.Pagination;
using ImgurWPF.Components.Search;
using ImgurWPF.Models;
using ImgurWPF.Presenters;
using ImgurWPF.ViewModels;
using IOCContainer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using static ImgurWPF.Components.Comment.CommentContract;
using static ImgurWPF.Components.GalleryDetail.GalleryDetailContract;
using static ImgurWPF.Components.GalleryItem.GalleryItemContract;
using static ImgurWPF.Components.Pagination.PaginationContract;
using static ImgurWPF.Components.Search.SearchContract;
using static ImgurWPF.Contracts.GalleryContract;

namespace ImgurWPF
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var services = new IOCContainer.ServiceCollection();

            services.AddSingleton<IMVPFactory, MVPFactory>();

            services.AddTransient<Window, MainWindow>();
            services.AddTransient<IGalleryPresenter, GalleryPresenter>();
            services.AddTransient<IGalleryView, GalleryViewModel>();

            services.AddTransient<ISearchPresenter, SearchPresenter>();
            services.AddTransient<ISearchView, SearchViewModel>();

            services.AddTransient<IPaginationPresenter, PaginationPresenter>();
            services.AddTransient<IPaginationView, PaginationViewModel>();

            services.AddTransient<IGalleryItemView, GalleryItemViewModel>();
            services.AddTransient<IGalleryItemPresenter, GalleryItemPresenter>();

            services.AddTransient<IGalleryDetailView, GalleryDetailViewModel>();
            services.AddTransient<IGalleryDetailPresenter, GalleryDetailPresenter>();

            services.AddTransient<ICommentView, CommentViewModel>();
            services.AddTransient<ICommentPresenter, CommentPresenter>();

            services.AddSingleton<ImgurAPI.ImgurContext, ImgurAPI.ImgurContext>();

            Services = services.BuildServiceProvider();

            Window window = Services.GetService<Window>();
            window.Show();
        }
    }
}