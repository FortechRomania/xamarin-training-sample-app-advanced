using GameOfThrones.Data;
using GameOfThrones.Resx;

namespace GameOfThrones.ViewModels
{
    public class HouseCellViewModel
    {
        public HouseCellViewModel(House house)
        {
            House = house;

            Name = !string.IsNullOrEmpty(House.Region)
                          ? $"{House.Name} ({House.Region})"
                          : House.Name;
            Words = !string.IsNullOrEmpty(House.Words)
                           ? string.Format(AppResources.HouseWordsFormat, House.Words)
                           : AppResources.HouseNoWordsInformation;
            CoatOfArms = !string.IsNullOrEmpty(House.CoatOfArms)
                                ? string.Format(AppResources.HouseCoatOfArmsFormat, House.CoatOfArms)
                                : AppResources.HouseNoCoatOfArmsInformation;

        }

        public string Name { get; }

        public string Words { get; }

        public string CoatOfArms { get; }

        internal House House { get; }
    }
}
