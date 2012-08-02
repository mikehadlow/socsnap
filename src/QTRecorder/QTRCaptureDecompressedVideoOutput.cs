using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;

using MonoMac.CoreVideo;
using MonoMac.CoreImage;
using MonoMac.QTKit;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace QTRecorder
{
	public class QTRCaptureDecompressedVideoOutput : QTCaptureDecompressedVideoOutput
	{
		private bool capture = false;

		public QTRCaptureDecompressedVideoOutput () : base()
		{
		}

		public override void OutputVideoFrame (CVImageBuffer videoFrame, QTSampleBuffer sampleBuffer, QTCaptureConnection connection)
		{
			if (videoFrame == null) {
				throw new ArgumentNullException ("videoFrame");
			}
			if (sampleBuffer == null) {
				throw new ArgumentNullException ("sampleBuffer");
			}
			if (connection == null) {
				throw new ArgumentNullException ("connection");
			}

			if (!capture) {
				videoFrame.Dispose ();
				sampleBuffer.Dispose ();
				return;
			}

			Console.WriteLine("Capturing photo");
			capture = false;

			
			try 
			{
				var imagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
				                             "captured_image.tiff");

				var image = CIImage.FromImageBuffer(videoFrame);
				var imageRep = new NSBitmapImageRep(image);
				
				var tiffImage = imageRep.TiffRepresentation;

				var bytes = new byte[tiffImage.Length];
				Marshal.Copy(tiffImage.Bytes, bytes, 0, (int)tiffImage.Length);
				File.WriteAllBytes(imagePath, bytes);

				//			NSError error;
				//			tiffImage.Save(imagePath, MonoMac.Foundation.NSDataWritingOptions.FileProtectionNone, out error);
				
				Console.WriteLine("Wrote image: '{0}'", imagePath);
				
				tiffImage.Dispose();
				imageRep.Dispose();
				videoFrame.Dispose();
				sampleBuffer.Dispose();
			} 
			catch (Exception ex) 
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public void Capture ()
		{
			capture = true;
		}
	}
}

