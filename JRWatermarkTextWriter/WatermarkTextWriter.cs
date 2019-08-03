using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace JRWatermarkTextWriter
{
    public class WatermarkTextWriter
    {
        /// <summary>
        /// Used for write text as watermark in source image
        /// </summary>
        /// <param name="savePath">Generated iamge file path</param>
        /// <param name="waterText">Text display in image</param>
        /// <param name="color">The color of text</param>
        /// <param name="alpha">alpha is less than 0 or greater than 255</param>
        /// <param name="sourceImage">
        /// source image used for generate new iamge with watermark, [Optional]
        /// </param>
        /// <param name="oldPath">source image used for generate new image with watermark,[Optiona]</param>
        /// <returns></returns>
        public static bool WriteWaterMarkText(string savePath, string waterText, string color, int alpha, Image sourceImage = null, string oldPath = null)
        {
            Image image = string.IsNullOrEmpty(oldPath) ? sourceImage : Image.FromFile(oldPath);
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height,
                GraphicsUnit.Pixel);
            Font font = new Font("arial", 14);
            Tuple<bool> tuple = Tuple.Create(false);

            try
            {
                StringFormat stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha, ColorTranslator.FromHtml(color)));
                float x = 10F, y = 20F;
                float width = 120.0F;
                float height = 40.0F;

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            RectangleF drawRect = new RectangleF(x, y, width, height);
                            graphics.RotateTransform(-8.0f);
                            graphics.DrawString(waterText, font, solidBrush, drawRect, stringFormat);
                            graphics.ResetTransform();
                            x += 120.0F;
                        }
                        y += 35f;
                    }
                    x = 0f;
                }

                solidBrush.Dispose();
                bitmap.Save(savePath, ImageFormat.Png);
                tuple = Tuple.Create(true);
            }
            catch (Exception ex)
            {
                tuple = Tuple.Create(false);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                bitmap.Dispose();
                image.Dispose();
            }
            return tuple.Item1;
        }
    }
}