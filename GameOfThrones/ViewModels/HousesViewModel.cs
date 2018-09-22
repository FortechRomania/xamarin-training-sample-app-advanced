using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GameOfThrones.Collections;
using GameOfThrones.Resx;
using GameOfThrones.Services;

namespace GameOfThrones.ViewModels
{
    public class HousesViewModel : ViewModelBase
    {
        private readonly IHousesService _housesService;

        public HousesViewModel(IHousesService housesService)
        {
            _housesService = housesService;
            Houses = new SmartObservableCollection<HouseCellViewModel>();
        }

        public string Title => AppResources.HousesTitle;

        public SmartObservableCollection<HouseCellViewModel> Houses { get; }

        private int CurrentPage { get; set; } = 1;

        private bool CanLoadMoreCharacters => !LoadingMoreCharactersInProgress && !LoadedAll;

        private bool LoadingMoreCharactersInProgress { get; set; }

        private bool LoadedAll { get; set; }

        public async void ViewDidLoad()
        {
            await FetchHousesAsync();
        }

        public async Task FetchHousesAsync()
        {
            try
            {
                var houses = await _housesService.FetchHousesAsync(CurrentPage);
                var houseCellViewModels = houses.Select(house => new HouseCellViewModel(house));

                Houses.Reset(houseCellViewModels);
            }
            catch
            {
                // TODO - Handle exception
            }
        }

        public async void WillDisplayCellAtIndex(int index)
        {
            if (CanLoadMoreCharacters && index == Houses.Count - 1)
            {
                await FetchMoreCharactersAsync();
            }
        }

        public async Task FetchMoreCharactersAsync()
        {
            try
            {
                LoadingMoreCharactersInProgress = true;

                var houses = await _housesService.FetchHousesAsync(CurrentPage + 1);
                var houseCellViewModels = houses.Select(house => new HouseCellViewModel(house));

                if (houseCellViewModels.Any())
                {
                    Houses.AddRange(houseCellViewModels);
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
