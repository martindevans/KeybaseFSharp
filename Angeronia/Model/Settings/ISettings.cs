﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Angeronia.Model.Settings
{
    public interface ISettings : INotifyPropertyChanged
    {
        string Accent { get; set; }

        string Theme { get; set; }

        string MostRecentlyLoggedInUser { get; set; }

        bool SaveMostRecentUser { get; set; }

        bool ParanoidLogout { get; set; }

        bool ParanoidLocking { get; set; }

        bool ParanoidVerification { get; set; }

        void Load();
    }
}
