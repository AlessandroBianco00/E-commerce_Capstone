namespace ApiBookStore.DTO
{
    public class SearchDto
    {
        public int Pages { get; set; }
        public List<BookSearchDto> Books { get; set; }
    }
}
