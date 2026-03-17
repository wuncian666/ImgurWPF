using PropertyChanged;

namespace ImgurApp.Components.Pagination
{
    [AddINotifyPropertyChangedInterface]
    public class PageIndex
    {
        public PageIndex(int index)
        {
            Index = index;
        }

        public int Index { get; set; }

        public bool IsActive { get; set; }
    }
}