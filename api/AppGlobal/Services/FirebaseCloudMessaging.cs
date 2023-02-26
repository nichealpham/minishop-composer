using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Services
{
    public static class FirebaseMessageHelper
    {
        public static Message BuildNotificationMessage<T>(this T @object) where T : class
        {
            var body = new Dictionary<string, string>() {
                { "metadata", @object.ToJsonString() },
                { "sound", "default" },
                { "title", "Sandrasoft" },
                { "message", "You have new notification." },
            };

            return new Message()
            {
                Data = body,
                Apns = new ApnsConfig()
                {
                    Headers = new Dictionary<string, string>() {
                        { "apns-priority" , "10" }
                    },
                    Aps = new Aps()
                    {
                        ContentAvailable = true,
                        Alert = new ApsAlert()
                        {
                            Title = "Sandrasoft",
                            Body = "You have new notification.",
                        },
                        Sound = "default",
                    },
                },
            };
        }
    }

    public class FirebaseCloudMessaging
    {
        public FirebaseCloudMessaging()
        {
            EnsureFirebaseInnitialized();
        }

        #region KHỞI TẠO FIREBASE INSTANCE
        public void EnsureFirebaseInnitialized()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                string serviceAccountPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase.json");

                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(serviceAccountPath),
                });
            }
        }
        #endregion

        #region PHIÊN BẢN SYNC
        // Send message cho 1 cá nhân thông qua device token, sử dụng cho mobile device hoặc web
        // Device token được tự động khơi tạo từ client khi user mở web/app
        // device token luôn được gửi kèm trên mỗi request headers, thẻ MessagingToken
        // send message sử dụng staffID
        public string SendMessage<T>(string channelName, T data) where T : class
        {
            var task = Task.Run(async () => await SendMessageAsync(channelName, data));
            return task.Result;
        }
        // Hàm subscribe user vào channel cá nhân
        public bool SubscribeToChannel(string channelName, string fcmToken)
        {
            var task = Task.Run(async () => await SubscribeToChannelAsync(channelName, fcmToken));
            return task.Result;
        }
        public bool UnSubscribeFromChannel(string channelName, string fcmToken)
        {
            var task = Task.Run(async () => await UnSubscribeFromChannelAsync(channelName, fcmToken));
            return task.Result;
        }
        #endregion

        #region PHIÊN BẢN ASYNC

        // Hàm gửi message message cho cá nhân 1 người sử dụng device tokenID
        public async Task<string> SendMessageAsync<T>(string channelName, T data) where T : class
        {
            EnsureFirebaseInnitialized();

            var cloudMessage = data.BuildNotificationMessage();
            cloudMessage.Topic = channelName;

            string response = await FirebaseMessaging.DefaultInstance.SendAsync(cloudMessage);
            Console.WriteLine("Successfully sent message: " + response);

            return response;
        }
        // Hàm subscribe
        public async Task<bool> SubscribeToChannelAsync(string channelName, string fcmToken)
        {
            EnsureFirebaseInnitialized();
            var registrationTokens = new List<string>() { fcmToken };
            var response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(registrationTokens, channelName);
            Console.WriteLine($"{response.SuccessCount} tokens were subscribed to channel {channelName} successfully!");
            return true;
        }

        // Hàm un-subscribe
        public async Task<bool> UnSubscribeFromChannelAsync(string channelName, string fcmToken)
        {
            EnsureFirebaseInnitialized();
            var registrationTokens = new List<string>() { fcmToken };
            var response = await FirebaseMessaging.DefaultInstance.UnsubscribeFromTopicAsync(registrationTokens, channelName);
            Console.WriteLine($"{response.SuccessCount} tokens were un-subscribed from channel {channelName} successfully");
            return true;
        }
        #endregion
    }
}