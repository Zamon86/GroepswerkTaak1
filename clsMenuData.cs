using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace GroepswerkTaak1
{
    [XmlRoot("Menu")]
    public class clsMenuData
    {
        [XmlElement("MenuItem")]
        public List<clsMenuItemData>? Items { get; set; }

        public string xmlFilePath = @"resources\MenuConfig.xml";

        public void ReadDataFromConfigFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(clsMenuData));

            using (FileStream fileStream = new FileStream(xmlFilePath, FileMode.Open))
            {
                var menuData = serializer.Deserialize(fileStream) as clsMenuData;
                Items = menuData?.Items;
            }
        }

        // Functie creëert menu-items
        public List<MenuItem>? CreateMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            if (Items == null) return null;

            // Loop voor hoofdItems in Menu
            foreach (var item in Items)
            {
                MenuItem menuItem = new MenuItem();

                // Creëert speciaal header met Icon
                if (item.IconResourceName != null)
                {
                    menuItem.Header = CreateStackPanelForHeader(item);
                }
                else
                {
                    menuItem.Header = item.Header;
                }

                if (item.SubItems == null) continue;

                List<MenuItem> submenuItems = new List<MenuItem>();

                // Loop voor subItems in Menu  
                foreach (var subItem in item.SubItems)
                {

                    MenuItem submenuItem = new MenuItem();
                    submenuItem.Header = subItem.Header;

                    if (subItem.IconResourceName != null)
                    {
                        submenuItem.Icon = Application.Current.Resources[subItem.IconResourceName];
                    }

                    submenuItems.Add(submenuItem);
                }

                menuItem.ItemsSource = submenuItems;

                menuItems.Add(menuItem);

            }

            return menuItems;
        }

        private static StackPanel CreateStackPanelForHeader(clsMenuItemData item)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            ContentPresenter icon = new ContentPresenter();
            icon.Content = Application.Current.Resources[item.IconResourceName];

            TextBlock textBlock = new TextBlock();
            textBlock.Text = item.Header;
            textBlock.VerticalAlignment = VerticalAlignment.Center;

            stackPanel.Children.Add(icon);
            stackPanel.Children.Add(textBlock);
            return stackPanel;
        }
    }

   
}
