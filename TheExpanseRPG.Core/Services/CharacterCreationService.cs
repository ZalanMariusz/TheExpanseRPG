using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services;

public class CharacterCreationService : IExpanseService
{
    private const int MAXABILITYVALUE = 3;
    private const int MINABILITYVALUE = 0;
    private const int ABILITYPOOL = 12;
    
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
    public CharacterAbilityBlock CharacterAbilityBlock { get; set; } = new();
    public List<int?> AbilityValuesToAssign { get; set; } = new();
    public int PointsToDistribute { get; private set; } = ABILITYPOOL;
    public int? Speed { get => 10 + (GetDexterityTotal() is null ? 0 : (int)GetDexterityTotal()!); }
    public int Defense { get => 10 + (GetDexterityTotal() is null ? 0 : (int)GetDexterityTotal()!); }
    public int Toughness { get => GetConstitutionTotal() is null ? 0 : (int)GetConstitutionTotal()!; }
    public int Fortune { get => 15 + (SelectedDriveBonus?.GetType() == typeof(Fortune) ? 5 : 0); }
    #endregion
    #region Origin
    private Dictionary<CharacterOrigin, string?> _originDescriptions = new();
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
        }
    }
    public string? CurrentOriginDescription
    {
        get { return SelectedCharacterOrigin is null ? null : _originDescriptions[(CharacterOrigin)SelectedCharacterOrigin]; }
    }
    #endregion
    #region Social class
    private Dictionary<CharacterSocialClass, string?> _socialClassDescriptions = new();
    public string? SelectedCharacterSocialClassDescription
    {
        get { return SelectedCharacterSocialClass is null ? null : _socialClassDescriptions[(CharacterSocialClass)SelectedCharacterSocialClass]; }
    }
    private CharacterSocialClass? _selectedCharacterSocialClass;
    public CharacterSocialClass? SelectedCharacterSocialClass
    {
        get { return _selectedCharacterSocialClass; }
        set { _selectedCharacterSocialClass = value; ClearSelectedProfession(); }
    }

    #endregion
    #region Background and Background bonuses
    private CharacterBackGround? _selectedCharacterBackground;
    public CharacterBackGround? SelectedCharacterBackground
    {
        get { return _selectedCharacterBackground; }
        set
        {
            _selectedCharacterBackground = value;
            SyncBonusDictionary();
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
        }
    }
    #endregion
    #region Drive and Drive bonuses
    public CharacterDrive? SelectedCharacterDrive { get; set; }
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

    public CharacterProfession? SelectedCharacterProfession
    {
        get { return _selectedCharacterProfession; }
        set { ClearProfessionBonuses(value); _selectedCharacterProfession = value; }
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
    public IDiceRollService DiceRollService { get; }
    public ICharacterProfessionListService ProfessionListService { get; }
    public ICharacterBackgroundListService BackgroundListService { get; }
    public ICharacterDriveListService CharacterDriveListService { get; }
    public AbilityRollType SelectedAbilityRollType { get; set; }
    public List<CharacterTalent> TalentBonuses { get; set; } = new();
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
        ITalentListService talentListService,
        IAbilityFocusListService focusListService,
        ICharacterBackgroundListService backgroundListService,
        IDiceRollService diceRollService,
        ICharacterProfessionListService professionListService,
        ICharacterDriveListService characterTalentListService
        )
    {
        TalentListService = talentListService;
        FocusListService = focusListService;
        DiceRollService = diceRollService;
        ProfessionListService = professionListService;
        BackgroundListService = backgroundListService;
        CharacterDriveListService = characterTalentListService;
        SelectedAbilityRollType = AbilityRollType.AllRandom;
        InitializeOriginDescriptions();
        InitiliazeSocialClassDescriptions();
        InitializeProfessionLists();
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
            bonusValue = FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Free-fall");
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
    public void ResetAbilities()
    {
        int? valueToResetTo = SelectedAbilityRollType == AbilityRollType.DistributePoints ? 0 : null;
        CharacterAbilityBlock.AbilityList.ForEach(x => x.BaseValue = valueToResetTo);
        PointsToDistribute = ABILITYPOOL;
    }
    public void RollAllRandom()
    {
        foreach (CharacterAbility ability in CharacterAbilityBlock.AbilityList)
        {
            int rollResult = DiceRollService.Roll3D6().GetRollResultSumValue();
            ability.BaseValue = GetAttributeValueFromRoll(rollResult);
        }
    }
    public void RollAssignableAbilityList()
    {
        AbilityValuesToAssign.Clear();
        for (int i = 0; i < CharacterAbilityBlock.AbilityList.Count; i++)
        {
            AbilityValuesToAssign.Add(GetAttributeValueFromRoll(DiceRollService.Roll3D6().GetRollResultSumValue()));
        }
    }
    public void AssignAbilityScore(string abilityName, int? newScore)
    {
        int? abilityValue = CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
        if (abilityValue.HasValue)
        {
            AbilityValuesToAssign.Add(abilityValue);
        }
        AbilityValuesToAssign.Remove(newScore);
        CharacterAbilityBlock.GetAbility(abilityName).BaseValue = newScore;
    }
    public void DecreaseAttributeFromPool(string abilityName)
    {
        if (CanDecrease(abilityName))
        {
            CharacterAbilityBlock.GetAbility(abilityName).BaseValue--;
            PointsToDistribute++;
        }
    }
    public void IncreaseAttributeFromPool(string abilityName)
    {
        if (CanIncrease(abilityName))
        {
            CharacterAbilityBlock.GetAbility(abilityName).BaseValue++;
            PointsToDistribute--;
        }
    }
    public bool CanIncrease(string abilityName)
    {
        int? propertyValue = CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
        return PointsToDistribute > 0 && propertyValue < MAXABILITYVALUE && propertyValue != null;
    }
    public bool CanDecrease(string abilityName)
    {
        int? propertyValue = CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
        return PointsToDistribute < ABILITYPOOL && propertyValue > MINABILITYVALUE && propertyValue != null;
    }
    private static int GetAttributeValueFromRoll(int rollResult)
    {
        return rollResult switch
        {
            3 => -2,
            4 or 5 => -1,
            6 or 7 or 8 => 0,
            9 or 10 or 11 => 1,
            12 or 13 or 14 => 2,
            15 or 16 or 17 => 3,
            _ => 4,
        };
    }
    public int? GetAbilityTotal(CharacterAbilityName abilityName)
    {
        List<int?> abilityValues = new()
        {
            CharacterAbilityBlock.GetAbility(abilityName).BaseValue,
            GetAbilityBonuses(abilityName)
        };
        return abilityValues.Any() ? abilityValues.Sum() : null;
    }
    public int? GetAbilityBonuses(CharacterAbilityName abilityName)
    {
        IEnumerable<CharacterAbility> abilityBonuses = Bonuses.Values.Where(x => x.GetType() == typeof(CharacterAbility) && ((CharacterAbility)x).AbilityName == abilityName).Cast<CharacterAbility>();
        return abilityBonuses.Any() ? abilityBonuses.Count() : null;
    }
    public int? GetAccuracyTotal()
    {
        return GetAbilityTotal(CharacterAbilityName.Accuracy);
    }
    public int? GetCommunicationTotal()
    {
        return GetAbilityTotal(CharacterAbilityName.Communication);
    }
    public int? GetConstitutionTotal()
    {
        return GetAbilityTotal(CharacterAbilityName.Constitution);
    }
    public int? GetDexterityTotal()
    {
        return GetAbilityTotal(CharacterAbilityName.Dexterity);
    }
    public int? GetFightingTotal()
    {
        return GetAbilityTotal(CharacterAbilityName.Fighting);
    }
    public int? GetIntelligenceTotal()
    {
        return GetAbilityTotal(CharacterAbilityName.Intelligence);
    }
    public int? GetPerceptionTotal()
    {
        return GetAbilityTotal(CharacterAbilityName.Perception);
    }
    public int? GetStrengthTotal()
    {
        return GetAbilityTotal(CharacterAbilityName.Strength);
    }
    public int? GetWillpowerTotal()
    {
        return GetAbilityTotal(CharacterAbilityName.Willpower);
    }
    public List<AbilityFocus> GetAbilityFocuses(CharacterAbilityName abilityName)
    {
        return Bonuses.Values.Where(x => x is AbilityFocus focus && focus.AbilityName == abilityName).Cast<AbilityFocus>().Distinct().ToList();
    }
    #endregion
    #region Conflict checkers
    public List<string> GetBackgroundFocusConflicts()
    {
        if (Bonuses.TryGetValue("BackgroundFocus", out ICharacterCreationBonus? backgroundFocus))
        {
            return Bonuses.Where(x => x.Value.CreationBonusName == backgroundFocus.CreationBonusName && x.Key != "BackgroundFocus").ToDictionary(x => x.Key, x => x.Key).Keys.ToList();
        }
        return new();

    }
    public List<string> GetBackgroundBenefitConflicts()
    {
        if (Bonuses.TryGetValue("BackgroundBenefit", out ICharacterCreationBonus? backgroundBenefit))
        {
            if (backgroundBenefit is AbilityFocus)
            {
                return Bonuses.Where(x => x.Value.CreationBonusName == backgroundBenefit.CreationBonusName && x.Key != "BackgroundBenefit").ToDictionary(x => x.Key, x => x.Key).Keys.ToList();
            }
            return new();
        }
        return new();
    }
    public List<string> GetOriginFocusConflicts()
    {
        if (Bonuses.TryGetValue("Origin", out ICharacterCreationBonus? originFocus))
        {
            return Bonuses.Where(x => x.Value.CreationBonusName == originFocus.CreationBonusName && x.Key != "Origin").ToDictionary(x => x.Key, x => x.Key).Keys.ToList();
        }
        return new();

    }

    public List<string> GetProfessionFocusConflicts()
    {
        if (Bonuses.TryGetValue("ProfessionFocus", out ICharacterCreationBonus? professionFocus))
        {
            return Bonuses.Where(x => x.Value.CreationBonusName == professionFocus.CreationBonusName && x.Key != "ProfessionFocus").ToDictionary(x => x.Key, x => x.Key).Keys.ToList();
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
            { CharacterOrigin.Earth,
                "With a population of some 30 billion, many Earthers are unemployed and live on government-provided Basic Assistance (generally known as just “Basic”) which provides for their essential food, housing, and medical needs, but little else. You are likely one of the few to leave Earth to find a new life elsewhere. " +
                "\n\n• Your native gravity is normal gravity—“Earth-normal” or 1 g. Earthers can and do learn to operate in lower gravity, but lack the instincts of people raised in it. " +
                "\n\n• Earthers have greater muscle and bone density from being raised in a gravity well, making them shorter and more broadly built than Belters or even native-born Martians, but Earthers in space need regular exercise and medical treatments to avoid muscle atrophy and bone density loss." },
            { CharacterOrigin.Mars,
                "Born in the Martian Congressional Republic, your life has been influenced by the Martian dream: to terraform the Red Planet into a lush and life-sustaining garden. Like the generations before you, you know that you will likely never see the completion of this work in your lifetime." +
                "\n\n• Your native gravity is low, the gravity of Mars rather than Earth. Martians are more comfortable with microgravity than Earthers, and better able to tolerate a full 1 g than Belters, operating in-between." },
            { CharacterOrigin.Belt,
                "You were born and raised in the Black, on a station or ship, and have lived most, if not all, of your life out in the Belt or beyond. Separated from death by nothing more than basic support systems your whole life, you have learned to be cautious and aware of your environment. " +
                "\n\n• Your native gravity is microgravity. Belters are most comfortable “on the float” and handle moving in free-fall easily. You automatically have the Dexterity (Free-fall) focus. Conversely, Earth-normal gravity is crushingly heavy for a Belter." +
                "\n\n• You speak Belter Creole, a combination of loan-words and phrases from various languages, combined with gestures useful for communicating while wearing a vac suit and unable to speak." +
                "\n\n• Belters tend to be tall and willowy as a result of being raised in low - or microgravity environments. Regimens of bone - density drugs and genetic treatments are needed to keep Belters healthy, and some Belters have minor physical abnormalities because of this." +
                "\n\n• Belters often have a diverse ethnic heritage, given the “melting pot” of the Belt, with ancestors from many different Earth cultures." }
        };
    }
    private void InitiliazeSocialClassDescriptions()
    {
        _socialClassDescriptions = new()
        {
            { CharacterSocialClass.Outsider,"More of a non-social class, outsiders tend to be outcasts, criminals, or non-conformists who can’t or won’t live according to society’s customs. They often lack access to things other people take for granted and learn to get by on their own, some times forming their own support networks and structures outside of those of mainstream society. Some outsiders reject the mainstream by choice, but in many cases, outsiders are pushed out by society’s biases."},
            { CharacterSocialClass.Lower,"Hard, usually physical, labor and precarious employment tend to rule the lives of lower class characters. Still, that work is often all that separates them from becoming outsiders, so they cling to it. Lower class characters often depend on family and friends to help keep them out of utter poverty. They might live in failing industrial areas, inner city slums, or hardscrabble farms. In all cases, they make do with what is available and find ways to stretch out resources until the next payday or job comes along."},
            { CharacterSocialClass.Middle,"A measure of comfort and security comes with the middle class. A steady job, often skilled labor or “white collar,” supplies the means to afford a few luxuries or non-essentials. Middle class characters might start off as a bit insular. They often separate themselves from the struggles of the lower social classes, focusing on the climb towards upper class status. Sometimes that climb leads to a slip. They tumble down to the lower class or even become outsiders. Some settle for stability instead, and prefer not to rock the boat."},
            { CharacterSocialClass.Upper,"Upper class characters sit at a society’s summit where they rarely need to worry about resources, except, of course, when they want more. Their concerns are often focused on the responsibilities and privileges associated with their status. Some are born into upper class privilege, inheriting wealth and opportunity, while others worked their way up to join the elite. In some societies, it’s almost impossible to work your way to upper class status, and even if you do, you might get less respect compared to hereditary “old money” peers."},
        };
    }
    private void InitializeProfessionLists()
    {
        OutsiderProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Outsider));
        LowerclassProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Lower));
        MiddleclassProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Middle));
        UpperclassProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Upper));
    }


    #endregion
}
