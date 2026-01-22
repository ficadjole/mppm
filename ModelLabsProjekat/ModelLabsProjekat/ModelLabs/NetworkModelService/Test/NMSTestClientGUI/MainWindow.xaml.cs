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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FTN.Common;
using FTN.ServiceContracts;
using TelventDMS.Services.NetworkModelService.TestClient.Tests;

namespace NMSTestClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestGda testGda;
        private NetworkModelGDAProxy gdaProxy;
        public MainWindow()
        {
            InitializeComponent();
            testGda = new TestGda();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string resultText = string.Empty;

            try
            {
                var selectedItem = comboBox.SelectedItem as ComboBoxItem;

                if (selectedItem == null) {
                
                    MessageBox.Show("Izaberite opciju iz padajuceg menija.");
                    return;
                    
                }

                string odabir = selectedItem.Content.ToString();
                string unesenGID = inputTextBox.Text;

                switch (odabir)
                {
                    case "Get values":
                        if(long.TryParse(unesenGID,out long gid))
                        {
                            var result = testGda.GetValues(gid);
                            resultText = $"Get values result for GID {gid}:\n{result}";
                        }
                        else
                        {
                            resultText = "Unesite ispravan GID (broj).";
                        }
                    break;

                    case "Get extent values":

                        ModelCode modelCode = 0;

                        if (FTN.Common.ModelCodeHelper.GetModelCodeFromString(unesenGID,out modelCode))
                        {
                            var result = testGda.GetExtentValues(modelCode);
                            resultText = $"Get extent values result for ModelCode {modelCode}:\n{result}";
                        }
                        else
                        {
                            resultText = "Unesite ispravan ModelCode (npr. SERIESCOMPENSATOR).";
                        }

                        break;

                    case "Get related values":

                        if (long.TryParse(unesenGID, out long relatedGid))
                        {
                            //var result = testGda.GetRelatedValues(relatedGid);
                            //resultText = $"Get related values result for GID {relatedGid}:\n{result}";
                        }
                        else
                        {
                            resultText = "Unesite ispravan GID (broj).";
                        }
                        break;

                    case "Get total length of DCLineSegments":

                        float totalLength = GetTotalLengthOfDCLineSegments();

                        resultText = $"Ukupna duzina DCLineSegmenata: {totalLength}";
                        break;

                    case "Get shortest ACLineSegment":

                        float shortestLength = GetShortestACLineSegment();

                        resultText = $"Najkraci ACLineSegment ima duzinu: {shortestLength}";
                        break;

                    default:
                        resultText = "Nepoznata opcija.";
                        break;
                }
            }
            catch (Exception ex)
            {
                resultText = $"Došlo je do greške: {ex.Message}";
            }

            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(resultText)));
        }

        private float GetTotalLengthOfDCLineSegments()
        {
            List<long> dcLineSegments = testGda.GetExtentValues(ModelCode.DCLINESEG);
            float totalLength = 0.0f;
            

            foreach (long gid in dcLineSegments)
            {
               var dcLineSegment = testGda.GetValues(gid);

                totalLength += dcLineSegment.GetProperty(ModelCode.CONDUCTOR_LEN).AsFloat();
            }

            return totalLength;
        }

        private float GetShortestACLineSegment()
        {
            float shortestLength = float.MaxValue;

            List<long> acLineSegments = testGda.GetExtentValues(ModelCode.ACLINESEG);

            foreach (long gid in acLineSegments)
            {
                var acLineSegment = testGda.GetValues(gid);
                float length = acLineSegment.GetProperty(ModelCode.CONDUCTOR_LEN).AsFloat();
                if (length < shortestLength)
                {
                    shortestLength = length;
                }
            }

            return shortestLength;
        }
    }
}
