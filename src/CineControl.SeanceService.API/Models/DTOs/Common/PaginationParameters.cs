namespace CineControl.SeanceService.API.Models.DTOs.Common
{
    public class PaginationParameters
    {
        private int _pageSize = 10;
        private const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}