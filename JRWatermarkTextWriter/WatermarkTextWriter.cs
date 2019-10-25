using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace JRWatermarkTextWriter
{
    public class WatermarkTextWriter
    {
        private static float x = 10F, y = 20F;
        private static readonly float width = 120.0F;
        private static readonly float height = 40.0F;
        /// <summary>
        /// Used for write text as watermark in source image
        /// </summary>
        /// <param name="originPath">source image used for generate new image with watermark</param>
        /// <param name="savePath">Generated image file path</param>
        /// <param name="waterText">Text display in image</param>
        /// <param name="color">The color of text</param>
        /// <param name="alpha">alpha is less than 0 or greater than 255</param>
        /// <returns></returns>
        /// 
        public static async Task<bool> WriteWaterMarkTextAsync(string originPath, string waterText, Color color, int alpha, string savePath)
        {
            Image image = await ConvertStreamToImageAsync(originPath);
            if (image == null) return false;
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height,
                GraphicsUnit.Pixel);
            Tuple<bool> tuple = Tuple.Create(false);
            var drawPropertyTool = BuildDrawPropertyTool(alpha, waterText, color);
            try
            {
                DrawString(graphics, drawPropertyTool);
                bitmap.Save(savePath, ImageFormat.Png);
                tuple = Tuple.Create(true);
            }
            catch (Exception ex)
            {
                tuple = Tuple.Create(false);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                bitmap.Dispose();
                image.Dispose();
                graphics.Dispose();
            }
            return tuple.Item1;
        }

        private static async Task<Image> ConvertStreamToImageAsync(string sourceFile)
        {
            var imageStream = await GetImageStreamAsync(sourceFile);
            if (imageStream == null) return null;

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
            return await Task.FromResult(sourceImage);
        }

        private static async Task<Stream> GetImageStreamAsync(string sourceUrl)
        {
            Stream imageStream = null;
            if (File.Exists(sourceUrl))
            {
                imageStream = new FileStream(sourceUrl, FileMode.Open, FileAccess.Read) as Stream;
                return imageStream;
            }
            else
            {
                try
                {
                    WebRequest webRequest = WebRequest.Create(sourceUrl);
                    var webResponse = webRequest.GetResponse() as HttpWebResponse;
                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        imageStream = webResponse.GetResponseStream();
                        await Task.FromResult(imageStream);
                    }

                }
                catch (TimeoutException timeoutException)
                {
                    Console.WriteLine(timeoutException.Message);
                    return imageStream;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return imageStream;
                }
            }
            return imageStream;
        }

        private static DrawPropertyTool BuildDrawPropertyTool(int alpha, string waterText, Color color)
        {
            Font font = new Font("arial", 14);
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha, color));
            return new DrawPropertyTool
            {
                WaterFont = font,
                WaterSolidBrush = solidBrush,
                WaterStringFormat = stringFormat,
                WaterText = waterText,
            };
        }

        private static void DrawString(Graphics graphics, DrawPropertyTool drawPropertyTool)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        RectangleF drawRect = new RectangleF(x, y, width, height);
                        graphics.RotateTransform(-8.0f);
                        graphics.DrawString(drawPropertyTool.WaterText, drawPropertyTool.WaterFont,
                            drawPropertyTool.WaterSolidBrush, drawRect,
                            drawPropertyTool.WaterStringFormat);
                        graphics.ResetTransform();
                        x += 120.0F;
                    }
                    y += 35f;
                }
                x = 0f;
            }
            drawPropertyTool.WaterSolidBrush.Dispose();

        }
        private class DrawPropertyTool
        {
            public string WaterText { get; set; }
            public Font WaterFont { get; set; }
            public SolidBrush WaterSolidBrush { get; set; }
            public StringFormat WaterStringFormat { get; set; }
        }
    }
}