using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TheExpanseRPG.MVVM.ViewModel;

namespace TheExpanseRPG.MVVM;

public class EventAggregator_ToDelete
{
    private Dictionary<Tuple<Type, Type>, List<Tuple<string, string>>> PropertiesToLink { get; set; } = new();
    private Dictionary<Tuple<Type, Type>, List<Tuple<string, string>>> AlreadySubscribed { get; set; } = new();
    private  HashSet<PropertyNotifierObject> Notifiers { get; set; } = new();
    public void InitPropertiesToLink()
    {
        /*subscriber.propertyname - notifier.propertyname*/
        PropertiesToLink = new()
        {
            {
                new(typeof(CharacterCreationViewModel),typeof(DrivesViewModel)),
                new()
                {
                     new(nameof(CharacterCreationViewModel.SelectedDrive),nameof(DrivesViewModel.SelectedCharacterDrive))
                }},
            {
                new(typeof(CharacterCreationViewModel),typeof(OriginSelectViewModel)),
                new()
                {
                    /*main*/
                    new(nameof(CharacterCreationViewModel.SelectedOrigin),nameof(OriginSelectViewModel.SelectedOrigin)),
                    /*SelectedOrigin conflicts*/
                    new(nameof(CharacterCreationViewModel.HasOriginSelectionConflict),nameof(OriginSelectViewModel.SelectedOrigin)),
                    new(nameof(CharacterCreationViewModel.HasSocialOrBackgroundSelectionConflict),nameof(OriginSelectViewModel.SelectedOrigin)),
                    new(nameof(CharacterCreationViewModel.HasProfessionSelectionConflict),nameof(OriginSelectViewModel.SelectedOrigin)),
                    new(nameof(CharacterCreationViewModel.OriginConflicts),nameof(OriginSelectViewModel.SelectedOrigin)),
                    new(nameof(CharacterCreationViewModel.SocialOrBackgroundConflicts),nameof(OriginSelectViewModel.SelectedOrigin)),
                    new(nameof(CharacterCreationViewModel.ProfessionConflicts),nameof(OriginSelectViewModel.SelectedOrigin))
                }},
            {
                new(typeof(CharacterCreationViewModel),typeof(CharacterProfessionViewModel)),
                new()
                {
                    /*main*/
                    new(nameof(CharacterCreationViewModel.SelectedProfession),nameof(CharacterProfessionViewModel.SelectedProfession)),
                    /*SelectedFocus* conflicts*/
                    new(nameof(CharacterCreationViewModel.HasSocialOrBackgroundSelectionConflict),nameof(CharacterProfessionViewModel.SelectedFocus)),
                    new(nameof(CharacterCreationViewModel.HasProfessionSelectionConflict),nameof(CharacterProfessionViewModel.SelectedFocus)),
                    new(nameof(CharacterCreationViewModel.HasOriginSelectionConflict),nameof(CharacterProfessionViewModel.SelectedFocus)),
                    new(nameof(CharacterCreationViewModel.OriginConflicts),nameof(CharacterProfessionViewModel.SelectedFocus)),
                    new(nameof(CharacterCreationViewModel.SocialOrBackgroundConflicts),nameof(CharacterProfessionViewModel.SelectedFocus)),
                    new(nameof(CharacterCreationViewModel.ProfessionConflicts),nameof(CharacterProfessionViewModel.SelectedFocus))
                }},
            {
                new(typeof(CharacterCreationViewModel),typeof(SocialAndBackgroundViewModel)),
                new()
                {
                    /*main*/
                    new(nameof(CharacterCreationViewModel.SelectedBackground),nameof(SocialAndBackgroundViewModel.SelectedBackground)),
                    new(nameof(CharacterCreationViewModel.SelectedSocialClass),nameof(SocialAndBackgroundViewModel.SelectedCharacterSocialClass)),
                    /*SelectedBenefit conflicts*/
                    new(nameof(CharacterCreationViewModel.HasOriginSelectionConflict),nameof(SocialAndBackgroundViewModel.SelectedBenefit)),
                    new(nameof(CharacterCreationViewModel.HasSocialOrBackgroundSelectionConflict),nameof(SocialAndBackgroundViewModel.SelectedBenefit)),
                    new(nameof(CharacterCreationViewModel.HasProfessionSelectionConflict),nameof(SocialAndBackgroundViewModel.SelectedBenefit)),
                    new(nameof(CharacterCreationViewModel.OriginConflicts),nameof(SocialAndBackgroundViewModel.SelectedBenefit)),
                    new(nameof(CharacterCreationViewModel.SocialOrBackgroundConflicts),nameof(SocialAndBackgroundViewModel.SelectedBenefit)),
                    new(nameof(CharacterCreationViewModel.ProfessionConflicts),nameof(SocialAndBackgroundViewModel.SelectedBenefit)),
                    /*SelectedBackgroundFocus conflicts*/
                    new(nameof(CharacterCreationViewModel.HasOriginSelectionConflict),nameof(SocialAndBackgroundViewModel.SelectedBackgroundFocus)),
                    new(nameof(CharacterCreationViewModel.HasSocialOrBackgroundSelectionConflict),nameof(SocialAndBackgroundViewModel.SelectedBackgroundFocus)),
                    new(nameof(CharacterCreationViewModel.HasProfessionSelectionConflict),nameof(SocialAndBackgroundViewModel.SelectedBackgroundFocus)),
                    new(nameof(CharacterCreationViewModel.OriginConflicts),nameof(SocialAndBackgroundViewModel.SelectedBackgroundFocus)),
                    new(nameof(CharacterCreationViewModel.SocialOrBackgroundConflicts),nameof(SocialAndBackgroundViewModel.SelectedBackgroundFocus)),
                    new(nameof(CharacterCreationViewModel.ProfessionConflicts),nameof(SocialAndBackgroundViewModel.SelectedBackgroundFocus)),
                }},
            {
                new(typeof(CharacterProfessionViewModel),typeof(CharacterCreationViewModel)),
                new()
                {
                     new(nameof(CharacterProfessionViewModel.SelectedProfession),nameof(CharacterCreationViewModel.SelectedSocialClass))
                }},
        };
    }

    public void SubscribeToKnownNotifier(PropertyNotifierObject notifier, PropertyNotifierObject subscriber)
    {
        RegisterNotifier(notifier);
        List<Tuple<string, string>> propertyPairs = PropertiesToLink[new(subscriber.GetType(), notifier.GetType())];
        if (!AlreadySubscribed.TryGetValue(new(subscriber.GetType(), notifier.GetType()), out List<Tuple<string, string>> props))
        {
            foreach (Tuple<string, string> propertyPair in propertyPairs)
            {
                notifier.PropertyChanged += (sender, eventArgs) => NotifyLinkedProperty(subscriber, propertyPair.Item1, eventArgs, propertyPair.Item2);
            }
            AlreadySubscribed.Add(new(subscriber.GetType(), notifier.GetType()), propertyPairs);
        }
    }

    public void Subscribe(PropertyNotifierObject subscriber)
    {
        var dictionaryEntriesForSubscriber = PropertiesToLink.Where(x => subscriber.GetType() == x.Key.Item1);
        foreach (KeyValuePair<Tuple<Type, Type>, List<Tuple<string, string>>> keys in dictionaryEntriesForSubscriber)
        {
            PropertyNotifierObject? notifier = Notifiers.FirstOrDefault(x => x.GetType() == keys.Key.Item2);
            if (notifier is not null)
            {
                SubscribeToKnownNotifier(notifier, subscriber);
            }
        }
    }

    public void RegisterNotifier(PropertyNotifierObject notifier)
    {
        Notifiers.Add(notifier);
    }

    private void NotifyLinkedProperty(PropertyNotifierObject subscriber, string subscriberPropertyName, PropertyChangedEventArgs eventArgs, string notifierProperty)
    {
        if (eventArgs.PropertyName == notifierProperty)
        {
            subscriber.OnPropertyChanged(subscriberPropertyName);
        }
    }

    public void UnSubscribeAll()
    {
        foreach (PropertyNotifierObject notifier in Notifiers)
        {
            notifier.UnsubscribeAll();
        }
        Notifiers.Clear();
        AlreadySubscribed.Clear();
    }
}
