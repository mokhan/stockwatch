﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using gorilla.utility;
using solidware.financials.windows.ui.handlers;
using solidware.financials.windows.ui.views.icons;

namespace solidware.financials.windows.ui.views
{
    public partial class Shell : RegionManager
    {
        public Shell()
        {
            InitializeComponent();
            regions = new Dictionary<Type, UIElement>
                      {
                          {GetType(), this},
                          {typeof (Window), this},
                          {StatusBar.GetType(), StatusBar},
                          {Menu.GetType(), Menu},
                          {DockManager.GetType(), DockManager},
                          {Tabs.GetType(), Tabs},
                          {ButtonBar.GetType(), ButtonBar},
                          {TaskBarIcon.GetType(), TaskBarIcon},
                      };
            DockManager.Loaded += (o, e) =>
            {
                if (File.Exists(settings_file)) DockManager.RestoreLayout(settings_file);
            };
            Closing += (o, e) => DockManager.SaveLayout(settings_file);
            Closing += (o, e) => TaskBarIcon.Dispose();
            Loaded += (o, e) =>
            {
                TaskBarIcon.Icon = UIIcon.Application.AsIcon();
                TaskBarIcon.ShowCustomBalloon(new FancyBalloon { DataContext = new BalloonMessage { BalloonText = "Welcome"} }, PopupAnimation.Slide, 4000);
            };
        }

        public void region<Region>(Action<Region> configure) where Region : UIElement
        {
            ensure_that_the_region_exists<Region>();
            configure(regions[typeof (Region)].downcast_to<Region>());
        }

        void ensure_that_the_region_exists<Region>()
        {
            if (!regions.ContainsKey(typeof (Region)))
                throw new Exception("Could not find region: {0}".format(typeof (Region)));
        }

        readonly IDictionary<Type, UIElement> regions;
        string settings_file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"mokhan.ca\momoney\default.ui.settings");
    }
}