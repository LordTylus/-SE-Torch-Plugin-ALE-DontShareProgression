using System;
using System.Collections.Generic;
using Torch;

namespace ALE_DontShareProgression {
    public class ALE_DontShareProgressionConfig : ViewModel {

        private bool _enabled = true;

        public bool Enabled { get => _enabled; set => SetValue(ref _enabled, value); }
    }
}
