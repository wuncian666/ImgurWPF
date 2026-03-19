using ImgurWPF.Components.GalleryItem;
using ImgurWPF.Components.Search;
using ImgurWPF.Models;
using IOCContainer;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TodoList;
using static ImgurWPF.Components.Pagination.PaginationContract;
using static ImgurWPF.Components.Search.SearchContract;
using static ImgurWPF.Contracts.GalleryContract;

namespace ImgurWPF.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GalleryViewModel : IGalleryView, INotifyPropertyChanged
    {
        // 顯示當前頁面的 items
        public ObservableCollection<GalleryItemViewModel> CurrentPageItems { get; }

        private Collection<GalleryItemViewModel> ResponseItems;

        public ICommand SelectedPageCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public IGalleryPresenter Presenter;

        public event PropertyChangedEventHandler PropertyChanged;

        private int _totalCount;

        public int TotalItemsCount
        {
            get => _totalCount;
            set
            {
                _totalCount = value;
                this.OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ISearchView SearchViewModel { get; } 
        public IPaginationView PaginationViewModel { get; set; }

        public GalleryViewModel(ISearchView searchViewModel, IPaginationView paginationViewModel, IMVPFactory mvpFactory)
        {
            this.SearchViewModel = searchViewModel;
            this.PaginationViewModel = paginationViewModel;

            this.CurrentPageItems = new ObservableCollection<GalleryItemViewModel>();

            this.Presenter = mvpFactory.Create<IGalleryView, IGalleryPresenter>(this);

            // 被 pagination 觸發
            this.SelectedPageCommand = new RelayCommand<PageInfo>((info) =>
            {
                this.CurrentPageItems.Clear();
                var itemsIndex = (info.CurrentPage - 1) * info.ItemsPerPage;
                var items = ResponseItems.Skip(itemsIndex).Take(info.ItemsPerPage);
                foreach (var item in items)
                {
                    this.CurrentPageItems.Add(item);
                }
            });

            // 被 search component 觸發
            this.SearchCommand = new RelayCommand<SearchParamsDTO>((searchParams) =>
            {
                this.Presenter.SearchGalleryAsync(searchParams);
            });
        }

        public void OnResponse(Collection<GalleryItemViewModel> items)
        {
            this.ResponseItems = items;
            this.TotalItemsCount = items.Count();
        }
    }
}