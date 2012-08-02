using System;
using TweetSharp;
using System.Threading;

namespace SocSnap.UserTokenGrabber
{
	class MainClass
	{
		private const string consumerKey = "i0creK9uysEuuVnEXyBHQ";
		private const string consumerSecret = "9tXMbNp00bPmSfSP9JYBSTqPt8HPeZei45NyJ3oPs";
	
		private const string accessToken = "732667386-bA0Me5Bk7Ej3ZXaAyea6qLjYdYUoaEpRYrGe8C4i";
		private const string accessTokenSecret = "enR0q8ZyBv9RAJXKBPlKJGlLJt0ZhOJ0B9gpVwBU08";

		public static void Main (string[] args)
		{
			var service = new TwitterService(consumerKey, consumerSecret);

//			GetUserToken(service);
			ListenOnUserStream(service);
		}

		public static void GetUserToken(TwitterService service)
		{
			// Step 1 - Retrieve an OAuth Request Token
			OAuthRequestToken requestToken = service.GetRequestToken();

			// Step 2 - Redirect to the OAuth Authorization URL
			Uri uri = service.GetAuthorizationUri(requestToken);

			Console.WriteLine("Enter this uri into a browser:\n{0}", uri.ToString());

			Console.WriteLine("Enter the pin given by twitter");

			// Step 3 - Exchange the Request Token for an Access Token
			string verifier = Console.ReadLine(); // <-- This is input into your application by your user
			OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);

			if(access == null)
			{
				Console.WriteLine("no tokens returned");
				return;
			}

			Console.WriteLine("Access Token:\n{0}", access.Token);
			Console.WriteLine("Access Secret:\n{0}", access.TokenSecret);

			// Step 4 - User authenticates using the Access Token
			service.AuthenticateWith(access.Token, access.TokenSecret);

		}

		public static void ListenOnUserStream(TwitterService service)
		{
			service.AuthenticateWith(accessToken, accessTokenSecret);

			Console.WriteLine("Starting Listen");

			service.StreamUser((streamEvent, response) => 
            {
				Console.WriteLine(streamEvent.RawSource);
			});

			Console.ReadLine();

			service.CancelStreaming();
			Console.WriteLine("Ending Listen");
		}
	}
}
