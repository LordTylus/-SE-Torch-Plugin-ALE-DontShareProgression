using NLog;
using System;
using System.IO;
using System.Windows.Controls;
using Torch;
using Torch.API;
using Torch.API.Managers;
using Torch.API.Plugins;
using Torch.API.Session;
using Torch.Session;

namespace ALE_DontShareProgression {
    public class ALE_DontShareProgressionPlugin : TorchPluginBase, IWpfPlugin {

        public static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static ALE_DontShareProgressionPlugin Instance { get; private set; }

        private static readonly string CONFIG_FILE_NAME = "ALE_DontShareProgressionConfig.cfg";

        private ALE_DontShareProgressionControl _control;
        public UserControl GetControl() => _control ?? (_control = new ALE_DontShareProgressionControl(this));

        private Persistent<ALE_DontShareProgressionConfig> _config;
        public ALE_DontShareProgressionConfig Config => _config?.Data;

        public override void Init(ITorchBase torch) {
            
            base.Init(torch);

            Instance = this;

            SetupConfig();
        }

        private void SetupConfig() {

            var configFile = Path.Combine(StoragePath, CONFIG_FILE_NAME);

            try {

                _config = Persistent<ALE_DontShareProgressionConfig>.Load(configFile);

            } catch (Exception e) {
                Log.Warn(e);
            }

            if (_config?.Data == null) {

                Log.Info("Create Default Config, because none was found!");

                _config = new Persistent<ALE_DontShareProgressionConfig>(configFile, new ALE_DontShareProgressionConfig());
                _config.Save();
            }
        }

        public void Save() {
            try {
                _config.Save();
                Log.Info("Configuration Saved.");
            } catch (IOException e) {
                Log.Warn(e, "Configuration failed to save");
            }
        }
    }
}
