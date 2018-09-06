using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace SmartMirror
{
    /// <summary>
    /// NewsWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NewsWindow : Window
    {
        #region constant
        const string flash_url = @"http://fs.jtbc.joins.com/RSS/newsflash.xml";
        const string politics_url = @"http://fs.jtbc.joins.com/RSS/politics.xml";
        const string economy_url = @"http://fs.jtbc.joins.com/RSS/economy.xml";
        const string society_url = @"http://fs.jtbc.joins.com/RSS/society.xml";
        const string international_url = @"http://fs.jtbc.joins.com/RSS/international.xml";
        const string culture_url = @"http://fs.jtbc.joins.com/RSS/culture.xml";
        #endregion

        #region field
        XmlDocument flash_xml = new XmlDocument();
        XmlDocument politics_xml = new XmlDocument();
        XmlDocument economy_xml = new XmlDocument();
        XmlDocument society_xml = new XmlDocument();
        XmlDocument international_xml = new XmlDocument();
        XmlDocument culture_xml = new XmlDocument();

        /**** rss category ***
             * title
             * link
             * description
             * language
             * copyright
             * category
             * pubDate
             * Item[n]
             */
        XmlNodeList flash_nodelist;
        XmlNodeList politics_nodelist;
        XmlNodeList economy_nodelist;
        XmlNodeList society_nodelist;
        XmlNodeList international_nodelist;
        XmlNodeList culture_nodelist;
        #endregion

        public NewsWindow()
        {
            InitializeComponent();

            try
            {
                flash_xml.Load(flash_url);
                politics_xml.Load(politics_url);
                economy_xml.Load(economy_url);
                society_xml.Load(society_url);
                international_xml.Load(international_url);
                culture_xml.Load(culture_url);

                flash_nodelist = flash_xml.SelectNodes("/rss/channel/item");
                politics_nodelist = politics_xml.SelectNodes("/rss/channel/item");
                economy_nodelist = economy_xml.SelectNodes("/rss/channel/item");
                society_nodelist = society_xml.SelectNodes("/rss/channel/item");
                international_nodelist = international_xml.SelectNodes("/rss/channel/item");
                culture_nodelist = culture_xml.SelectNodes("/rss/channel/item");

                load(flash_nodelist);
            }
            catch (Exception exception)
            {
                
            }
        }

        void load(XmlNodeList xml_nodelist)
        {
            try
            {
                foreach (XmlNode xml_node in xml_nodelist)
                {
                    string title = xml_node["title"].InnerText;
                    string link = xml_node["link"].InnerText;
                    string description = xml_node["description"].InnerText + "...";
                    string pub_date = xml_node["pubDate"].InnerText;

                    News_Item news_item = new News_Item(link);
                    news_item.Width = 200;
                    news_item.Height = 100;

                    news_item.news_title.Text = title;
                    news_item.news_title.ToolTip = description;
                    news_item.news_publish_date.Text = pub_date;

                    news_items.Children.Add(news_item);
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show("뉴스 연결에 문제가 발생하였습니다.");
            }
        }

        #region Event
        private void flash_click(object sender, RoutedEventArgs e)
        {
            news_items.Children.Clear();
            load(flash_nodelist);
        }

        private void politics_click(object sender, RoutedEventArgs e)
        {
            news_items.Children.Clear();
            load(politics_nodelist);
        }

        private void economy_click(object sender, RoutedEventArgs e)
        {
            news_items.Children.Clear();
            load(economy_nodelist);
        }

        private void society_click(object sender, RoutedEventArgs e)
        {
            news_items.Children.Clear();
            load(society_nodelist);
        }

        private void international_click(object sender, RoutedEventArgs e)
        {
            news_items.Children.Clear();
            load(international_nodelist);
        }

        private void culture_click(object sender, RoutedEventArgs e)
        {
            news_items.Children.Clear();
            load(culture_nodelist);
        }
        #endregion
    }
}
