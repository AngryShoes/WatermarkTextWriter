using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WaterMarkDemo
{
    public partial class ShowImageForm : Form
    {
        public ShowImageForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sourceImagePath = "https://i.loli.net/2018/09/05/5b8fc930eb809.bmp";
            WebRequest webRequest = WebRequest.Create(sourceImagePath);
            WebResponse webResponse = webRequest.GetResponse();
            Stream imageStream = webResponse.GetResponseStream();
            byte[] imageBytes = new byte[1024];
            int length = 0;
            Image sourceImage = null;
            using (MemoryStream ms = new MemoryStream())
            {
                while ((length = imageStream.Read(imageBytes, 0, imageBytes.Length)) > 0)
                {
                    ms.Write(imageBytes, 0, length);
                }
                sourceImage = Image.FromStream(ms);
            }
            if (sourceImage != null)
            {
                string newPath = Application.StartupPath + "\\WaterMarkImage.png";
                bool isGenerateSuccess = WatermarkTextWriter.WriteWaterMarkText(newPath, "HelloWold", "LightGray", 200, sourceImage);
                if (isGenerateSuccess && File.Exists(newPath))
                {
                    pictureBox2.Image = Image.FromFile(newPath);
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
}