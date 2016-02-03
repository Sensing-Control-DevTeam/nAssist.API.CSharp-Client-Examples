using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using nassist.ServiceInterface.Services;
using nassist.ServiceModel;
using nassist.Shared.SensorData;
using ServiceStack;

namespace Example
{
    public class Program
    {
        private static IJsonServiceClient client;
        private static AuthenticateResponse credentials;

        private const string BASE_URL = "http://dev.nassist-test.com/api";

        private const string CAMERA_ID = "4c03631b-c62b-4ce4-ad0c-998cdbffbfc7";
        private const string SENSOR_ID = "127126ef-a96a-4177-9a7f-cd28f0e79326";
        private const string INSTALLATION_ID = "00000000-0000-0000-0000-b827eb9e544b";

        private const string USERNAME = "demo";
        private const string PASSWORD = "demo";

        public static void Main(string[] args)
        {
            client = new JsonServiceClient(BASE_URL);
            credentials = client.Post(new Authenticate { UserName = USERNAME, Password = PASSWORD, RememberMe = true });

            uploadSensorValues();

            uploadSensorStatuses();

            getSensorValues();

            getSensorStatuses();

            getSensorsForInstallation();

            getNotificationsByType();

            uploadPicture();

            downloadPicture();

            Console.ReadKey();
        }

        public static void uploadSensorValues()
        {
            try
            {
                client.Post(new SensorValues
                {
                    Id = SENSOR_ID,
                    DataPoints = new List<DataPoint> { new DataPoint { Date = DateTime.Now, Value = new Random().NextDouble() } }
                });

                Console.WriteLine("Data upload success!");
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine("Error uploading data: " + ex);
            }
        }

        public static void uploadSensorStatuses()
        {
            try
            {
                client.Post(new SensorStatuses
                {
                    Id = SENSOR_ID,
                    StatusPoints = new List<StatusPoint> { new StatusPoint { Date = DateTime.Now, Status = "ok", Trigger = credentials.UserId, TriggerName = credentials.UserName } }
                });

                Console.WriteLine("Status upload success!");
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine("Error uploading data: " + ex);
            }
        }

        public static void getSensorValues()
        {
            var valuesResponse = client.Get(new SensorValues { Id = SENSOR_ID });

            foreach (var dataValue in valuesResponse.Values)
            {
                Console.WriteLine("Date: " + dataValue.Date + " Value: " + dataValue.Value);
            }
        }

        public static void getSensorStatuses()
        {
            var statusesResponse = client.Get(new SensorStatuses { Id = SENSOR_ID});

            foreach (var dataValue in statusesResponse.Statuses)
            {
                Console.WriteLine("Date: " + dataValue.Date + " Status: " + dataValue.Status);
            }
        }

        public static void getSensorsForInstallation()
        {
            var installationSensors = client.Get(new InstallationSensors { Id = new Guid(INSTALLATION_ID) });

            foreach (var sensor in installationSensors.Sensors)
            {
                Console.WriteLine(sensor.Id + " " + sensor.Name + " " + sensor.Type);
            }
        }

        public static void getNotificationsByType()
        {
            var eventsResponse = client.Get(new EventsBatch { UserId = credentials.UserId, Type = "security" });

            foreach (var evt in eventsResponse.Events)
            {
                Console.WriteLine(evt.Subtype + " " + evt.TranslatedDescription);
            }
        }

        public static void uploadPicture()
        {
            try
            {
                client.Post(new CameraPhoto
                {
                    Id = CAMERA_ID,
                    Date = DateTime.UtcNow,
                    Trigger = credentials.UserId,
                    Base64 = ImageToBase64("logo.jpg")
                });

                Console.WriteLine("Image uploaded successfully!");
            } catch(Exception e)
            {
                Console.WriteLine("Error uploading image: " + e);
            }
            
        }

        public static void downloadPicture()
        {
            var photosResponse = client.Get(new CameraPhoto
            {
                Id = CAMERA_ID,
                FromDate = DateTime.UtcNow.AddDays(-1),
                ToDate = DateTime.UtcNow
            });

            Console.WriteLine("Obtained pictures list for camera: " + photosResponse.CameraName);

            if (photosResponse.Photos.Any())
            {
                Photo p = photosResponse.Photos.First();

                Console.WriteLine("Downloading picture: " + p.TriggerId + "_" + p.Date.ToString("yy-MM-dd") + ".jpg");

                new WebClient().DownloadFile(p.Url, p.TriggerId + "_" + p.Date.ToString("yy-MM-dd") + ".jpg");

                Console.WriteLine("Picture downloaded successfully!");
            }
        }

        #region Helpers

        public static string ImageToBase64(string fileName)
        {
            Image i = Image.FromFile(fileName);

            using (var stream = new MemoryStream())
            {
                i.Save(stream, ImageFormat.Jpeg);

                return Convert.ToBase64String(stream.ToArray());
            }
        }

        #endregion
    }
}
