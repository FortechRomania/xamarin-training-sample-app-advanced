using System;
using Autofac;
using GameOfThrones.Services;
using GameOfThrones.ViewModels;

namespace GameOfThrones
{
    public class DependencyLocator
    {
        public static readonly DependencyLocator Current = new DependencyLocator();

        private IContainer _container;

        public BooksViewModel CreateBooksViewModel() => _container.Resolve<BooksViewModel>();

        public CharactersViewModel CreateCharactersViewModel() => _container.Resolve<CharactersViewModel>();

        public HousesViewModel CreateHousesViewModel() => _container.Resolve<HousesViewModel>();

        public void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            Type[] types =
            {
                typeof(BooksService),
                typeof(CharactersService),
                typeof(HousesService),
            };

            builder.RegisterTypes(types).AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<BooksViewModel>();
            builder.RegisterType<CharactersViewModel>();
            builder.RegisterType<HousesViewModel>();

            _container = builder.Build();
        }
    }
}