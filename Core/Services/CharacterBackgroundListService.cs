using System.Collections.Generic;
using System.Linq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class CharacterBackgroundListService : IExpanseService
    {
        public List<CharacterBackGround> CharacterBackgroundList { get; private set; }
        public AbilityFocusListService FocusListService { get; private set; }
        public TalentListService TalentListService { get; private set; }

        public CharacterBackgroundListService(AbilityFocusListService abilityFocusListService, TalentListService talentListService)
        {
            CharacterBackgroundList = new List<CharacterBackGround>();
            FocusListService = abilityFocusListService;
            TalentListService = talentListService;
            PopulateBackgroundList();
        }

        private void PopulateBackgroundList()
        {
            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Academic",
                    "You spent a lot more time in schools and academic environments than your peers. Perhaps you lived at a boarding school, or someone in your family worked at a college or university. You’re inclined to be a bit more bookish and to  know your way around academic institutions.",
                    (CharacterSocialClass)2,
                    (CharacterAbilityName)5,
                    FocusListService.GetAbilityFocusList((CharacterAbilityName)5),
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Knowledge"),
                        TalentListService.GetTalent("Linguistics")
                    },
                    FocusListService.GetAbilityFocusList(CharacterAbilityName.Intelligence).Concat(
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Willpower,"Self-Discipline"),
                        new CharacterAbility(CharacterAbilityName.Communication,1),
                        new CharacterAbility(CharacterAbilityName.Willpower,1),
                        new CharacterAbility(CharacterAbilityName.Perception,1),
                        new Income(2)
                    }
                    ).ToList())
                );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Aristocratic",
                    "You come from a family with a history of privilege and responsibility, although it might have less of both these days. You might be actual nobility from an Earth culture that still has it, or “just” belong to an important family with equivalent wealth and influence.",
                    (CharacterSocialClass)3,
                    (CharacterAbilityName)3,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Etiquette"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"History"),
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Affluent"),
                        TalentListService.GetTalent("Contacts")
                    },
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        new CharacterAbility(CharacterAbilityName.Accuracy,1),
                        new CharacterAbility(CharacterAbilityName.Perception,1),
                        new CharacterAbility(CharacterAbilityName.Willpower,1),
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Persuasion"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Piloting"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Gambling"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Leadership"),
                        new Income(2)

                    }
                )
            );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Bohemian",
                    "You grew up in an experimental, eccentric, or creative community. Perhaps you belonged to a commune, an artist colony, or some “off the grid” intentional community. You may be the child of a group or polyamorous marriage, particularly on Earth. You’re unfamiliar with some of the things people take for granted, but had plenty of opportunities to expand your horizons. You might be trying to fit into mainstream society after some time away, or you might revel in your offbeat lifestyle.",
                    (CharacterSocialClass)0,
                    (CharacterAbilityName)3,
                    FocusListService.GetAbilityFocusList(CharacterAbilityName.Intelligence).Prepend(
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication, "Performing")).ToList(),
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Carousing"),
                        TalentListService.GetTalent("Performance")
                    },
                    FocusListService.GetAbilityFocusList(CharacterAbilityName.Communication).Concat(
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Acrobatics"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Free-fall"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Perception,"Empathy"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Willpower,"Courage"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Willpower,"Faith"),
                        new CharacterAbility(CharacterAbilityName.Dexterity,1),
                        new CharacterAbility(CharacterAbilityName.Constitution,1),
                        new CharacterAbility(CharacterAbilityName.Perception,1)
                    }
                    ).ToList())

            );


            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Corporate",
                    "Your early life was tied up in corporate culture, most likely due to family members who built their lives around a company in some fashion. Your family might be influential stockholders or include powerful executives, and they may well have expected you to follow their example—whether you did or not.",
                    (CharacterSocialClass)3,
                    (CharacterAbilityName)3,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Bargaining"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Business")
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Contacts"),
                        TalentListService.GetTalent("Intrigue")
                    },
                    FocusListService.GetAbilityFocusList(CharacterAbilityName.Communication).Concat(
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Evaluation"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Perception,"Empathy"),
                        new CharacterAbility(CharacterAbilityName.Perception,1),
                        new CharacterAbility(CharacterAbilityName.Intelligence,1),
                        new CharacterAbility(CharacterAbilityName.Accuracy,1)
                    }
                    ).ToList())

            );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Cosmopolitan",
                    "You were raised in a cosmopolitan environment: a big city, or even a series of great cities, where people from all over the world came and mixed. You were exposed to some of the best—and, potentially, the worst—of humanity and human achievement.",
                    (CharacterSocialClass)3,
                    (CharacterAbilityName)5,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Etiquette"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Current Affairs")
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Knowledge"),
                        TalentListService.GetTalent("Observation")
                    },
                    FocusListService.GetAbilityFocusList(CharacterAbilityName.Intelligence)
                        .Concat(FocusListService.GetAbilityFocusList(CharacterAbilityName.Communication)).Concat(
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Perception,"Seeing"),
                        new CharacterAbility(CharacterAbilityName.Perception,1),
                        new CharacterAbility(CharacterAbilityName.Communication,1),
                        new CharacterAbility(CharacterAbilityName.Willpower,1)
                    }
                    ).ToList())
            );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Exile",
                    "You might have lived a very different life were it not for some event that drove you from society. Feel free to roll again on the Social Class and Background tables to get a glimpse of what your former life was. Whatever the case, you (and possibly your family) were exiled by war, disaster, disgrace, or some other misfortune, forced to start over with virtually nothing.",
                    (CharacterSocialClass)0,
                    (CharacterAbilityName)1,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Fighting,"Brawling"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Willpower,"Self-Discipline")
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Affluent"),
                        TalentListService.GetTalent("Fringer")
                    },
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        new CharacterAbility(CharacterAbilityName.Fighting,1),
                        new CharacterAbility(CharacterAbilityName.Willpower,1),
                        new CharacterAbility(CharacterAbilityName.Perception,1),
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Bargaining"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Stealth"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Perception,"Searching"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Accuracy,"Pistols"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Driving")
                        
                    }
                    )
            );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Military",
                    "You grew up as a military brat. You were either a dependent with family members in military service, or you lived close by or on a military installation. You’re familiar with military culture and picked up a thing or two from it along the way.",
                    (CharacterSocialClass)1,
                    (CharacterAbilityName)2,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Accuracy,"Pistols"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Tactics")
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Observation"),
                        TalentListService.GetTalent("Dual Weapon Style"),
                        TalentListService.GetTalent("Grappling Style"),
                        TalentListService.GetTalent("Overwhelm Style"),
                        TalentListService.GetTalent("Pistol Style"),
                        TalentListService.GetTalent("Rifle Style"),
                        TalentListService.GetTalent("Self-Defense Style"),
                        TalentListService.GetTalent("Single Weapon Style"),
                        TalentListService.GetTalent("Striking Style"),
                        TalentListService.GetTalent("Thrown Weapon Style"),
                        TalentListService.GetTalent("Two-Handed Style"),
                    },
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        new CharacterAbility(CharacterAbilityName.Willpower,1),
                        new CharacterAbility(CharacterAbilityName.Strength,1),
                        new CharacterAbility(CharacterAbilityName.Constitution,1),
                        FocusListService.GetFocusByName(CharacterAbilityName.Accuracy,"Rifles"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Leadership"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Security"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Perception,"Searching"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Fighting,"Brawling")
                    }
                    )
            );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Outcast",
                    "Fate singled you out for the life of an outcast on the fringes of society. You might have a criminal background that makes it impossible to find decent work and true respect, or you might belong to a minority group your society rejects. On Earth, you might have been an unlicensed and unregistered birth, a literal non-person. Whatever the case, you had to learn how to survive outside of the safety and structures most people rely upon.",
                    (CharacterSocialClass)0,
                    (CharacterAbilityName)8,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Deception"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Stealth")
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Fringer"),
                        TalentListService.GetTalent("Misdirection")
                    },
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        new CharacterAbility(CharacterAbilityName.Communication,1),
                        new CharacterAbility(CharacterAbilityName.Perception,1),
                        new CharacterAbility(CharacterAbilityName.Constitution,1),
                        FocusListService.GetFocusByName(CharacterAbilityName.Perception,"Seeing"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Fighting,"Light Weapons"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Technology"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Sleight of Hand"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Willpower,"Courage")
                    }
                )
            );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Laborer",
                    "Your family is used to hard, physical work—and so are you.\r\nGenerations have worked the factory line, the mines, the assembly line, or the dockyards. You might be looking to move on and move up from there, or stick with it, or something might have upended the life you once knew, forcing you to move on.",
                    (CharacterSocialClass)1,
                    (CharacterAbilityName)1,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Crafting"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Strength,"Might")
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Grappling Style"),
                        TalentListService.GetTalent("Striking Style")
                    },
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        new CharacterAbility(CharacterAbilityName.Fighting,1),
                        new CharacterAbility(CharacterAbilityName.Strength,1),
                        new CharacterAbility(CharacterAbilityName.Dexterity,1),

                        FocusListService.GetFocusByName(CharacterAbilityName.Fighting,"Brawling"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Engineering"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Willpower,"Self-Discipline"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Gambling"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Constitution,"Stamina")
                    }
                )
            );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Suburban",
                    "Life for you once existed in the picturesque neighborhoods outside of the city but still just a short ride on public transportation to the shopping center. It might have been just as idyllic for you as the housing development brochures portrayed, or perhaps it was a cookie-cutter, conformist nightmare you couldn’t wait to escape.",
                    (CharacterSocialClass)2,
                    (CharacterAbilityName)3,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Etiquette"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Current Affairs")
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Affluent"),
                        TalentListService.GetTalent("Contacts")
                    },
                    FocusListService.GetAbilityFocusList(CharacterAbilityName.Intelligence)
                        .Concat(FocusListService.GetAbilityFocusList(CharacterAbilityName.Perception)).Concat(
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        new CharacterAbility(CharacterAbilityName.Perception,1),
                        new CharacterAbility(CharacterAbilityName.Intelligence,1),
                        new CharacterAbility(CharacterAbilityName.Dexterity,1),
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Persuasion"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Driving"),
                        new Income(2)
                    }
                ).ToList())
            );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Trade",
                    "Your family lived off a skilled trade of some sort. It might not have been glamorous (far from it, quite possibly) but it definitely paid the bills. You may have picked up some practical lessons and skills along the way. Perhaps you were ready to enter the family trade yourself, but you yearned for something new—and even dangerous.",
                    (CharacterSocialClass)2,
                    (CharacterAbilityName)4,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Crafting"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Engineering")
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Improvisation"),
                        TalentListService.GetTalent("Maker")
                    },
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        new CharacterAbility(CharacterAbilityName.Perception,1),
                        new CharacterAbility(CharacterAbilityName.Strength,1),
                        new CharacterAbility(CharacterAbilityName.Constitution,1),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Technology"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Art"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Constitution,"Tolerance"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Fighting,"Grappling"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Constitution,"Stamina")
                    }
                )
            );

            CharacterBackgroundList.Add(
                new CharacterBackGround(
                    "Urban",
                    "You lived in the city, not in a high-rise or someplace with private security, but in the city. Its streets and vacant lots were your playgrounds, and you were navigating public transportation long before you could drive (as if you ever needed to drive). You know the ins and outs of an urban environment, even if it’s not the particular city where you lived, even if it’s a large station in the depths of an asteroid or moon, in fact. You know local eccentrics, dangerous people, and the secrets from the heart of the city.",
                    (CharacterSocialClass)1,
                    (CharacterAbilityName)4,
                    new List<AbilityFocus>()
                    {
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Persuasion"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Constitution,"Stamina")
                    },
                    new List<CharacterTalent>()
                    {
                        TalentListService.GetTalent("Agility"),
                        TalentListService.GetTalent("Misdirection")
                    },
                    new List<IBackgroundOrProfessionBenefit>()
                    {
                        new CharacterAbility(CharacterAbilityName.Accuracy,1),
                        new CharacterAbility(CharacterAbilityName.Perception,1),
                        new CharacterAbility(CharacterAbilityName.Fighting,1),
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Acrobatics"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Deception"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Sleight of Hand"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Perception,"Hearing"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Strength,"Climbing"),
                        FocusListService.GetFocusByName(CharacterAbilityName.Strength,"Jumping")
                    }
                )
            );

        }
    }
}
