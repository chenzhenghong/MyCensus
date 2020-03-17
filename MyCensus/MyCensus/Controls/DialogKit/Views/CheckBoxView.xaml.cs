﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCensus.Controls.DialogKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckBoxView : ContentView
    {
        public CheckBoxView(string title,string message, params string[] options)
        {
            InitializeComponent();
            this.BindingContext = new { Title = title, Message = message };
            for (int i = 0; i < options.Length; i++)
                slContent.Children.Add(new CheckBox(options[i],i));
        }

        public CheckBoxView(string title,string message,Dictionary<int,string> keyValues)
        {
            InitializeComponent();
            this.BindingContext = new { Title = title, Message = message };
            foreach (var item in keyValues)
                slContent.Children.Add(new CheckBox(item.Value,item.Key));
        }
        public event EventHandler<IList<int>> Completed;

        private void Confirm_Clicked(object sender, EventArgs e)
        {
            Completed?.Invoke(this, GetSelectedKeys());
        }
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Completed?.Invoke(this, null);
        }
        public static readonly BindableProperty SelectedKeysProperty = BindableProperty.Create(nameof(SelectedKeys), typeof(IList<int>), typeof(CheckBoxView), new[] { 0 }, propertyChanged: (bo, ov, nv) => (bo as CheckBoxView).SelectedKeys = (IList<int>)nv);
        public IList<int> SelectedKeys { get => GetSelectedKeys(); set => UpdateSelectedKeys(value); }
        public IList<string> SelectedValues { get => GetSelectedValues(); }
        public IList<int> GetSelectedKeys()
        {
            List<int> selectedKeys = new List<int>();
            foreach (var item in slContent.Children)
                if (item is CheckBox)
                    if ((item as CheckBox).IsChecked)
                        selectedKeys.Add((item as CheckBox).Key);

            return selectedKeys;
        }
        public void UpdateSelectedKeys(IList<int> keys)
        {
            foreach (var item in slContent.Children)
            {
                if (item is CheckBox)
                    (item as CheckBox).IsChecked = keys.Contains((item as CheckBox).Key);
            }
        }
        public IList<string> GetSelectedValues()
        {
            List<string> selectedValues = new List<string>();
            foreach (var item in slContent.Children)
                if (item is CheckBox)
                    if ((item as CheckBox).IsChecked)
                        selectedValues.Add((item as CheckBox).Text);

            return selectedValues;
        }

    }

    public class CheckBox : StackLayout
    {
        BoxView boxBackground = new BoxView
        {
            HeightRequest = 20,
            WidthRequest = 20,
            BackgroundColor = Color.LightGray
        };

        BoxView boxSelected = new BoxView
        {
            IsVisible = false,
            HeightRequest = 10,
            WidthRequest = 10,
            BackgroundColor = Color.Accent,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.Center
        };

        Label lblOption = new Label
        {
            VerticalOptions = LayoutOptions.CenterAndExpand,
            FontSize = 12
        };

        public CheckBox()
        {
            this.Orientation = StackOrientation.Horizontal;
            this.Margin = new Thickness(0);
            this.Padding = new Thickness(2);
            this.Spacing = 5;
            this.Children.Add(new Grid { Children = { boxBackground, boxSelected } });
            this.Children.Add(lblOption);
            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => { IsChecked = !IsChecked; }),
            });
        }
        public CheckBox(string optionName,int key) : this()
        {
            Key = key;
            lblOption.Text = optionName;
        }
        public int Key { get; set; }
        public string Text { get => lblOption.Text; set => lblOption.Text = value; }
        public bool IsChecked { get => boxSelected.IsVisible; set => boxSelected.IsVisible = value; }
        public Color Color { get => boxSelected.BackgroundColor; set => boxSelected.BackgroundColor = value; }


        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(CheckBox), Color.Accent, propertyChanged: (bo, ov, nv) => (bo as CheckBox).Color = (Color)nv);
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBox), false, propertyChanged: (bo, ov, nv) => (bo as CheckBox).IsChecked = (bool)nv);
        public static readonly BindableProperty KeyProperty = BindableProperty.Create(nameof(Key), typeof(int), typeof(CheckBox), 0, propertyChanged: (bo, ov, nv) => (bo as CheckBox).Key = (int)nv);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),typeof(string),typeof(CheckBox),"",propertyChanged:(bo,ov,nv)=>(bo as CheckBox).Text = (string)nv);

    }
}