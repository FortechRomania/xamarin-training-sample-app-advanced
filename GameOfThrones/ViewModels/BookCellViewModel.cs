using GameOfThrones.Data;
using GameOfThrones.Resx;

namespace GameOfThrones.ViewModels
{
    public class BookCellViewModel
    {
        public BookCellViewModel(Book book)
        {
            Book = book;

            Name = Book.Name;
            Authors = string.Join(", ", Book.Authors);
            ReleaseDate = Book.Released.ToString("D");
            NumberOfPages = string.Format(AppResources.BookPagesFormat, Book.NumberOfPages);
        }

        public string Name { get; }

        public string Authors { get; }

        public string ReleaseDate { get; set; }

        public string NumberOfPages { get; set; }

        internal Book Book { get; }
    }
}
