using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace UbachsBramProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            MyMap.MapServiceToken = "ELOICT";
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;

            // Vergeet niet Location Capability te enabelen!
            var position = await locator.GetGeopositionAsync();

            await MyMap.TrySetViewAsync(position.Coordinate.Point, 18D);

            mySlider.Value = MyMap.ZoomLevel;
        }

        private void getPositionButton_Click(object sender, RoutedEventArgs e)
        {
            PositionTextBlock.Text = String.Format("{0}, {1}",
                MyMap.Center.Position.Latitude,
                MyMap.Center.Position.Longitude);
        }

        private async void setPositionButton_Click(object sender, RoutedEventArgs e)
        {
            var myPosition = new Windows.Devices.Geolocation.BasicGeoposition();
            myPosition.Latitude = 50.9652;
            myPosition.Longitude = 005.4951;

            var myPoint = new Windows.Devices.Geolocation.Geopoint(myPosition);
            if (await MyMap.TrySetViewAsync(myPoint, 10D))
            {

            }
        }

        private void mySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (MyMap != null)
            {
                MyMap.ZoomLevel = e.NewValue;

            } 
        }
    }
}
