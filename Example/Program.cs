using System;
using System.Collections.Generic;
using nassist.ServiceInterface.Services;
using nassist.Shared.SensorData;
using ServiceStack;

namespace Example
{
    public class Program
    {
        private static IJsonServiceClient client;
        private static AuthenticateResponse credentials;

        public static void Main(string[] args)
        {
            client = new JsonServiceClient("http://dev.nassist-test.com/api");
            credentials = client.Post(new Authenticate { UserName = "demo", Password = "demo", RememberMe = true });

            uploadSensorValues();

            uploadSensorStatuses();

            getSensorValues();

            getSensorStatuses();

            getSensorsForInstallation();

            getNotificationsByType();

            Console.ReadKey();
        }

        public static void uploadSensorValues()
        {
            try
            {
                client.Post(new SensorValues
                {
                    Id = "127126ef-a96a-4177-9a7f-cd28f0e79326",
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
                    Id = "127126ef-a96a-4177-9a7f-cd28f0e79326",
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
            var valuesResponse = client.Get(new SensorValues { Id = "127126ef-a96a-4177-9a7f-cd28f0e79326" });

            foreach (var dataValue in valuesResponse.Values)
            {
                Console.WriteLine("Date: " + dataValue.Date + " Value: " + dataValue.Value);
            }
        }

        public static void getSensorStatuses()
        {
            var statusesResponse = client.Get(new SensorStatuses { Id = "127126ef-a96a-4177-9a7f-cd28f0e79326" });

            foreach (var dataValue in statusesResponse.Statuses)
            {
                Console.WriteLine("Date: " + dataValue.Date + " Status: " + dataValue.Status);
            }
        }

        public static void getSensorsForInstallation()
        {
            var installationSensors = client.Get(new InstallationSensors { Id = new Guid("00000000-0000-0000-0000-b827eb9e544b") });

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
    }
}
