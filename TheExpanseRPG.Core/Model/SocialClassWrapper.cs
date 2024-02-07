using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.Model;
public class SocialClassWrapper
{
    public SocialClassWrapper(CharacterSocialClass socialClass, string description)
    {
        SocialClass = socialClass;
        Description = description;
    }

    public CharacterSocialClass SocialClass { get; }
    public string Description { get; }
}
