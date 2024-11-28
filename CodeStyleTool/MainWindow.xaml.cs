using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace CodeStyleTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OperationButton_OnClick(object sender, RoutedEventArgs e)
        {
            var folder = @"D:\Github-others\mathnet-numerics";
            var csFiles = Directory.GetFiles(folder, "*.cs",SearchOption.AllDirectories);
            foreach (var csFile in csFiles)
            {
                ArrangeStyle(csFile);
            }
        }

        private void ArrangeStyle(string csFile)
        {
            var allLines = File.ReadAllLines(csFile);
            var outputLines = new List<string>();
            RemoveFileTitle(allLines, outputLines);
            File.WriteAllLines(csFile, outputLines);
        }
        /// <summary>
        /// 去除类文件注释
        /// </summary>
        /// <param name="allLines"></param>
        /// <param name="outputLines"></param>
        private static void RemoveFileTitle(string[] allLines, List<string> outputLines)
        {
            var isClearing = false;
            for (int i = 0; i < allLines.Length; i++)
            {
                var line = allLines[i];
                //清理类注释
                var lineStr = line.Replace("//", string.Empty).Trim();
                if (lineStr.StartsWith("<copyright") || lineStr.StartsWith("<contribution"))
                {
                    isClearing = true;
                    continue;
                }
                if (line.EndsWith("/copyright>") || line.EndsWith("/contribution>"))
                {
                    isClearing = false;
                    continue;
                }
                if (isClearing)
                {
                    continue;
                }
                //跳过文件前面空行
                if (outputLines.Count == 0 && string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                outputLines.Add(line);
            }
        }
    }
}