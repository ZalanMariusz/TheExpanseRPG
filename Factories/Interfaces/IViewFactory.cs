using System.Windows;

namespace TheExpanseRPG.Core.Factories.Interfaces
{
    public interface IViewFactory
    {
        public Window GetWindow<TWindow>() where TWindow : Window;
    }
}
