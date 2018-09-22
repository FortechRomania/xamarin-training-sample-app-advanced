using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GameOfThrones.Collections;
using GameOfThrones.Resx;
using GameOfThrones.Services;

namespace GameOfThrones.ViewModels
{
    public class BooksViewModel : ViewModelBase
    {
        private readonly IBooksService _booksService;

        public BooksViewModel(IBooksService booksService)
        {
            _booksService = booksService;

            Books = new SmartObservableCollection<BookCellViewModel>();
        }

        public string Title => AppResources.BooksTitle;

        public SmartObservableCollection<BookCellViewModel> Books { get; }

        public async void ViewDidLoad()
        {
            await FetchBooksAsync();
        }

        public async Task FetchBooksAsync()
        {
            try
            {
                var books = await _booksService.FetchBooksAsync();
                var bookCellViewModels = books.Select(book => new BookCellViewModel(book));

                Books.Reset(bookCellViewModels);
            }
            catch
            {
                // TODO - Handle exception
            }
        }
    }
}
