using System;
using System.Windows;
using TheExpanseRPG.Core.Factories.Interfaces;

namespace TheExpanseRPG.Factories
{
    public class ViewFactory : IViewFactory
    {
        public readonly Func<Type, Window> _factory;
        public ViewFactory(Func<Type, Window> _viewFactory)
        {
            _factory = _viewFactory;
        }
        public Window GetWindow<TWindow>() where TWindow : Window
        {
            return _factory.Invoke(typeof(TWindow));
        }
    }
}
