using JRWatermarkTextWriter;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterMarkDemo
{
    public partial class ShowImageForm : Form
    {
        public ShowImageForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var remoteOriginImagePath = "https://i.loli.net/2018/09/05/5b8fc930eb809.bmp";
            var localOriginImagePath = Path.Combine(Application.StartupPath, "SampleImage\\sampleImage.PNG");
            var savePath = Path.Combine(Application.StartupPath, "WaterMarkImage.png");
            var isGenerateSuccess = await WatermarkTextWriter.WriteWaterMarkTextAsync(remoteOriginImagePath, "HelloWorld", Color.Red, 30, savePath);
            if (isGenerateSuccess && File.Exists(savePath))
            {
                pictureBox2.Image = Image.FromFile(savePath);
                return;
            }
            else
            {
                MessageBox.Show("The generated image path is not exist!");
                return;
            }
        }
    }
}