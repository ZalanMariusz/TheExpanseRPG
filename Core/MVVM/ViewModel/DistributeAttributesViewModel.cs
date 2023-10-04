using System;
using System.Reflection;
using TheExpanseRPG.Core.Commands;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class DistributeAttributesViewModel : ViewModelBase
    {
        private const int MAXATTRIBUTEVALUE = 3;
        private const int MINATTRIBUTEVALUE = 0;
        private const int ATTRIBUTEPOOL = 12;
        public int Accuracy { get { return _accuracy; } set { _accuracy = value; OnPropertyChanged(); } }
        public int Constitution { get { return _constitution; } set { _constitution = value; OnPropertyChanged(); } }
        public int Fighting { get { return _fighting; } set { _fighting = value; OnPropertyChanged(); } }
        public int Communication { get { return _communication; } set { _communication = value; OnPropertyChanged(); } }
        public int Dexterity { get { return _dexterity; } set { _dexterity = value; OnPropertyChanged(); } }
        public int Intelligence { get { return _intelligence; } set { _intelligence = value; OnPropertyChanged(); } }
        public int Perception { get { return _perception; } set { _perception = value; OnPropertyChanged(); } }
        public int Strength { get { return _strength; } set { _strength = value; OnPropertyChanged(); } }
        public int Willpower { get { return _willpower; } set { _willpower = value; OnPropertyChanged(); } }
        private int _accuracy;
        private int _constitution;
        private int _fighting;
        private int _communication;
        private int _dexterity;
        private int _intelligence;
        private int _perception;
        private int _strength;
        private int _willpower;
        private int _attributePool;

        public RelayCommand IncreaseAttributeValue { get; set; }
        public RelayCommand DecreaseAttributeValue { get; set; }
        public int AttributePool { get { return _attributePool; } set { _attributePool = value; OnPropertyChanged(); } }
        public DistributeAttributesViewModel()
        {
            AttributePool = ATTRIBUTEPOOL;
            IncreaseAttributeValue = new RelayCommand(CanIncrease, Increase);
            DecreaseAttributeValue = new RelayCommand(CanDecrease, Decrease);
        }
        private void Increase(object attributeName)
        {
            string attributeNameString = attributeName.ToString()!;
            PropertyInfo property = GetType().GetProperty(attributeNameString)!;
            int propertyValue = GetAttributePropertyValue(attributeNameString);
            property!.SetValue(this, propertyValue + 1);
            AttributePool--;
        }
        private void Decrease(object attributeName)
        {
            string attributeNameString = attributeName.ToString()!;
            PropertyInfo property = GetType().GetProperty(attributeNameString)!;
            int propertyValue = GetAttributePropertyValue(attributeNameString);
            property!.SetValue(this, propertyValue - 1);
            AttributePool++;
        }
        private bool CanIncrease(object attributeName)
        {
            int propertyValue = GetAttributePropertyValue(attributeName.ToString()!);
            return AttributePool > 0 && propertyValue < MAXATTRIBUTEVALUE;
        }

        private bool CanDecrease(object attributeName)
        {
            int propertyValue = GetAttributePropertyValue(attributeName.ToString()!);
            return AttributePool < ATTRIBUTEPOOL && propertyValue > MINATTRIBUTEVALUE;
        }

        private int GetAttributePropertyValue(string attributeName)
        {
            PropertyInfo property = GetType().GetProperty(attributeName.ToString()!)!;
            int propertyValue = Convert.ToInt32(property.GetValue(this));
            return propertyValue;
        }
    }
}
