using System;
using System.Collections.Generic;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class AbilityFocusListService : IExpanseService
    {
        public List<AbilityFocus> FocusList { get; } = new List<AbilityFocus>();
        public AbilityFocusListService()
        {
            PopulateFocusList();
        }

        private void PopulateFocusList()
        {
            AddAccuracyFocuses();
            AddCommunicationFocuses();
            AddConstitutionFocuses();
            AddDexterityFocuses();
            AddFightingFocuses();
            AddIntelligenceFocuses();
            AddPerceptionFocuses();
            AddStrengthFocuses();
            AddWillpowerFocuses();
           
        }
        public AbilityFocus GetEmptyFocus() 
        {
            return new AbilityFocus();
        }
        public AbilityFocus GetEmptyAbilityFocus(CharacterAbilityName abilityName)
        {
            return new AbilityFocus(abilityName);
        }
        public AbilityFocus GetFocusByName(CharacterAbilityName abilityName, string focusName)
        {
            AbilityFocus? retval = FocusList.Find(x => x.FocusName == focusName && x.AbilityName == abilityName);
            return retval ?? throw new KeyNotFoundException($"{abilityName}({focusName}) Focus not found");
        }
        public List<AbilityFocus> GetAbilityFocusList(CharacterAbilityName abilityName)
        {
            return FocusList.FindAll(x => x.AbilityName == abilityName);
        }
        private void ParseFocusesFromList(CharacterAbilityName abilityName, string focusNameList)
        {
            foreach (string focusName in focusNameList.Split(','))
            {
                FocusList.Add(new AbilityFocus(abilityName, focusName));
            }
        }
        private void AddAccuracyFocuses()
        {
            string focusNameList = "Bows,Gunnery,Pistols,Rifles,Throwing";
            ParseFocusesFromList(CharacterAbilityName.Accuracy, focusNameList);
        }
        private void AddCommunicationFocuses()
        {
            string focusNameList = "Bargaining,Deception,Disguise,Etiquette,Expression,Gambling,Investigation,Leadership,Performing,Persuasion,Seduction";
            ParseFocusesFromList(CharacterAbilityName.Communication, focusNameList);
        }
        private void AddConstitutionFocuses()
        {
            string focusNameList = "Running,Stamina,Swimming,Tolerance";
            ParseFocusesFromList(CharacterAbilityName.Constitution, focusNameList);
        }
        private void AddDexterityFocuses()
        {
            string focusNameList = "Acrobatics,Crafting,Driving,Free-fall,Initiative,Piloting,Sleight of Hand,Stealth";
            ParseFocusesFromList(CharacterAbilityName.Dexterity, focusNameList);
        }
        private void AddFightingFocuses()
        {
            string focusNameList = "Brawling,Grappling,Heavy Weapons,Light Weapons";
            ParseFocusesFromList(CharacterAbilityName.Fighting, focusNameList);
        }
        private void AddIntelligenceFocuses()
        {
            string focusNameList = "Art,Business,Cryptography,Current Affairs,Demolitions,Engineering,Evaluation,Law,Medicine,Navigation,Research,Science,Security,Tactics,Technology,History";
            ParseFocusesFromList(CharacterAbilityName.Intelligence, focusNameList);
        }
        private void AddPerceptionFocuses()
        {
            string focusNameList = "Empathy,Hearing,Intuition,Searching,Seeing,Smelling,Survival,Tasting,Touching,Tracking";
            ParseFocusesFromList(CharacterAbilityName.Perception, focusNameList);
        }
        private void AddStrengthFocuses()
        {
            string focusNameList = "Climbing,Intimidation,Jumping,Might";
            ParseFocusesFromList(CharacterAbilityName.Strength, focusNameList);
        }
        private void AddWillpowerFocuses()
        {
            string focusNameList = "Courage,Faith,Self-Discipline";
            ParseFocusesFromList(CharacterAbilityName.Willpower, focusNameList);
        }
    }
}
