using System.Runtime.CompilerServices;
using System.Text.Json;
using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services;

public class CharacterCreationService : IExpanseService
{
    public ICharacterAbilityBlockBuilder CharacterAbilityBlockBuilder { get; set; }

    public int? GetTotalIncome()
    {
        List<int?> totalIncome = new();
        int? professionIncome = SelectedCharacterProfession?.IncomeBase;
        int? socialClassProfessionDiff = SelectedCharacterSocialClass - SelectedCharacterProfession?.ProfessionSocialClass;
        int? incomeBonuses = Bonuses.Values.Where(x => x is Income).Cast<Income>().Sum(x => x.Value);
        int? affluentBonus = default;

        CharacterTalent? affluent = TalentBonuses.FirstOrDefault(x => x.TalentName == "Affluent");
        if (affluent != null)
        {
            affluentBonus = (int)affluent.Degree + 2;
        }

        return new List<int?>() {
            professionIncome,
            socialClassProfessionDiff,
            incomeBonuses,
            affluentBonus
        }.Sum();
    }
    public Dictionary<string, ICharacterCreationBonus> Bonuses { get; set; } = new();
    public string CharacterName { get; set; } = string.Empty;
    public string CharacterDescription { get; set; } = string.Empty;
    #region Abilities
    public int? Speed { get => 10 + (CharacterAbilityBlockBuilder.GetDexterityTotal() is null ? 0 : (int)CharacterAbilityBlockBuilder.GetDexterityTotal()!); }
    public int Defense { get => 10 + (CharacterAbilityBlockBuilder.GetDexterityTotal() is null ? 0 : (int)CharacterAbilityBlockBuilder.GetDexterityTotal()!); }
    public int Toughness { get => CharacterAbilityBlockBuilder.GetConstitutionTotal() is null ? 0 : (int)CharacterAbilityBlockBuilder.GetConstitutionTotal()!; }
    public int Fortune { get => 15 + (SelectedDriveBonus?.GetType() == typeof(Fortune) ? 5 : 0); }
    #endregion
    #region Origin
    private Dictionary<CharacterOrigin, string?> _originDescriptions = new();
    public event EventHandler? OriginChanged;
    private CharacterOrigin? _selectedCharacterOrigin;
    public CharacterOrigin? SelectedCharacterOrigin
    {
        get => _selectedCharacterOrigin;
        set
        {
            _selectedCharacterOrigin = value;
            if (value == CharacterOrigin.Belt)
            {
                SyncBonusDictionary();
            }
            else
            {
                Bonuses.Remove(nameof(SelectedCharacterOrigin));
            }
            OriginChanged?.Invoke(this, new EventArgs());
        }
    }
    public string? CurrentOriginDescription
    {
        get { return SelectedCharacterOrigin is null ? null : _originDescriptions[(CharacterOrigin)SelectedCharacterOrigin]; }
    }
    #endregion
    #region Social class
    private Dictionary<CharacterSocialClass, string?> _socialClassDescriptions = new();
    public event EventHandler? SocialClassChanged;
    public string? SelectedCharacterSocialClassDescription
    {
        get { return SelectedCharacterSocialClass is null ? null : _socialClassDescriptions[(CharacterSocialClass)SelectedCharacterSocialClass]; }
    }
    private CharacterSocialClass? _selectedCharacterSocialClass;
    public CharacterSocialClass? SelectedCharacterSocialClass
    {
        get { return _selectedCharacterSocialClass; }
        set { _selectedCharacterSocialClass = value; ClearSelectedProfession(); SocialClassChanged?.Invoke(this, new EventArgs()); }
    }
    public List<SocialClassWrapper> SocialClassesWithDescription { get; set; } = new();
    #endregion
    #region Background and Background bonuses
    private CharacterBackGround? _selectedCharacterBackground;
    public event EventHandler? BackgroundChanged;
    public event EventHandler? SelectedBackgroundBenefitChanged;
    public event EventHandler? SelectedBackgroundFocusChanged;
    public CharacterBackGround? SelectedCharacterBackground
    {
        get { return _selectedCharacterBackground; }
        set
        {
            _selectedCharacterBackground = value;
            SyncBonusDictionary();
            BackgroundChanged?.Invoke(this, new EventArgs());
        }
    }
    private ICharacterCreationBonus? _selectedBackgroundBenefit;
    public ICharacterCreationBonus? SelectedBackgroundBenefit
    {
        get { return _selectedBackgroundBenefit; }
        set
        {
            _selectedBackgroundBenefit = value;
            SyncBonusDictionary();
            SelectedBackgroundBenefitChanged?.Invoke(this, new EventArgs());
        }
    }
    public CharacterTalent? SelectedBackgroundTalent
    {
        get => _selectedBackgroundTalent;
        set
        {
            _selectedBackgroundTalent = value;
            SyncBonusDictionary();
        }
    }
    public AbilityFocus? SelectedBackgroundFocus
    {
        get => _selectedBackgroundFocus;
        set
        {
            _selectedBackgroundFocus = value;
            SyncBonusDictionary();
            SelectedBackgroundFocusChanged?.Invoke(this, new EventArgs());
        }
    }
    #endregion
    #region Drive and Drive bonuses
    public event EventHandler? DriveSelectionChanged;
    private CharacterDrive? _selectedCharacterDrive;
    public CharacterDrive? SelectedCharacterDrive { get { return _selectedCharacterDrive; } set { _selectedCharacterDrive = value; DriveSelectionChanged?.Invoke(this, new EventArgs()); } }
    public CharacterTalent? SelectedDriveTalent
    {
        get => _selectedDriveTalent;
        set
        {
            _selectedDriveTalent = value;
            SyncBonusDictionary();
        }
    }
    public ICharacterCreationBonus? SelectedDriveBonus
    {
        get => _selectedDriveBonus;
        set
        {
            _selectedDriveBonus = value;
            SyncBonusDictionary();
        }
    }
    #endregion
    #region Profession and Profession bonuses
    private CharacterProfession? _selectedCharacterProfession;
    private CharacterTalent? _selectedBackgroundTalent;
    private AbilityFocus? _selectedBackgroundFocus;
    private CharacterTalent? _selectedDriveTalent;
    private ICharacterCreationBonus? _selectedDriveBonus;
    private CharacterTalent? _selectedProfessionTalent;
    private AbilityFocus? _selectedProfessionFocus;
    public event EventHandler? ProfessionChanged;
    public event EventHandler? ProfessionFocusChanged;
    public CharacterProfession? SelectedCharacterProfession
    {
        get { return _selectedCharacterProfession; }
        set { ClearProfessionBonuses(value); _selectedCharacterProfession = value; ProfessionChanged?.Invoke(this, new EventArgs()); }
    }
    public CharacterTalent? SelectedProfessionTalent
    {
        get => _selectedProfessionTalent;
        set
        {
            _selectedProfessionTalent = value;
            SyncBonusDictionary();
        }
    }
    public AbilityFocus? SelectedProfessionFocus
    {
        get => _selectedProfessionFocus;
        set
        {
            _selectedProfessionFocus = value;
            SyncBonusDictionary();
            ProfessionFocusChanged?.Invoke(this, new EventArgs());
        }
    }
    public List<CharacterProfession> OutsiderProfessions { get; private set; } = new();
    public List<CharacterProfession> LowerclassProfessions { get; private set; } = new();
    public List<CharacterProfession> MiddleclassProfessions { get; private set; } = new();
    public List<CharacterProfession> UpperclassProfessions { get; private set; } = new();
    #endregion
    #region Services
    public ITalentListService TalentListService { get; }
    public IAbilityFocusListService FocusListService { get; }
    public ICharacterProfessionListService ProfessionListService { get; }
    public ICharacterBackgroundListService BackgroundListService { get; }
    public ICharacterDriveListService CharacterDriveListService { get; }
    public List<CharacterTalent> TalentBonuses { get; set; } = new();
    public string CharacterAvatar { get; set; } = string.Empty;

    private void ClearSelectedProfession()
    {
        if (SelectedCharacterProfession != null)
        {
            if (SelectedCharacterProfession.ProfessionSocialClass > SelectedCharacterSocialClass)
            {
                SelectedCharacterProfession = null;
            }
        }
    }
    private void ClearProfessionBonuses(CharacterProfession? newProfession)
    {
        if (newProfession != null && newProfession != SelectedCharacterProfession)
        {
            SelectedProfessionTalent = null;
            SelectedProfessionFocus = null;
        }
    }

    #endregion
    public CharacterCreationService(
        ICharacterAbilityBlockBuilder characterAbilityBlockBuilder,
        ITalentListService talentListService,
        IAbilityFocusListService focusListService,
        ICharacterBackgroundListService backgroundListService,
        //IDiceRollService diceRollService,
        ICharacterProfessionListService professionListService,
        ICharacterDriveListService characterTalentListService
        )
    {
        CharacterAbilityBlockBuilder = characterAbilityBlockBuilder;
        TalentListService = talentListService;
        FocusListService = focusListService;

        //DiceRollService = diceRollService;
        ProfessionListService = professionListService;
        BackgroundListService = backgroundListService;
        CharacterDriveListService = characterTalentListService;
        InitializeOriginDescriptions();
        InitiliazeSocialClassDescriptions();
        InitiliazeSocialClassWrapperList();
        InitializeProfessionLists();
    }

    private void InitiliazeSocialClassWrapperList()
    {
        SocialClassesWithDescription = new()
        {
            new(CharacterSocialClass.Outsider, _socialClassDescriptions[CharacterSocialClass.Outsider]!),
            new(CharacterSocialClass.Lower, _socialClassDescriptions[CharacterSocialClass.Lower]!),
            new(CharacterSocialClass.Middle, _socialClassDescriptions[CharacterSocialClass.Middle]!),
            new(CharacterSocialClass.Upper, _socialClassDescriptions[CharacterSocialClass.Upper]!),
        };
    }

    public void RefreshTalentBonuses()
    {
        TalentBonuses.Clear();
        IEnumerable<CharacterTalent> currentBonusList = Bonuses.Values.Where(x => x is CharacterTalent).Cast<CharacterTalent>();
        foreach (CharacterTalent talent in currentBonusList)
        {
            CharacterTalent? alreadySelected = TalentBonuses.FirstOrDefault(x => x.TalentName == talent.TalentName);
            if (alreadySelected is not null)
            {
                alreadySelected.ImproveTalent();
            }
            else
            {
                TalentBonuses.Add((CharacterTalent)talent.ShallowCopy());
            }

        }
    }
    private void SyncBonusDictionary([CallerMemberName] string bonusPropertyName = "")
    {
        var bonusValue = GetType().GetProperty(bonusPropertyName)!.GetValue(this);
        if (bonusPropertyName == nameof(SelectedCharacterOrigin))
        {
            bonusValue = FocusListService.GetFocusByName(CharacterAbilityName.Dexterity, "Free-fall");
        }

        Bonuses.Remove(bonusPropertyName);

        if (bonusValue is not null)
        {
            if (bonusValue is CharacterBackGround)
            {
                bonusValue = SelectedCharacterBackground!.AbilityBonus;
            }
            Bonuses.Add(bonusPropertyName, (bonusValue as ICharacterCreationBonus)!);
        }
        RefreshTalentBonuses();
    }
    #region Ability methods

    public List<AbilityFocus> GetAbilityFocuses(CharacterAbilityName abilityName)
    {
        return Bonuses.Values.Where(x => x is AbilityFocus focus && focus.AbilityName == abilityName).Cast<AbilityFocus>().Distinct().ToList();
    }
    public List<AbilityFocus> GetAllAbilityFocus()
    {
        return Bonuses.Values.Where(x => x is AbilityFocus focus).Cast<AbilityFocus>().Distinct().ToList();
    }
    #endregion
    #region Conflict checkers
    public List<string> GetBackgroundFocusConflicts()
    {
        if (Bonuses.TryGetValue(nameof(SelectedBackgroundFocus), out ICharacterCreationBonus? backgroundFocus))
        {
            return Bonuses.Where(x => x.Value.CreationBonusName == backgroundFocus.CreationBonusName && x.Key != nameof(SelectedBackgroundFocus)).ToDictionary(x => x.Key, x => x.Key).Keys.ToList();
        }
        return new();

    }
    public List<string> GetBackgroundBenefitConflicts()
    {
        if (Bonuses.TryGetValue(nameof(SelectedBackgroundBenefit), out ICharacterCreationBonus? backgroundBenefit))
        {
            if (backgroundBenefit is AbilityFocus)
            {
                return Bonuses.Where(x => x.Value.CreationBonusName == backgroundBenefit.CreationBonusName && x.Key != nameof(SelectedBackgroundBenefit)).ToDictionary(x => x.Key, x => x.Key).Keys.ToList();
            }
            return new();
        }
        return new();
    }
    public List<string> GetOriginFocusConflicts()
    {
        if (Bonuses.TryGetValue(nameof(SelectedCharacterOrigin), out ICharacterCreationBonus? originFocus))
        {
            return Bonuses.Where(x => x.Value.CreationBonusName == originFocus.CreationBonusName && x.Key != nameof(SelectedCharacterOrigin)).ToDictionary(x => x.Key, x => x.Key).Keys.ToList();
        }
        return new();

    }

    public List<string> GetProfessionFocusConflicts()
    {
        if (Bonuses.TryGetValue(nameof(SelectedProfessionFocus), out ICharacterCreationBonus? professionFocus))
        {
            return Bonuses.Where(x => x.Value.CreationBonusName == professionFocus.CreationBonusName && x.Key != nameof(SelectedProfessionFocus)).ToDictionary(x => x.Key, x => x.Key).Keys.ToList();
        }
        return new();
    }

    public bool HasBackgroundConflict()
    {
        return GetBackgroundBenefitConflicts().Any() || GetBackgroundFocusConflicts().Any();
    }
    public bool HasOriginConflict()
    {
        return GetOriginFocusConflicts().Any();
    }
    public bool HasProfessionConflict()
    {
        return GetProfessionFocusConflicts().Any();
    }
    #endregion
    #region List and description initializers
    private void InitializeOriginDescriptions()
    {
        _originDescriptions = new()
        {
            { CharacterOrigin.Earth,ModelResources.DescriptionOriginEarth },
            { CharacterOrigin.Mars,ModelResources.DescriptionOriginMars },
            { CharacterOrigin.Belt,ModelResources.DescriptionOriginBelt }
        };
    }
    private void InitiliazeSocialClassDescriptions()
    {
        _socialClassDescriptions = new()
        {
            //{ CharacterSocialClass.Outsider,"More of a non-social class, outsiders tend to be outcasts, criminals, or non-conformists who can’t or won’t live according to society’s customs. They often lack access to things other people take for granted and learn to get by on their own, some times forming their own support networks and structures outside of those of mainstream society. Some outsiders reject the mainstream by choice, but in many cases, outsiders are pushed out by society’s biases."},
            { CharacterSocialClass.Outsider,ModelResources.DescriptionOutsiderClass},
            { CharacterSocialClass.Lower,ModelResources.DescriptionLowerClass},
            { CharacterSocialClass.Middle,ModelResources.DescriptionMiddleClass},
            { CharacterSocialClass.Upper,ModelResources.DescriptionUpperClass},
        };
    }
    private void InitializeProfessionLists()
    {
        OutsiderProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Outsider));
        LowerclassProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Lower));
        MiddleclassProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Middle));
        UpperclassProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Upper));
    }

    public bool IsMissingBackgroundBonus()
    {
        return SelectedBackgroundBenefit is null || SelectedBackgroundFocus is null || SelectedBackgroundTalent is null;
    }
    public bool IsMissingProfessionBonus()
    {
        return SelectedProfessionFocus is null || SelectedProfessionTalent is null;
    }
    public bool IsMissingDriveBonus()
    {
        return SelectedDriveBonus is null || SelectedDriveTalent is null;
    }


    public bool CanCreateCharacter()
    {
        List<bool> validationProperties = new()
        {
            IsMissingBackgroundBonus(),
            IsMissingProfessionBonus(),
            IsMissingDriveBonus(),

            CharacterAbilityBlockBuilder.IsMissingAbilityRoll(),

            HasBackgroundConflict(),
            HasOriginConflict(),
            HasProfessionConflict(),

            SelectedCharacterOrigin is null,
            string.IsNullOrEmpty(CharacterName)
        };

        return !validationProperties.Any(x => x == true);
    }

    #endregion

    public bool CharacterExists()
    {
        return File.Exists($"{ModelResources.CharacterSavePath}{CharacterName}.json");
    }
    public void CreateCharacter()
    {
        //ExpanseCharacter character = new()
        //{
        //    Abilities = CharacterAbilityBlock,
        //    Focuses = GetAllAbilityFocus(),
        //    Talents = TalentBonuses,
        //    Name = CharacterName,
        //    Description = CharacterDescription,
        //    Background = SelectedCharacterBackground!.BackgroundName,
        //    Origin = SelectedCharacterOrigin,
        //    SocialClass = SelectedCharacterSocialClass,
        //    Profession = SelectedCharacterProfession!.ProfessionName,
        //    Drive = SelectedCharacterDrive!.DriveName,
        //    Fortune = Fortune,
        //    Income = GetTotalIncome(),
        //    Speed = Speed,
        //    Toughness = Toughness,
        //    Defense = Defense,
        //    Avatar = CharacterAvatar

        //};

        //string characterJson = JsonSerializer.Serialize(character, new JsonSerializerOptions { WriteIndented = true });
        //Directory.CreateDirectory(ModelResources.CharacterSavePath);
        //File.WriteAllText($"{ModelResources.CharacterSavePath}{CharacterName}.json", characterJson);

    }

    public void RandomizeCharacter()
    {
        //SelectedCharacterOrigin = (CharacterOrigin)Random.Shared.Next(0, 3);
        //SelectedCharacterSocialClass = (CharacterSocialClass)Random.Shared.Next(0, 4);

        //var possibleBackgrounds = BackgroundListService.CharacterBackgroundList.Where(bg => bg.MainSocialClass == SelectedCharacterSocialClass).ToList();
        //SelectedCharacterBackground = possibleBackgrounds[Random.Shared.Next(0, possibleBackgrounds.Count)];
        //SelectedBackgroundFocus = SelectedCharacterBackground!.PossibleAbilityFocuses[Random.Shared.Next(0, SelectedCharacterBackground.PossibleAbilityFocuses.Count)];
        //while (HasBackgroundConflict())
        //{
        //    SelectedBackgroundFocus = SelectedCharacterBackground!.PossibleAbilityFocuses[Random.Shared.Next(0, SelectedCharacterBackground.PossibleAbilityFocuses.Count)];
        //}
        //SelectedBackgroundTalent = SelectedCharacterBackground.PossiblePlayerTalents[Random.Shared.Next(0, SelectedCharacterBackground.PossiblePlayerTalents.Count)];
        //SelectedBackgroundBenefit = SelectedCharacterBackground.BackgroundBenefits[Random.Shared.Next(0, SelectedCharacterBackground.BackgroundBenefits.Count)];
        //while (HasBackgroundConflict())
        //{
        //    SelectedBackgroundBenefit = SelectedCharacterBackground.BackgroundBenefits[Random.Shared.Next(0, SelectedCharacterBackground.BackgroundBenefits.Count)];
        //}

        //var possibleProfessions = ProfessionListService.ProfessionList.Where(p => p.ProfessionSocialClass <= SelectedCharacterSocialClass).ToList();
        //SelectedCharacterProfession = possibleProfessions[Random.Shared.Next(0, possibleProfessions.Count)];
        //SelectedProfessionFocus = SelectedCharacterProfession.FocusChoices[Random.Shared.Next(0, SelectedCharacterProfession.FocusChoices.Count)];
        //while (HasProfessionConflict())
        //{
        //    SelectedProfessionFocus = SelectedCharacterProfession.FocusChoices[Random.Shared.Next(0, SelectedCharacterProfession.FocusChoices.Count)];
        //}
        //SelectedProfessionTalent = SelectedCharacterProfession.TalentChoices[Random.Shared.Next(0, SelectedCharacterProfession.TalentChoices.Count)];

        //SelectedCharacterDrive = CharacterDriveListService.DriveList[Random.Shared.Next(0, CharacterDriveListService.DriveList.Count)];
        //SelectedDriveBonus = CharacterDriveListService.DriveBonuses[Random.Shared.Next(0, CharacterDriveListService.DriveBonuses.Count)];
        //SelectedDriveTalent = CharacterDriveListService.DriveTalentList[SelectedCharacterDrive.DriveName][Random.Shared.Next(0, CharacterDriveListService.DriveTalentList[SelectedCharacterDrive.DriveName].Count)];

        //SelectedAbilityRollType = AbilityRollType.AllRandom;
        CharacterAbilityBlockBuilder.RollAllRandom();

    }
}
