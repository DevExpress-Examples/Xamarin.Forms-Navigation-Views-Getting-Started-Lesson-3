using System;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.XamarinForms.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DrawerSample.Views {
    public partial class RootPage : ContentPage {
        const string IsLandscapeOrientedPropertyName = "IsLandscapeOriented";

        public static readonly BindableProperty IsLandscapeOrientedProperty = BindableProperty.Create(
            IsLandscapeOrientedPropertyName,
            typeof(bool),
            typeof(RootPage),
            defaultValue: false);

        public bool IsLandscapeOriented {
            get => (bool)GetValue(IsLandscapeOrientedProperty);
            set => SetValue(IsLandscapeOrientedProperty, value);
        }

        public RootPage() {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
        }

        protected void OnSizeChanged(object sender, EventArgs args) {
            IsLandscapeOriented = this.Width > this.Height;
        }

        void OnMenuButtonClicked(object sender, EventArgs e) {
            drawer.IsDrawerOpened = !drawer.IsDrawerOpened;
        }

        const string SafeAreaPropertyName = "SafeArea";
        public static readonly BindableProperty SafeAreaProperty = BindableProperty.Create(
            SafeAreaPropertyName,
            typeof(Thickness),
            typeof(RootPage),
            defaultValue: new Thickness());
        public Thickness SafeArea {
            get => (Thickness)GetValue(SafeAreaProperty);
            set => SetValue(SafeAreaProperty, value);
        }
        protected override void OnAppearing() {
            base.OnAppearing();

            var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            this.SafeArea = safeInsets;
        }
    }

    class BoolToDrawerBehaviorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (targetType != typeof(DrawerBehavior)) return null;
            bool boolValue = (bool)value;
            return boolValue ? DrawerBehavior.Split : DrawerBehavior.SlideOnTop;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
