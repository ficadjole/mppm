using FTN.Common;
using FTN.ServiceContracts;
using FTN.Services.NetworkModelService;
using FTN.Services.NetworkModelService.TestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                long gid = 0;
                switch (odabir)
                {
                    case "Get values":
                    gid = InputGlobalId(unesenGID);
                        if (gid!=0)
                        {
                            var result = testGda.GetValues(gid);
                            resultText = $"Get values result for GID {gid}:\n{result.ToString()}";
                        }
                        else
                        {
                            resultText = "Unesite ispravan GID (broj).";
                        }
                    break;

                    case "Get extent values":

                        ModelCode modelCode = InputModelCode(unesenGID);

                        if (modelCode!=0)
                        {
                            var result = testGda.GetExtentValues(modelCode);
                            resultText = $"Get extent values result for ModelCode {modelCode}:\n{GetExtendedValuesAsString(result,testGda)}";
                        }
                        else
                        {
                            resultText = "Unesite ispravan ModelCode (npr. SERIESCOMPENSATOR).";
                        }

                        break;

                    case "Get related values":

                        gid = InputGlobalId(unesenGID);
                        string[] parts = associationTextBox.Text.Split(',');
                        if (parts.Length != 2)
                        {
                            MessageBox.Show("Association mora biti u formatu: PropertyId,Type");
                            return;
                        }

                        string propertyIdStr = parts[0].Trim();
                        string typeStr = parts[1].Trim();

                        Association association = InputAssociation(propertyIdStr,typeStr);

                        MessageBox.Show("Associtano: " + association.PropertyId + " " + association.Type);

                        var relatedIds = testGda.GetRelatedValues(gid, association);

                        
                        if(relatedIds != null && relatedIds.Count > 0)
                        {
                            resultText = $"Get related values result for GID {gid} with association (PropertyId: {association.PropertyId}, Type: {association.Type}):\n{GetExtendedValuesAsString(relatedIds,testGda)}";
                        }
                        else
                        {
                            resultText = $"Nema povezanih vrednosti za GID {gid} sa zadatom asocijacijom.";
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

        #region Helpers


        private static long InputGlobalId(string inputGlobalId)
        {

            try
            {

                if (inputGlobalId.StartsWith("0x", StringComparison.Ordinal))
                {
                    inputGlobalId = inputGlobalId.Remove(0, 2);
                    CommonTrace.WriteTrace(CommonTrace.TraceVerbose, "Entering globalId successfully ended.");

                    return Convert.ToInt64(Int64.Parse(inputGlobalId, System.Globalization.NumberStyles.HexNumber));
                }
                else
                {
                    CommonTrace.WriteTrace(CommonTrace.TraceVerbose, "Entering globalId successfully ended.");
                    return Convert.ToInt64(inputGlobalId);
                }
            }
            catch (FormatException ex)
            {
                string message = "Entering entity id failed. Please use hex (0x) or decimal format.";
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                Console.WriteLine(message);
                throw ex;
            }
        }

        private static ModelCode InputModelCode(string userModelCode)
        {

            try
            {
                ModelCode modelCode = 0;

                if (!ModelCodeHelper.GetModelCodeFromString(userModelCode, out modelCode))
                {
                    if (userModelCode.StartsWith("0x", StringComparison.Ordinal))
                    {
                        modelCode = (ModelCode)long.Parse(userModelCode.Substring(2), System.Globalization.NumberStyles.HexNumber);
                    }
                    else
                    {
                        modelCode = (ModelCode)long.Parse(userModelCode);
                    }
                }

                return modelCode;
            }
            catch (Exception ex)
            {
                string message = string.Format("Entering Model Code failed. {0}", ex);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                Console.WriteLine(message);
                throw ex;
            }
        }

        private static Association InputAssociation(string userModelCode,string type)
        {
            Association association = new Association();

            try
            {
                ModelCode modelCode = 0;

                if (!ModelCodeHelper.GetModelCodeFromString(userModelCode, out modelCode))
                {
                    if (userModelCode.StartsWith("0x", StringComparison.Ordinal))
                    {
                        modelCode = (ModelCode)long.Parse(userModelCode.Substring(2), System.Globalization.NumberStyles.HexNumber);
                    }
                    else
                    {
                        modelCode = (ModelCode)long.Parse(userModelCode);
                    }
                }

                association.PropertyId = modelCode;

                modelCode = 0;

                if (!ModelCodeHelper.GetModelCodeFromString(type, out modelCode))
                {
                    if (type.StartsWith("0x", StringComparison.Ordinal))
                    {
                        modelCode = (ModelCode)long.Parse(type.Substring(2), System.Globalization.NumberStyles.HexNumber);
                    }
                    else
                    {
                        modelCode = (ModelCode)long.Parse(type);
                    }
                }

                association.Type = modelCode;

                return association;
            }
            catch (Exception ex)
            {
                string message = string.Format("Entering association failed. {0}", ex);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                Console.WriteLine(message);
                throw ex;
            }
        }

        private static string GetExtendedValuesAsString(List<long> num,TestGda testGda)
        {
            StringBuilder sb = new StringBuilder();

            foreach (long n in num)
            {
                var result = testGda.GetValues(n);
                sb.AppendLine(result.ToString());
            }

            return sb.ToString();
        }

        #endregion

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedText = selectedItem.Content.ToString();

                if (selectedText == "Get related values")
                {
                    associationTextBox.Visibility = Visibility.Visible;
                }
                else
                {
                    associationTextBox.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
