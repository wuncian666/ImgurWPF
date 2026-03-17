using ImgurApp.Components.Pagination;
using System.Collections.Generic;

namespace ImgurWPF.Components.Pagination
{
    public class PaginationContract
    {
        public interface IPaginationView
        {
            void OnPageChanged(int currentIndex);

            void OnPaginationChanged(List<PageIndex> pages);
        };

        public interface IPaginationPresenter
        {
            int TotalItems { get; set; }

            int ItemPerPage { get; set; }

            int CurrentPageIndex { get; set; }

            void SelectPage(int pageIndex);

            void NextPage();

            void PreviousPage();
        };
    }
}