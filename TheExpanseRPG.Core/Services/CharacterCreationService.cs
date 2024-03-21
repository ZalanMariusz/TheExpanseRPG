using System.Text.Json;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Model.Talents;

namespace TheExpanseRPG.Core.Services;

public class CharacterCreationService : ICharacterCreationService
{
    public ICharacterAbilityBlockBuilder AbilityBlockBuilder { get; }
    public ICharacterSocialAndBackgroundBuilder SocialAndBackgroundBuilder { get; }
    public ICharacterProfessionBuilder ProfessionBuilder { get; }
    public ICharacterDriveBuilder DriveBuilder { get; }
    public ICharacterOriginBuilder OriginBuilder { get; }
    public List<CharacterTalent> TalentBonuses { get; set; } = new();
    public List<AbilityFocus> FocusBonuses { get; set; } = new();
    public List<Income> IncomeBonuses { get; set; } = new();
    public Dictionary<string, ICharacterCreationBonus> AllBonuses { get; set; } = new();

    public string CharacterName { get; set; } = string.Empty;
    public string CharacterDescription { get; set; } = string.Empty;
    public string CharacterAvatar { get; set; } = string.Empty;
    private CharacterCreationFocusConflictChecker ConflictChecker { get; set; }



    public CharacterCreationService(
        ICharacterAbilityBlockBuilder characterAbilityBlockBuilder,
        ICharacterSocialAndBackgroundBuilder characterSocialAndBackgroundBuilder,
        ICharacterProfessionBuilder characterProfessionBuilder,
        ICharacterDriveBuilder characterDriveBuilder,
        ICharacterOriginBuilder characterOriginBuilder
        )
    {
        OriginBuilder = characterOriginBuilder;
        SocialAndBackgroundBuilder = characterSocialAndBackgroundBuilder;
        ProfessionBuilder = characterProfessionBuilder;
        DriveBuilder = characterDriveBuilder;
        AbilityBlockBuilder = characterAbilityBlockBuilder;

        SocialAndBackgroundBuilder.SocialClassChanged += (sender, selectedSocialClass) => ProfessionBuilder.ClearSelectedProfession(selectedSocialClass);
        OriginBuilder.OriginChanged += SyncBonusDictionary;
        SocialAndBackgroundBuilder.BonusSelectionChanged += SyncBonusDictionary;
        ProfessionBuilder.BonusSelectionChanged += SyncBonusDictionary;
        DriveBuilder.BonusSelectionChanged += SyncBonusDictionary;
        ConflictChecker = CharacterCreationFocusConflictChecker.Instance;
        ConflictChecker.AllBonuses = AllBonuses;

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
        RefreshIncomeBonuses();
    }
    private void RefreshIncomeBonuses()
    {
        IncomeBonuses.Clear();
        foreach (var bonus in AllBonuses.Values)
        {
            if (bonus is Income incomeBonus)
            {
                IncomeBonuses.Add(incomeBonus);
            }
        }
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
    #region Creation procs
    public bool CanCreateCharacter()
    {
        List<bool> validationProperties = new()
        {
            SocialAndBackgroundBuilder.IsMissingBackgroundBonus(),
            ProfessionBuilder.IsMissingProfessionBonus(),
            DriveBuilder.IsMissingDriveBonus(),
            AbilityBlockBuilder.IsMissingAbilityRoll(),

            ConflictChecker.HasConflitcts(),

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
        if (CanCreateCharacter())
        {
            ExpanseCharacter character = ExpanseCharacterBuilder
                .StartCreateCharacter()
                .WithOrigin(OriginBuilder.SelectedCharacterOrigin)
                .AndSocialClass(SocialAndBackgroundBuilder.SelectedCharacterSocialClass)
                .AndBackground(SocialAndBackgroundBuilder.SelectedCharacterBackground!.BackgroundName)
                .AndProfession(ProfessionBuilder.SelectedCharacterProfession!.ProfessionName)
                .AndDrive(DriveBuilder.SelectedCharacterDrive!.DriveName)
                .WithDriveBonus(DriveBuilder.SelectedDriveBonus)
                .AddAbilityBlock(AbilityBlockBuilder.CharacterAbilityBlock)
                .WithAbilityBonuses(AbilityBlockBuilder.AbilityBonuses)
                .AddFocuses(FocusBonuses)
                .AndTalents(TalentBonuses)
                .SetCharacterName(CharacterName)
                .AndDescription(CharacterDescription)
                .AndAvatar(CharacterAvatar)
                .SetIncome((int)GetTotalIncome()!);

            string characterJson = JsonSerializer.Serialize(character, new JsonSerializerOptions { WriteIndented = true });
            string fullPath = Path.Combine(ModelResources.CharacterSavePath, CharacterName);
            Directory.CreateDirectory(ModelResources.CharacterSavePath);

            File.WriteAllText($"{fullPath}.json", characterJson);
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
        } while (ConflictChecker.HasConflitcts());
    }
    #endregion
    public int? GetTotalIncome()
    {
        List<int?> totalIncome = new()
        {
            ProfessionBuilder.SelectedCharacterProfession?.IncomeBase,
            SocialAndBackgroundBuilder.SelectedCharacterSocialClass - ProfessionBuilder.SelectedCharacterProfession?.ProfessionSocialClass,
            IncomeBonuses.Sum(x => x.Value),
            (int?)TalentBonuses.FirstOrDefault(x => x is Affluent)?.Degree + 2
        };

        return totalIncome.Sum();
    }
}
