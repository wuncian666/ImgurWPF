using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TodoList;
using static ImgurWPF.Components.Favorite.FavoriteContract;

namespace ImgurWPF.Components.Favorite
{
    public class FavoriteViewModel : IFavoriteView, INotifyPropertyChanged
    {
        // 影響 color
        private bool _isFavorite;

        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite == value) return;
                _isFavorite = value;
                OnPropertyChanged();
            }
        }

        // 在元件被使用的地方所綁定的 command
        public ICommand ParentBindingCommand { get; set; } // 對外呼叫

        public ICommand ChangeFavoriteCommand { get; set; } //內部抽換 icon

        public FavoriteViewModel()
        {
            ChangeFavoriteCommand = new RelayCommand(() =>
            {
                this.IsFavorite = !this.IsFavorite;
                ParentBindingCommand?.Execute(null);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}