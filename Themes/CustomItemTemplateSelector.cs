using System.Windows;
using System.Windows.Controls;

namespace TheExpanseRPG.Themes;

public class CustomItemTemplateSelector : DataTemplateSelector
{
    public required DataTemplate ComboItemTemplate { get; set; }

    public required DataTemplate ComboSelectedTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        ContentPresenter? contentPresenter = container as ContentPresenter;

        if (contentPresenter != null && contentPresenter.TemplatedParent is ComboBox)
        {
            ComboBox? comboBox = contentPresenter.TemplatedParent as ComboBox;
            if (comboBox is not null && comboBox.SelectedItem == item)
            {
                return ComboSelectedTemplate;
            }            
        }
        return ComboItemTemplate;
    }
}
