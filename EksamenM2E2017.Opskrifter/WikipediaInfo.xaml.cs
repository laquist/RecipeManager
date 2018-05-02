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
using Services;

namespace EksamenM2E2017.Opskrifter
{
    /// <summary>
    /// Interaction logic for WikipediaInfo.xaml
    /// </summary>
    public partial class WikipediaInfo : Window
    {
        public WikipediaInfo()
        {
            InitializeComponent();

        }

        public WikipediaInfo(string searchWord)
        {
            InitializeComponent();

            string endPoint = "https://da.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro=&explaintext=&titles={0}";

            ApiAccess aa = new ApiAccess(endPoint);

            TblWikiContent.Text = aa.GetApiResponse(searchWord);
        }
    }
}
