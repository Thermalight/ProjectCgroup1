using MQTTnet;
using MQTTnet.Client;
using System.Text;
using System.Text.Json;
using ChengetaWebApp.Api.Database.Models;

namespace ChengetaWebApp.Api.Services.MqttHandler;

public class MqttService
{
    private readonly DatabaseService _databaseService;
    private readonly EmailService _emailService;
    public MqttService(DatabaseService databaseService, EmailService emailService)
    {
        _databaseService = databaseService;
        _emailService = emailService;
    }
    public void Start() => HandleMessages();

    private async Task HandleMessages()
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

    private async Task MessageHandler(MqttApplicationMessageReceivedEventArgs args)
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var notification = JsonSerializer.Deserialize<Notification>(Encoding.UTF8.GetString(args.ApplicationMessage.Payload), options);

        if (notification != null)
        {
            await _databaseService.AddNotificationAsync(notification);
            _emailService.MailSubscribers(notification);
        }
        await Task.Delay(1);
    }
}