﻿using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullLoadingModal : PopupPage
    {
        public FullLoadingModal()
        {
            InitializeComponent();
        }
    }
}