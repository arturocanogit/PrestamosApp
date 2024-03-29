﻿using PrestamosApp.Services;
using PrestamosApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrestamosApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
