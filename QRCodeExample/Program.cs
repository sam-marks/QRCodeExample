using System;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using SkiaSharp;

namespace QRCodeExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string qrText = "https://sammarks.com";
            SKBitmap qrCodeImage = GenerateQRCodeImage(qrText);

            // Save as PNG
            using (var ms = new MemoryStream())
            {
                SaveImage(qrCodeImage, ms);
                var qrCodeBase64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("QR Code (Base64): " + qrCodeBase64);
                File.WriteAllBytes("qrcode.png", ms.ToArray()); // Save the PNG to a file
            }

            // Save as SVG
            SaveSvg(qrText, "qrcode.svg");
        }

        private static SKBitmap GenerateQRCodeImage(string text)
        {
            var options = new EncodingOptions
            {
                Width = 250,
                Height = 250,
                Margin = 1
            };

            var writer = new BarcodeWriterSvg
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };

            var bitMatrix = writer.Encode(text);
            return ToSkBitmap(bitMatrix);
        }

        private static SKBitmap ToSkBitmap(BitMatrix matrix)
        {
            int width = matrix.Width;
            int height = matrix.Height;
            var bitmap = new SKBitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bitmap.SetPixel(x, y, matrix[x, y] ? SKColors.Black : SKColors.White);
                }
            }

            return bitmap;
        }

        private static void SaveImage(SKBitmap image, Stream stream)
        {
            using (var skStream = new SKManagedWStream(stream))
            {
                image.Encode(skStream, SKEncodedImageFormat.Png, 100);
            }
        }

        private static void SaveSvg(string text, string filePath)
        {
            var options = new EncodingOptions
            {
                Width = 250,
                Height = 250,
                Margin = 1
            };

            var writer = new BarcodeWriterSvg
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };

            var svgImage = writer.Write(text);
            File.WriteAllText(filePath, svgImage.Content);
        }
    }
}
