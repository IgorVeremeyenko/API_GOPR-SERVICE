﻿using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using System.Text.Json;

namespace API_GOPR_SERVICE
{
    public class PushNotification
    {      

        public async void SendMessage(Notification msg, string DeviceToken)
        {
            var messaging = FirebaseMessaging.GetMessaging(FirebaseApp.DefaultInstance);

            // See documentation on defining a message payload.
            var message = new Message
            {
                Notification = new Notification()
                {
                    Title = msg.Title,
                    Body = msg.Body,
                    ImageUrl = "https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",

                },
                Android = new AndroidConfig()
                {

                    TimeToLive = TimeSpan.FromHours(1),
                    Notification = new AndroidNotification()
                    {
                        Icon = "https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg",
                        Color = "#f45342",
                        Sound = "/Resourses/zvonok.mp3"

                    },
                    Priority = Priority.High

                },
                Webpush = new()
                {
                    FcmOptions = new()
                    {
                        Link = "https://elite-service-92d53.web.app/"
                    },
                },
                Apns = new ApnsConfig()
                {
                    Aps = new Aps()
                    {
                        Badge = 42,
                    },

                },

                Token = DeviceToken,
            };
            string jsonString = JsonSerializer.Serialize(message);
            // Send a message to the device corresponding to the provided
            // registration token.
            // Response is a message ID string.
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine("Successfully sent message: " + response);
        }
    }
}