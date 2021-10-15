using GarbageManager.Services;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System.Windows;
using GarbageManager.Model;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace GarbageManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ISettingsService _settingsService;
        private IFileSystemManager _fileSystemManager;


        public MainWindow()
        {
            InitializeComponent();

            _settingsService = new SettingsService();
            _fileSystemManager = new FileSystemManager();

            GMAppContext.StartAppSettings = _settingsService.GetStartAppSettings();
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            tbfilePath.Text = GMAppContext.StartAppSettings?.PathToGarbageFolder ?? "<-- Please choose garbage directory";
        }

        private void btnChooseGarbageDirectory_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
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

        private void btnStartForceCleanup_Click(object sender, RoutedEventArgs e)
        {
            _fileSystemManager.StartCleanUp();
            MessageBox.Show("Success");
        }
    }
}
