using System;
using System.Globalization;
using System.Windows.Data;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.Converters
{
    class SocialClassToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            if(Enum.TryParse(value.ToString(),out CharacterSocialClass selectedSocialClass))
            {
                switch (selectedSocialClass) 
                {
                    case CharacterSocialClass.Outsider:
                        return "More of a non-social class, outsiders tend to be outcasts, criminals, or non-conformists who can’t or won’t live according to society’s customs. They often lack access to things other people take for granted and learn to get by on their own, some times forming their own support networks and structures outside of those of mainstream society. Some outsiders reject the mainstream by choice, but in many cases, outsiders are pushed out by society’s biases.";
                    case CharacterSocialClass.Lower:
                        return "Hard, usually physical, labor and precarious employment tend to rule the lives of lower class characters. Still, that work is often all that separates them from becoming outsiders, so they cling to it. Lower class characters often depend on family and friends to help keep them out of utter poverty. They might live in failing industrial areas, inner city slums, or hardscrabble farms. In all cases, they make do with what is available and find ways to stretch out resources until the next payday or job comes along.";
                    case CharacterSocialClass.Middle:
                        return "A measure of comfort and security comes with the middle class. A steady job, often skilled labor or “white collar,” supplies the means to afford a few luxuries or non-essentials. Middle class characters might start off as a bit insular. They often separate themselves from the struggles of the lower social classes, focusing on the climb towards upper class status. Sometimes that climb leads to a slip. They tumble down to the lower class or even become outsiders. Some settle for stability instead, and prefer not to rock the boat.";
                    case CharacterSocialClass.Upper:
                        return "Upper class characters sit at a society’s summit where they rarely need to worry about resources, except, of course, when they want more. Their concerns are often focused on the responsibilities and privileges associated with their status. Some are born into upper class privilege, inheriting wealth and opportunity, while others worked their way up to join the elite. In some societies, it’s almost impossible to work your way to upper class status, and even if you do, you might get less respect compared to hereditary “old money” peers.";
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
