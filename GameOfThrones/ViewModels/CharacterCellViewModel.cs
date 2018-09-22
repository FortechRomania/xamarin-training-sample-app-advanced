using System.Linq;
using GameOfThrones.Data;
using GameOfThrones.Resx;

namespace GameOfThrones.ViewModels
{
    public class CharacterCellViewModel
    {
        public CharacterCellViewModel(Character character)
        {
            Character = character;

            if (!string.IsNullOrEmpty(Character.Name) && Character.Aliases.Any() && !string.IsNullOrEmpty(Character.Aliases.First()))
            {
                NameAndNickname = $"{Character.Name} ({string.Join(", ", Character.Aliases)})";
            }
            else
            {
                NameAndNickname = !string.IsNullOrEmpty(Character.Name) 
                                         ? Character.Name 
                                         : string.Join(",", Character.Aliases);
            }

            BornInformation = !string.IsNullOrEmpty(Character.Born)
                ? string.Format(AppResources.CharacterBornFormat, Character.Born)
                : AppResources.CharacterNoBirthInformation;

            PlayedBy = Character.PlayedBy.Any() && !string.IsNullOrEmpty(Character.PlayedBy.First())
                                ? string.Format(AppResources.CharacterPlayedByFormat, string.Join(", ", Character.PlayedBy))
                                : AppResources.CharacterNoPlayedByInformation;
        }

        public Character Character { get; }

        public string NameAndNickname { get; }

        public string BornInformation { get; }

        public string PlayedBy { get; }
    }
}
