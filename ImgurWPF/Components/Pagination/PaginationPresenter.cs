using ImgurApp.Components.Pagination;
using System;
using System.Collections.Generic;
using static ImgurWPF.Components.Pagination.PaginationContract;

namespace ImgurWPF.Components.Pagination
{
    public class PaginationPresenter : IPaginationPresenter
    {
        private int _totalItems = 0;

        public int TotalItems
        {
            get { return _totalItems; }
            set
            {
                _totalItems = value;
                Console.WriteLine($"總 items {TotalItems}");
                // 處理多餘的頁數，當除不盡時要多一個 pagination 來顯示
                // total item = 51，一頁 5 個 item，會有 11 頁
                TotalPageIndexCount = _totalItems % ItemPerPage == 0 ?
                    _totalItems / ItemPerPage :
                    (_totalItems / ItemPerPage) + 1;
                Console.WriteLine($"總 pagination {TotalPageIndexCount}");

                // 初始化第一頁
                CurrentPageIndex = 1;

                // 處理最後一頁無法填滿預設 pagination 數量
                // 例如預設顯示 10 頁，但僅剩 5 頁的資料
                int indexToBeDisplayed = this.CalcPageIndexToBeDisplayed;
                Console.WriteLine($"剩餘顯示 {indexToBeDisplayed}");

                List<PageIndex> pages = new List<PageIndex>();
                for (int i = CurrentPageIndex; i <= indexToBeDisplayed; i++)
                {
                    var page = new PageIndex(i);
                    pages.Add(page);
                }
                _view.OnPaginationChanged(pages);
                _view.OnPageChanged(CurrentPageIndex);
            }
        }

        public int ItemPerPage { get; set; } = 5;

        public int DefaultPageIndexToBeDisplayed { get; set; } = 10;

        // 每個 pagination 可以處理 10 頁，
        // 如果 default < 全部，則可顯示 10 頁，
        // 否則 顯示剩餘頁數 1 頁
        public int CalcPageIndexToBeDisplayed =>
            (DefaultPageIndexToBeDisplayed < TotalPageIndexCount - CurrentPageIndex) ?
                DefaultPageIndexToBeDisplayed : TotalPageIndexCount % DefaultPageIndexToBeDisplayed;

        public int PageIndexToBeDisplayed { get; set; }

        public int TotalPageIndexCount { get; set; }

        private int _currentPageIndex = 1;

        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set
            {
                if (value <= 0) _currentPageIndex = 1;
                else if (value > TotalPageIndexCount) _currentPageIndex = TotalPageIndexCount;
                else
                {
                    _currentPageIndex = value;
                    // onpagechange
                }
            }
        }

        private readonly IPaginationView _view;

        public PaginationPresenter(IPaginationView view)
        {
            this._view = view;
        }

        public void SelectPage(int pageIndex)
        {
            this.CurrentPageIndex = pageIndex;
            this._view.OnPageChanged(pageIndex);
        }

        public void NextPage()
        {
            // 最後頁
            if (CurrentPageIndex == TotalPageIndexCount) return;

            CurrentPageIndex++;

            if (CurrentPageIndex % DefaultPageIndexToBeDisplayed == 1)
            {
                // change to next 10 page
                int indexToBeDisplayed = this.CalcPageIndexToBeDisplayed;
                int end = indexToBeDisplayed - 1 + CurrentPageIndex;
                Console.WriteLine($"next 剩餘顯示 {end}");

                List<PageIndex> list = new List<PageIndex>();
                for (int i = CurrentPageIndex; i <= end; i++)
                {
                    var page = new PageIndex(i);
                    list.Add(page);
                }
                this._view.OnPaginationChanged(list);
            }

            this._view.OnPageChanged(CurrentPageIndex);
        }

        public void PreviousPage()
        {
            if (CurrentPageIndex == 1) return;

            CurrentPageIndex--;

            // (21 - 1) / 10 餘 0 翻頁
            // (7 - 1) / 10 餘 7 不翻頁
            if (CurrentPageIndex % DefaultPageIndexToBeDisplayed == 0)
            {
                int start = CurrentPageIndex - DefaultPageIndexToBeDisplayed + 1;
                Console.WriteLine($"previous 剩餘顯示 {CurrentPageIndex}");

                List<PageIndex> list = new List<PageIndex>();
                for (int i = start; i <= CurrentPageIndex; i++)
                {
                    var page = new PageIndex(i);
                    list.Add(page);
                }
                this._view.OnPaginationChanged(list);
            }

            this._view.OnPageChanged(CurrentPageIndex);
        }
    }
}