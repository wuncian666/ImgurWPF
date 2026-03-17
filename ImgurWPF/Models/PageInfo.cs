namespace ImgurWPF.Models
{
    public class PageInfo
    {
        public PageInfo(int currentPage, int itemsPerPage)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
        }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }
    }
}