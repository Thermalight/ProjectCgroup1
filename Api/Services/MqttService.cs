using MQTTnet;
using MQTTnet.Client;
using System.Text;
using System.Text.Json;
using ChengetaWebApp.Api.Database.Models;

namespace ChengetaWebApp.Api.Database;

public class MqttService
{
    public void Start() => HandleMessages();

    private static async Task HandleMessages()
    {
        var mqttFactory = new MqttFactory();
        using (var mqttClient = mqttFactory.CreateMqttClient())
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(Environment.GetEnvironmentVariable("mqttip"), 1883)
                .WithCredentials(Environment.GetEnvironmentVariable("mqttusername"), Environment.GetEnvironmentVariable("mqttpassword"))
                .Build();

            mqttClient.ApplicationMessageReceivedAsync += e => MessageHandler(e);

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic("chengeta/notifications"); })
                .Build();

            var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            Console.WriteLine("Subscribed to topic");
            await Task.Delay(-1);
        }
    }

    private static async Task MessageHandler(MqttApplicationMessageReceivedEventArgs args)
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var notification = JsonSerializer.Deserialize<Notification>(Encoding.UTF8.GetString(args.ApplicationMessage.Payload), options);

        if (notification != null)
        {
            System.Console.WriteLine(JsonSerializer.Serialize(notification));
        }
        await Task.Delay(1);
    }
}