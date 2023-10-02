using System.Windows;
using System;
using TheExpanseRPG.Core.Factories.Interfaces;

namespace TheExpanseRPG.Core.Factories
{
    class ViewFactory : IViewFactory
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
