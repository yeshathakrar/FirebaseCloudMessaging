using System;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;

namespace FirebaseConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var DemoProject = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(@"C:\Users\yesha.t\Desktop\WORK\Firebase-POC\fir-project-64a56-firebase-adminsdk-4dzfs-8cdf15469c.json"),
            });

            var MyProject = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(@"C:\Users\yesha.t\Desktop\WORK\Firebase-POC\notifications-poc-fab77-firebase-adminsdk-gx7f1-49ab706663.json"),
            }, "MyProject");

            // This registration token comes from the client FCM SDKs.
            var registrationToken = "eOKNcZguS8yPtP6SsNK75h:APA91bH1tQgrFt8kmK7JQUnElmTwnFbV4TCl55T1E-KUN1E_2eDPy0_NsKfeHJ0DDcpUxk47QoO42pqFVpC4NlHBCP5AUgqFXAhExCZVka7bsJty9ABd0HN8rsC35gQ2TDgjlbl6YSwJ";

            // See documentation on defining a message payload.
            var message = new Message()
            {
                Token = registrationToken,
                Notification = new Notification()
                {
                    Title = "Firebase",
                    Body = "Notification sent from Firebase"
                }
            };
            //Send notification to single device
            //WriteResponse(message, DemoProject);

            //subscribe to topic
            //1. News
            //2. Photos
            SubscribeToTopic("TestWithEmptyToken");
            //UnsubscribeFromTopic("Photos");
            //SendToTopic("News");
        }
        public static void WriteResponse(Message message, FirebaseApp DemoProject)
        {
            //var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            //var response = FirebaseMessaging.DefaultInstance.SendAsync(message);
            var response = FirebaseMessaging.GetMessaging(DemoProject).SendAsync(message);
            var result = System.Threading.Tasks.Task.WhenAll(response);
            // Response is a message ID string.
            Console.WriteLine("Successfully sent message: " + result.Result);
        }
    
        public static void SubscribeToTopic(string topic)
        {
            var registrationTokens = new System.Collections.Generic.List<string>()
            {
                "DummyToken"
                //"eOKNcZguS8yPtP6SsNK75h:APA91bH1tQgrFt8kmK7JQUnElmTwnFbV4TCl55T1E-KUN1E_2eDPy0_NsKfeHJ0DDcpUxk47QoO42pqFVpC4NlHBCP5AUgqFXAhExCZVka7bsJty9ABd0HN8rsC35gQ2TDgjlbl6YSwJ",
                //"dYQgaAx3QRmu6SDHCC0jAn:APA91bFmuwhBF0bjZfQ6671ziAUHkrTWrKE9WKH3uP0E0xtXfqog1CwjCh3vvGzuvorjlWuM44W4i2pS13cCwEd3lho7cmj3eMqeOJ4poBBwOdxXQ9JhDtHinjtLbN4CyuiNpumi2epQ"
            };

            // Subscribe the devices corresponding to the registration tokens to the
            // topic
            var response = FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(
                registrationTokens, topic);
            var result = System.Threading.Tasks.Task.WhenAll(response);
            // See the TopicManagementResponse reference documentation
            // for the contents of response.
            Console.WriteLine("tokens were subscribed successfully - " + result.Result);
        }

        public static void UnsubscribeFromTopic(string topic)
        {
            var registrationTokens = new System.Collections.Generic.List<string>()
            {
                "eOKNcZguS8yPtP6SsNK75h:APA91bH1tQgrFt8kmK7JQUnElmTwnFbV4TCl55T1E-KUN1E_2eDPy0_NsKfeHJ0DDcpUxk47QoO42pqFVpC4NlHBCP5AUgqFXAhExCZVka7bsJty9ABd0HN8rsC35gQ2TDgjlbl6YSwJ",
                "dYQgaAx3QRmu6SDHCC0jAn:APA91bFmuwhBF0bjZfQ6671ziAUHkrTWrKE9WKH3uP0E0xtXfqog1CwjCh3vvGzuvorjlWuM44W4i2pS13cCwEd3lho7cmj3eMqeOJ4poBBwOdxXQ9JhDtHinjtLbN4CyuiNpumi2epQ"
            };

            // Subscribe the devices corresponding to the registration tokens to the
            // topic
            var response = FirebaseMessaging.DefaultInstance.UnsubscribeFromTopicAsync(
                registrationTokens, topic);
            var result = System.Threading.Tasks.Task.WhenAll(response);
            // See the TopicManagementResponse reference documentation
            // for the contents of response.
            Console.WriteLine("tokens were unsubscribed successfully - " + result.Result);
        }

        public static void SendToTopic(string topic)
        {
            //string condition = "'News' in topics && 'Photos' in topics";
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = "News & Photos Notification",
                    Body = "Latest News & Photos"
                },
                Topic = topic,
                //Condition = condition
            };
            var response = FirebaseMessaging.DefaultInstance.SendAsync(message);
            var result = System.Threading.Tasks.Task.WhenAll(response);
            // See the TopicManagementResponse reference documentation
            // for the contents of response.
            Console.WriteLine("tokens were subscribed successfully - " + result.Result);
        }
    }
}
