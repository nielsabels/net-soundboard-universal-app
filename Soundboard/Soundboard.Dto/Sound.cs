﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace Soundboard.Dto
{
    public class Sound : BindableBase
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { SetProperty(ref _displayName, value); }
        }

        private string _pictureUri;
        public string PictureUri
        {
            get { return _pictureUri; }
            set { SetProperty(ref _pictureUri, value); }
        }

        public string SoundUri { get; set; }

    }
}
