using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Demo.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CardViewPage : Page
    {
        ObservableCollection<ItemModel> _itemList = new ObservableCollection<ItemModel>();
        public ObservableCollection<ItemModel> ItemList { get { return _itemList; } }

        public CardViewPage()
        {
            this.InitializeComponent();
            InitData();
        }

        void InitData()
        {
            ItemList.Clear();
            ItemList.Add(new ItemModel() { ImagePath = "ms-appx:///Assets/image1.jpg", Content = "One may fall in love with many people during the lifetime.When you finally get your own happiness, you will understand the previous sadness is kind of treasure." });
            ItemList.Add(new ItemModel() { ImagePath = "ms-appx:///Assets/image2.jpg", Content = "Don't forget the things you once you owned.Treasure the things you can't get.Don't give up the things that belong to you and keep those lost things in memory." });
            ItemList.Add(new ItemModel() { ImagePath = "ms-appx:///Assets/image3.jpg", Content = "When you feel hurt and your tears are gonna to drop.Please look up and have a look at the sky once belongs to us.If the sky is still vast,clouds are still clear." });
        }
    }

    public class ItemModel
    {
        public string ImagePath { get; set; }

        public string Content { get; set; }

        public string Time = DateTime.Now.ToString();
    }
}
