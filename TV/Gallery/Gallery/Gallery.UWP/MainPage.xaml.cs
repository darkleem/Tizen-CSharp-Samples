﻿namespace Gallery.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new Gallery.App());
        }
    }
}
