using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.ApplicationModel.Activation;
using Microsoft.QueryStringDotNET;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPtest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();            
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            //Constructing the content
            var content = new ToastContentBuilder()
                .AddToastActivationInfo("wtf", ToastActivationType.Foreground)
                .AddText("First text")
                .AddText("Second text")
                .GetToastContent();

            //Create the notification
            var notif = new ToastNotification(content.GetXml());//for some reason it uses xml

            //Showing it
            //ToastNotificationManager.CreateToastNotifier().Show(notif);

            int strangeID = 384928;

            var content2 = new ToastContentBuilder()

                //Arguments returned when user taps body of notification
                .AddToastActivationInfo(new QueryString() //Using QueryString.NET
                {
                    { "action", "viewConversation"},
                    { "conversationId", strangeID.ToString() }
                }.ToString(), ToastActivationType.Foreground)
                .AddText("First Text")
                .AddText("Second Text")
                .AddText("Third Text")
                .GetToastContent();

            var notif2 = new ToastNotification(content2.GetXml());

            //ToastNotificationManager.CreateToastNotifier().Show(notif2);

            //int conversationId = 384928;

            var contentImage = new ToastContentBuilder()

                .AddToastActivationInfo(new QueryString() //Using QueryString.NET
                {
                    { "action", "viewConversation"},
                    { "conversationId", strangeID.ToString() }
                }.ToString(), ToastActivationType.Foreground)
                .AddToastActivationInfo("bon _ver", ToastActivationType.Foreground)
                .AddText("Sup")
                .AddText("Some other information")
                .AddInlineImage(new Uri("C:\\Users\\Maksym\\Pictures\\Saved Pictures\\20200121155057fed9ef42de.jpg"))
                //Profile
                .AddAppLogoOverride(new Uri("C:\\Users\\Maksym\\Pictures\\Saved Pictures\\weeknd.jpg"))
                //Text box for replying
                .AddInputTextBox("tbReply", placeHolderContent: "Type a response")
                .GetToastContent();
            var notif3 = new ToastNotification(contentImage.GetXml());

            ToastNotificationManager.CreateToastNotifier().Show(notif3);

            //SendNotificationWithoutHistory(notif3);
        }

        private async void SendNotificationWithoutHistory(ToastNotification toast)
        {
            toast.Tag = "testTag";
            var notifier = ToastNotificationManager.CreateToastNotifier();
            notifier.Show(toast);
            await Task.Delay(3000);
            ToastNotificationManager.History.Remove("testTag");
        }
    }
}
