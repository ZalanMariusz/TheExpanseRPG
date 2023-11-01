using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class OriginSelectViewModel : CharacterCreationViewModelBase
    {
        public string? _selectedOriginDescription;
        public string? SelectedOriginDescription
        {
            get { return _selectedOriginDescription; }
            set { _selectedOriginDescription = value; OnPropertyChanged(); }
        }
        public CharacterOrigin? SelectedOrigin
        {
            get { return CharacterCreationService!.CharacterOrigin; }
            set
            {
                CharacterCreationService!.CharacterOrigin = value;
                OnPropertyChanged();
                ChangeOriginDescription();
            }
        }
        private void ChangeOriginDescription()
        {
            switch (SelectedOrigin)
            {
                case CharacterOrigin.Earth:
                    SelectedOriginDescription =
                        "With a population of some 30 billion, many Earthers are unemployed and live on government-provided Basic Assistance (generally known as just “Basic”) which provides for their essential food, housing, and medical needs, but little else. You are likely one of the few to leave Earth to find a new life elsewhere. " +
                        "\n\n• Your native gravity is normal gravity—“Earth-normal” or 1 g. Earthers can and do learn to operate in lower gravity, but lack the instincts of people raised in it. " +
                        "\n\n• Earthers have greater muscle and bone density from being raised in a gravity well, making them shorter and more broadly built than Belters or even native-born Martians, but Earthers in space need regular exercise and medical treatments to avoid muscle atrophy and bone density loss.";
                    break;
                case CharacterOrigin.Mars:
                    SelectedOriginDescription =
                        "Born in the Martian Congressional Republic, your life has been influenced by the Martian dream: to terraform the Red Planet into a lush and life-sustaining garden. Like the generations before you, you know that you will likely never see the completion of this work in your lifetime." +
                        "\n\n• Your native gravity is low, the gravity of Mars rather than Earth. Martians are more comfortable with microgravity than Earthers, and better able to tolerate a full 1 g than Belters, operating in-between.";
                    break;
                case CharacterOrigin.Belt:
                    SelectedOriginDescription =
                        "You were born and raised in the Black, on a station or ship, and have lived most, if not all, of your life out in the Belt or beyond. Separated from death by nothing more than basic support systems your whole life, you have learned to be cautious and aware of your environment. " +
                        "\n\n• Your native gravity is microgravity. Belters are most comfortable “on the float” and handle moving in free-fall easily. You automatically have the Dexterity (Free-fall) focus. Conversely, Earth-normal gravity is crushingly heavy for a Belter." +
                        "\n\n• You speak Belter Creole, a combination of loan-words and phrases from various languages, combined with gestures useful for communicating while wearing a vac suit and unable to speak." +
                        "\n\n• Belters tend to be tall and willowy as a result of being raised in low - or microgravity environments.Regimens of bone - density drugs and genetic treatments are needed to keep Belters healthy, and some Belters have minor physical abnormalities because of this." +
                        "\n\n• Belters often have a diverse ethnic heritage, given the “melting pot” of the Belt, with ancestors from many different Earth cultures.";
                    break;
            }
        }
    }
}
