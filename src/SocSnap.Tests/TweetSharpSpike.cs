using System;
using NUnit.Framework;
using TweetSharp;

namespace SocSnap.Tests
{
	[TestFixture()]
	public class TweetSharpSpike
	{
		[Test()]
		public void CanPostStatus ()
		{
			var twitterService = new TwitterService();
			var tweets = twitterService.ListTweetsOnPublicTimeline();

			foreach(var tweet in tweets)
			{
				if(tweet == null)
				{
					Console.WriteLine("tweet is null");
					continue;
				}

				Console.WriteLine("{0}", tweet.Text);
			}
		}
	}
}

