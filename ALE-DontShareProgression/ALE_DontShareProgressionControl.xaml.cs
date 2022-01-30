using System.Windows;
using System.Windows.Controls;

namespace ALE_DontShareProgression {
    public partial class ALE_DontShareProgressionControl : UserControl {

        private ALE_DontShareProgressionPlugin Plugin { get; }

        private ALE_DontShareProgressionControl() {
            InitializeComponent();
        }

        public ALE_DontShareProgressionControl(ALE_DontShareProgressionPlugin plugin) : this() {
            Plugin = plugin;
            DataContext = plugin.Config;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e) {
            Plugin.Save();
        }
    }
}
