using System.Linq;
using System.Threading.Tasks;
using GameOfThrones.Collections;
using GameOfThrones.Resx;
using GameOfThrones.Services;

namespace GameOfThrones.ViewModels
{
    public class CharactersViewModel
    {
        private readonly ICharactersService _charactersService;

        public CharactersViewModel(ICharactersService charactersService)
        {
            _charactersService = charactersService;
            Characters = new SmartObservableCollection<CharacterCellViewModel>();
        }

        public string Title => AppResources.CharactersTitle;

        public SmartObservableCollection<CharacterCellViewModel> Characters { get; }

        private int CurrentPage { get; set; } = 1;

        private bool CanLoadMoreCharacters => !LoadingMoreCharactersInProgress && !LoadedAll;

        private bool LoadingMoreCharactersInProgress { get; set; }

        private bool LoadedAll { get; set; }

        public async void ViewDidLoad()
        {
            await FetchCharactersAsync();
        }

        public async Task FetchCharactersAsync()
        {
            try
            {
                LoadingMoreCharactersInProgress = true;

                var characters = await _charactersService.FetchCharactersAsync(CurrentPage);
                var characterCellViewModels = characters.Select(character => new CharacterCellViewModel(character));

                Characters.Reset(characterCellViewModels);
            }
            catch
            {
                // TODO - Handle exception
            }
            finally
            {
                LoadingMoreCharactersInProgress = false;
            }
        }

        public async void WillDisplayCellAtIndex(int index)
        {
            if (CanLoadMoreCharacters && index == Characters.Count - 1)
            {
                await FetchMoreCharactersAsync();
            }
        }

        public async Task FetchMoreCharactersAsync()
        {
            try
            {
                LoadingMoreCharactersInProgress = true;

                var characters = await _charactersService.FetchCharactersAsync(CurrentPage + 1);
                var characterCellViewModels = characters.Select(character => new CharacterCellViewModel(character));

                if (characterCellViewModels.Any())
                {
                    Characters.AddRange(characterCellViewModels);
                    CurrentPage++;
                }
                else
                {
                    LoadedAll = true;
                }
            }
            catch
            {
                // TODO - Handle exception
            }
            finally
            {
                LoadingMoreCharactersInProgress = false;
            }
        }
    }
}
