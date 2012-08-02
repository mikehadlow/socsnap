// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace QTRecorder
{
	[Register ("QTRDocument")]
	partial class QTRDocument
	{
		[Outlet]
		MonoMac.AppKit.NSLevelIndicator audioLevelIndicator { get; set; }

		[Outlet]
		MonoMac.QTKit.QTCaptureView captureView { get; set; }

		[Action ("StopDevice:")]
		partial void StopDevice (MonoMac.Foundation.NSObject sender);

		[Action ("_takePhotoClick:")]
		partial void _takePhotoClick (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (audioLevelIndicator != null) {
				audioLevelIndicator.Dispose ();
				audioLevelIndicator = null;
			}

			if (captureView != null) {
				captureView.Dispose ();
				captureView = null;
			}
		}
	}
}
