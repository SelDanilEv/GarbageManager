using GarbageManager.Services;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using Microsoft.Win32;
using System.Windows;
using GarbageManager.Model;
using System.Windows.Forms;

namespace GarbageManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ISettingsService _settingsService;
        public MainWindow()
        {
            InitializeComponent();

            _settingsService = new SettingsService();

            GMAppContext.StartAppSettings = _settingsService.GetStartAppSettings();
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            tbfilePath.Text = GMAppContext.StartAppSettings?.PathToGarbageFolder;
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                {
                    var filePath = dialog.SelectedPath;

                    var newStartAppSettings = GMAppContext.StartAppSettings ?? new StartAppSettings();
                    newStartAppSettings.PathToGarbageFolder = filePath;

                    GMAppContext.StartAppSettings = newStartAppSettings;
                    _settingsService.UpdateSettings(newStartAppSettings);
                    tbfilePath.Text = filePath;
                }
            }

        }
    }
}
