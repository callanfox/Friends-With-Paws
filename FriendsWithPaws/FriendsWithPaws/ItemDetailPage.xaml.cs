using FriendsWithPaws.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.StartScreen;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.DataTransfer;
using System.Text;
using Windows.Storage.Streams;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace FriendsWithPaws
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class ItemDetailPage : FriendsWithPaws.Common.LayoutAwarePage
    {
        public ItemDetailPage()
        {
            this.InitializeComponent();
        }

// ******** SHARING IMPLEMENTATION CODE -- derived from 'Activity 6 - Implement Sharing' ********
        void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;

            var item = (BreedDataItem)this.flipView.SelectedItem;

            request.Data.Properties.Title = item.Title;
            request.Data.Properties.Description = "Breed details and description";

            var breed = "\r\nBreed Details\r\n";

            breed += String.Join("\r\n", item.Breed_details);
            breed += ("\r\n\r\nDescription\r\n" + item.Description);

            request.Data.SetText(breed);

            var reference = RandomAccessStreamReference.CreateFromUri(new Uri(item.ImagePath.AbsoluteUri));

            request.Data.Properties.Thumbnail = reference;
            request.Data.SetBitmap(reference);
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var item = BreedDataSource.GetItem((String)navigationParameter);
            this.DefaultViewModel["Group"] = item.Group;
            this.DefaultViewModel["Items"] = item.Group.Items;
            this.flipView.SelectedItem = item;

            DataTransferManager.GetForCurrentView().DataRequested += OnDataRequested;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            var selectedItem = (BreedDataItem)this.flipView.SelectedItem;
            pageState["SelectedItem"] = selectedItem.UniqueId;

            DataTransferManager.GetForCurrentView().DataRequested -= OnDataRequested;
        }

        private async void OnPinBreedButtonClicked(object sender, RoutedEventArgs e)
        {
            var item = (BreedDataItem)this.flipView.SelectedItem;
            var uri = new Uri(item.TileImagePath.AbsoluteUri);
            var tile = new SecondaryTile(item.UniqueId, item.ShortTitle, item.Title, item.UniqueId, TileOptions.ShowNameOnLogo, uri);
            await tile.RequestCreateAsync();
        }

        private async void OnReminderButtonClicked(object sender, RoutedEventArgs e)
        {
            ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier();
            
            if (notifier.Setting != NotificationSetting.Enabled)
            {
                var dialog = new MessageDialog("Notifications are currently disabled");
                await dialog.ShowAsync();
                return;
            }

            ToastTemplateType toastTemplate = ToastTemplateType.ToastText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode("Reminder! Check Friends With Paws"));
            var date = DateTimeOffset.Now.AddSeconds(15);
            var stnA = new ScheduledToastNotification(toastXml, date);
            notifier.AddToSchedule(stnA);

            ToastTemplateType toastTemplateB = ToastTemplateType.ToastText01;
            XmlDocument toastXmlB = ToastNotificationManager.GetTemplateContent(toastTemplateB);
            XmlNodeList toastTextElementsB = toastXmlB.GetElementsByTagName("text");
            toastTextElementsB[0].AppendChild(toastXmlB.CreateTextNode("Please check what's new with Friends With Paws"));
            var dateB = DateTimeOffset.Now.AddSeconds(60);
            IXmlNode toastNodeB = toastXmlB.SelectSingleNode("/toast");
            ((XmlElement)toastNodeB).SetAttribute("duration", "long");
            var stnB = new ScheduledToastNotification(toastXmlB, dateB);
            notifier.AddToSchedule(stnB);
        }
    }
}

