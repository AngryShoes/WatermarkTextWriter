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
            string sourceImagePath = "https://i.loli.net/2018/09/05/5b8fc930eb809.bmp";
            var imageStream = await GetImageStream(sourceImagePath);
            if (imageStream == null) return;

            byte[] imageBytes = new byte[1024];
            Image sourceImage = null;
            using (MemoryStream ms = new MemoryStream())
            {
                int length;
                while ((length = imageStream.Read(imageBytes, 0, imageBytes.Length)) > 0)
                {
                    ms.Write(imageBytes, 0, length);
                }
                sourceImage = Image.FromStream(ms);
            }
            if (sourceImage != null)
            {
                string newPath = Application.StartupPath + "\\WaterMarkImage.png";
                bool isGenerateSuccess = WatermarkTextWriter.WriteWaterMarkText(newPath, "HelloWold", "Red", 20, sourceImage);
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

        private async Task<Stream> GetImageStream(string sourceUrl)
        {
            Stream imageStream = null;
            try
            {
                WebRequest webRequest = WebRequest.Create(sourceUrl);
                WebResponse webResponse = webRequest.GetResponse();
                imageStream = webResponse.GetResponseStream();
                await Task.FromResult(imageStream);
            }
            catch (TimeoutException timeoutException)
            {
                MessageBox.Show(timeoutException.Message);
                return imageStream;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return imageStream;
            }
            return imageStream;
        }
    }
}