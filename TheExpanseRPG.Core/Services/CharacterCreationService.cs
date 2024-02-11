using System.Text.Json;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Model.Talents;

namespace TheExpanseRPG.Core.Services;

public class CharacterCreationService : ICharacterCreationService
{
    public ICharacterAbilityBlockBuilder AbilityBlockBuilder { get; set; }
    public ICharacterSocialAndBackgroundBuilder SocialAndBackgroundBuilder { get; set; }
    public ICharacterProfessionBuilder ProfessionBuilder { get; set; }
    public ICharacterDriveBuilder DriveBuilder { get; set; }
    public ICharacterOriginBuilder OriginBuilder { get; set; }
    private IExpanseCharacterBuilder CharacterBuilder { get; set; }
    public List<CharacterTalent> TalentBonuses { get; set; } = new();
    public List<AbilityFocus> FocusBonuses { get; set; } = new();
    public Dictionary<string, ICharacterCreationBonus> AllBonuses { get; set; } = new();

    public string CharacterName { get; set; } = string.Empty;
    public string CharacterDescription { get; set; } = string.Empty;
    public string CharacterAvatar { get; set; } = string.Empty;
    public int Speed => 10 + (AbilityBlockBuilder.GetDexterityTotal() is null ? 0 : (int)AbilityBlockBuilder.GetDexterityTotal()!);
    public int Defense => 10 + (AbilityBlockBuilder.GetDexterityTotal() is null ? 0 : (int)AbilityBlockBuilder.GetDexterityTotal()!);
    public int Toughness => AbilityBlockBuilder.GetConstitutionTotal() is null ? 0 : (int)AbilityBlockBuilder.GetConstitutionTotal()!;
    public int Fortune => 15 + (DriveBuilder.SelectedDriveBonus?.GetType() == typeof(Fortune) ? 5 : 0);
    public int? GetTotalIncome()
    {
        List<int?> totalIncome = new()
        {
            ProfessionBuilder.SelectedCharacterProfession?.IncomeBase,
            SocialAndBackgroundBuilder.SelectedCharacterSocialClass - ProfessionBuilder.SelectedCharacterProfession?.ProfessionSocialClass,
            AllBonuses.Values.Where(x => x is Income).Cast<Income>().Sum(x => x.Value),
            (int?)TalentBonuses.FirstOrDefault(x => x is Affluent)?.Degree + 2
        };

        return totalIncome.Sum();
    }

    public CharacterCreationService(
        ICharacterAbilityBlockBuilder characterAbilityBlockBuilder,
        ICharacterSocialAndBackgroundBuilder characterSocialAndBackgroundBuilder,
        ICharacterProfessionBuilder characterProfessionBuilder,
        ICharacterDriveBuilder characterDriveBuilder,
        ICharacterOriginBuilder characterOriginBuilder,
        IExpanseCharacterBuilder characterBuilder
        )
    {
        OriginBuilder = characterOriginBuilder;
        SocialAndBackgroundBuilder = characterSocialAndBackgroundBuilder;
        ProfessionBuilder = characterProfessionBuilder;
        DriveBuilder = characterDriveBuilder;
        AbilityBlockBuilder = characterAbilityBlockBuilder;
        CharacterBuilder = characterBuilder;

        SocialAndBackgroundBuilder.SocialClassChanged += (sender, selectedSocialClass) => ProfessionBuilder.ClearSelectedProfession(selectedSocialClass);


        OriginBuilder.OriginChanged += SyncBonusDictionary;

        SocialAndBackgroundBuilder.BackgroundChanged += SyncBonusDictionary;
        SocialAndBackgroundBuilder.SelectedBackgroundFocusChanged += SyncBonusDictionary;
        SocialAndBackgroundBuilder.SelectedBackgroundTalentChanged += SyncBonusDictionary;
        SocialAndBackgroundBuilder.SelectedBackgroundBenefitChanged += SyncBonusDictionary;

        ProfessionBuilder.ProfessionFocusChanged += SyncBonusDictionary;
        ProfessionBuilder.ProfessionTalentChanged += SyncBonusDictionary;

        DriveBuilder.SelectedDriveBonusChanged += SyncBonusDictionary;
        DriveBuilder.SelectedDriveTalentChanged += SyncBonusDictionary;

    }
    #region Bonus sync procs
    private void SyncBonusDictionary(object? sender, string bonusPropertyName)
    {
        AllBonuses.Remove(bonusPropertyName);

        ICharacterCreationBonus? bonusValue = bonusPropertyName switch
        {
            nameof(CharacterOriginBuilder.SelectedCharacterOrigin) => OriginBuilder.GetOriginBonus(),
            nameof(CharacterSocialAndBackgroundBuilder.SelectedCharacterBackground) => SocialAndBackgroundBuilder.GetAbilityBonus(),
            _ => (ICharacterCreationBonus?)sender!.GetType().GetProperty(bonusPropertyName)!.GetValue(sender),
        };

        if (bonusValue is not null)
        {
            AllBonuses.Add(bonusPropertyName, bonusValue);
        }

        RefreshTalentBonuses();
        RefreshAbilityBonuses();
        RefreshFocusBonuses();
    }
    private void RefreshTalentBonuses()
    {
        TalentBonuses.Clear();
        IEnumerable<CharacterTalent> currentTalentBonuses = AllBonuses.Values.Where(x => x is CharacterTalent).Cast<CharacterTalent>();
        foreach (CharacterTalent talent in currentTalentBonuses)
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
    private void RefreshFocusBonuses()
    {
        FocusBonuses.Clear();
        FocusBonuses = AllBonuses.Values.Where(x => x is AbilityFocus).Cast<AbilityFocus>().Distinct().ToList();
    }
    private void RefreshAbilityBonuses()
    {
        AbilityBlockBuilder.AbilityBonuses = AllBonuses.Values.Where(x => x is CharacterAbility).ToList();
    }
    #endregion
    #region Conflict checkers
    private List<string> GetConflictsWith(string bonusKey)
    {
        if (AllBonuses.TryGetValue(bonusKey, out ICharacterCreationBonus? bonus))
        {
            return AllBonuses.Where(x =>
                x.Value?.CreationBonusName == bonus?.CreationBonusName
                    && x.Key != bonusKey
                    && x.Value is AbilityFocus).Select(x => x.Key).ToList();
        }
        return new();
    }
    public List<string> GetBackgroundFocusConflicts()
    {
        return GetConflictsWith(nameof(SocialAndBackgroundBuilder.SelectedBackgroundFocus));
    }
    public List<string> GetBackgroundBenefitConflicts()
    {
        return GetConflictsWith(nameof(SocialAndBackgroundBuilder.SelectedBackgroundBenefit));
    }
    public List<string> GetOriginFocusConflicts()
    {
        return GetConflictsWith(nameof(OriginBuilder.SelectedCharacterOrigin));
    }

    public List<string> GetProfessionFocusConflicts()
    {
        return GetConflictsWith(nameof(ProfessionBuilder.SelectedProfessionFocus));
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
    public bool CanCreateCharacter()
    {
        List<bool> validationProperties = new()
        {
            SocialAndBackgroundBuilder.IsMissingBackgroundBonus(),
            ProfessionBuilder.IsMissingProfessionBonus(),
            DriveBuilder.IsMissingDriveBonus(),
            AbilityBlockBuilder.IsMissingAbilityRoll(),

            HasBackgroundConflict(),
            HasOriginConflict(),
            HasProfessionConflict(),

            OriginBuilder.SelectedCharacterOrigin is null,
            string.IsNullOrEmpty(CharacterName)
        };
        return !validationProperties.Any(x => x == true);
    }

    public bool CharacterExists()
    {
        return File.Exists($"{ModelResources.CharacterSavePath}{CharacterName}.json");
    }

    public void CreateCharacter()
    {
        CharacterBuilder
            .SetCharacterAbilityBlock(AbilityBlockBuilder.CharacterAbilityBlock)
            .SetCharacterFocuses(FocusBonuses)
            .SetCharacterTalents(TalentBonuses)
            .SetCharacterName(CharacterName)
            .SetCharacterDescription(CharacterDescription)
            .SetCharacterOrigin(SocialAndBackgroundBuilder.SelectedCharacterBackground!.BackgroundName)
            .SetCharacterBackground(OriginBuilder.SelectedCharacterOrigin)
            .SetCharacterSocialClass(SocialAndBackgroundBuilder.SelectedCharacterSocialClass)
            .SetCharacterProfession(ProfessionBuilder.SelectedCharacterProfession!.ProfessionName)
            .SetCharacterDrive(DriveBuilder.SelectedCharacterDrive!.DriveName)
            .SetCharacterAvatar(CharacterAvatar)
            .SetCharacterAbilityBonuses(AbilityBlockBuilder.AbilityBonuses)
            .Create();

        if (CanCreateCharacter())
        {
            ExpanseCharacter character = new()
            {
                Abilities = AbilityBlockBuilder.CharacterAbilityBlock,
                Focuses = FocusBonuses,
                Talents = TalentBonuses,
                Name = CharacterName,
                Description = CharacterDescription,
                Background = SocialAndBackgroundBuilder.SelectedCharacterBackground!.BackgroundName,
                Origin = OriginBuilder.SelectedCharacterOrigin,
                SocialClass = SocialAndBackgroundBuilder.SelectedCharacterSocialClass,
                Profession = ProfessionBuilder.SelectedCharacterProfession!.ProfessionName,
                Drive = DriveBuilder.SelectedCharacterDrive!.DriveName,
                Fortune = Fortune,
                Income = GetTotalIncome(),
                Speed = Speed,
                Toughness = Toughness,
                Defense = Defense,
                Avatar = CharacterAvatar

            };
            string characterJson = JsonSerializer.Serialize(character, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(ModelResources.CharacterSavePath);
            File.WriteAllText($"{ModelResources.CharacterSavePath}{CharacterName}.json", characterJson);
        }
    }

    public void RandomizeCharacter()
    {
        OriginBuilder.GenerateRandom();
        DriveBuilder.GenerateRandom();
        AbilityBlockBuilder.RollAllRandom();

        do
        {
            SocialAndBackgroundBuilder.GenerateRandom();
            ProfessionBuilder.GenerateRandom(SocialAndBackgroundBuilder.SelectedCharacterSocialClass);
        } while (HasBackgroundConflict() || HasProfessionConflict());
    }
}