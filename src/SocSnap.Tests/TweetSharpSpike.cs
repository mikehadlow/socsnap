using System;
using System.Linq;
using NUnit.Framework;
using TweetSharp;
using System.Threading;
using System.Threading.Tasks;

namespace SocSnap.Tests
{
	[TestFixture()]
	public class TweetSharpSpike
	{
		private const string consumerKey = "i0creK9uysEuuVnEXyBHQ";
		private const string consumerSecret = "9tXMbNp00bPmSfSP9JYBSTqPt8HPeZei45NyJ3oPs";

		private const string accessToken = "732667386-bA0Me5Bk7Ej3ZXaAyea6qLjYdYUoaEpRYrGe8C4i";
		private const string accessTokenSecret = "enR0q8ZyBv9RAJXKBPlKJGlLJt0ZhOJ0B9gpVwBU08";

		[Test]
		public void CanListMentions()
		{
			var service = new TwitterService(consumerKey, consumerSecret);
			service.AuthenticateWith(accessToken, accessTokenSecret);

			var mentions = service.ListTweetsMentioningMe();

			Console.WriteLine("Number of mentions: {0}", mentions.Count());

			foreach(var mention in mentions)
			{
				Console.WriteLine("{0}", mention.RawSource);
			}
		}

		[Test]
		public void CanUpdateStatus()
		{
			var service = new TwitterService(consumerKey, consumerSecret);
			service.AuthenticateWith(accessToken, accessTokenSecret);

			var tweet = service.SendTweet("@SocSnapTest " + DateTime.Now.ToLongTimeString() + " @SocSnapTest");
			Console.WriteLine(tweet.Text);
		}

		[Test]
		public void CanUpdateStatusWithMedia()
		{
			var service = new TwitterService(consumerKey, consumerSecret);
			service.AuthenticateWith(accessToken, accessTokenSecret);


		}

		[Test]
		public void CanGetUserStream()
		{
			var service = new TwitterService(consumerKey, consumerSecret);
			service.AuthenticateWith(accessToken, accessTokenSecret);

			var block = new AutoResetEvent(false);

			Console.WriteLine("About to start listening on stream");

			Task.Factory.StartNew(() => block.Set());

//			service.StreamUser((streamEvent, response) => 
//            {
//				Console.WriteLine(streamEvent.RawSource);
//				block.Set();
//			});

			Console.WriteLine("Waiting for events");
			block.WaitOne();

			service.CancelStreaming();
		}
	}
}

