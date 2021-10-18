using GarbageManager.Services;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System.Windows;
using GarbageManager.Model;
using System.Windows.Forms;
using Application = System.Windows.Application;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Model.Result;
using TextBox = System.Windows.Controls.TextBox;
using System.Windows.Media;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Globalization;
using GarbageManager.Resource;
using System.Windows.Controls;
using System.Linq;

namespace GarbageManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ISettingsService _settingsService;
        private IFileSystemCleanUp _fileSystemManager;

        public MainWindow()
        {
            InitializeComponent();

            _settingsService = new SettingsService();
            _fileSystemManager = new FileSystemCleanUp();

            InitializeWindow();
        }

        private void StartTest()
        {
            ProccessResult(Result.SuccessResult().BuildMessage("Success"));
            ProccessResult(Result.WarningResult().BuildMessage("Warning"));
            ProccessResult(Result.ErrorResult().BuildMessage(ResultMessages.CleaningFailed));
        }

        private void InitializeWindow()
        {
            var startAppSettingsResult = _settingsService.GetStartAppSettings();
            if (startAppSettingsResult.IsSuccess)
            {
                GMAppContext.StartAppSettings = startAppSettingsResult.GetData ?? new StartAppSettings();

                SetupCurrentUICulture(GMAppContext.StartAppSettings.CultureInfoName);
                cbLanguage.SelectedItem = cbLanguage.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(x => x.Tag.ToString() == GMAppContext.StartAppSettings.CultureInfoName);
            }
            else
            {
                SetupLocalText();
                ProccessResult(startAppSettingsResult);
            }


            //StartTest();
        }

        private void btnChooseGarbageDirectory_Click(object sender, RoutedEventArgs e)
        {
            var filePath = SelectFolderPathBrowserDialog();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            var newStartAppSettings = GMAppContext.StartAppSettings;
            newStartAppSettings.PathToGarbageFolder = filePath;

            GMAppContext.StartAppSettings = newStartAppSettings;
            _settingsService.UpdateSettings(newStartAppSettings);
            tbfilePath.Text = filePath;
        }

        private void btnStartForceCleanup_Click(object sender, RoutedEventArgs e)
        {
            var cleanupResult = _fileSystemManager.StartCleanUp();
            ProccessResult(cleanupResult);
        }

        private void ProccessResult(IResult result)
        {
            var resultBox = new TextBox()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Margin = new Thickness(5),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 180,
                SelectionBrush = new SolidColorBrush(Colors.Transparent),
                SelectionTextBrush = new SolidColorBrush(Colors.Transparent),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                FontWeight = FontWeights.Bold,
                IsReadOnly = true
            };


            resultBox.Text = result.Message;
            switch (result.Status)
            {
                case ResultStatus.Success:
                    resultBox.Background = new SolidColorBrush(Color.FromArgb(95, 57, 240, 41));
                    break;
                case ResultStatus.Warning:
                    resultBox.Background = new SolidColorBrush(Color.FromArgb(95, 211, 96, 34));
                    break;
                case ResultStatus.Error:
                    resultBox.Background = new SolidColorBrush(Color.FromArgb(95, 209, 29, 34));
                    break;
            }

            ResultPanel.Children.Add(resultBox);

            Task.Run(() =>
            {
                Thread.Sleep(7000);

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ResultPanel.Children.Remove(resultBox);
                }));
            });
        }


        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTag = ((ComboBoxItem)cbLanguage.SelectedItem).Tag.ToString();

            SetupCurrentUICulture(selectedTag);
        }

        private void SetupCurrentUICulture(string cultureInfoName)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureInfoName);
            SetupLocalText();

            if (GMAppContext.StartAppSettings.CultureInfoName != cultureInfoName)
            {
                GMAppContext.StartAppSettings.CultureInfoName = cultureInfoName;
                _settingsService.UpdateSettings(GMAppContext.StartAppSettings);
            }
        }

        private void SetupLocalText()
        {
            tbfilePath.Text =
                GMAppContext.StartAppSettings?.PathToGarbageFolder ??
                CommonResources.ShowIfGarbageDirectoryIsNotInstalled;

            lbLanguage.Content = CommonResources.LanguageLable;

            btnStartForceCleanup.Content = CommonResources.StartCleanupButton;
            btnChooseGarbageDirectory.Content = CommonResources.ChooseGarbageDirectoryButton;
        }

        private string SelectFolderPathBrowserDialog()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
                return null;
            }
        }

    }
}
