using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Java.Nio;
using ZXing;

namespace ComPact.Droid
{
	public class ZxingBarcode
	{
		public static Bitmap GetQRCode(int height, int width, string qrCodeValue)
		{
			var writer = new BarcodeWriter
			{
				Format = BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions
				{
					Height = height,
					Width = width
				}
			};

			var image = writer.Write(qrCodeValue);
			var buffer = ByteBuffer.Allocate(image.ByteCount);
			image.CopyPixelsToBuffer(buffer);
			var array = buffer.ToArray<byte[]>();

			var array2 = image.ToArray<byte[]>();

			return image;
		}
		public static async Task<string> ScanQrCode(Context context)
		{
			var scanner = new ZXing.Mobile.MobileBarcodeScanner(context);
			var result = await scanner.Scan();

			return result.Text;
		}
	}
}
