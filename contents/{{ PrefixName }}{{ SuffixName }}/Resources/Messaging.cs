using Confluent.Kafka;

namespace {{ PrefixName }}{{ SuffixName }}.Resources;

public static class MessagingExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, Settings settings)
    {
        services.AddSingleton<IProducer<Null, string>>(_ =>
        {
            var config = new ProducerConfig
            {
                // MESSAGING_BROKERS (PAO camelCase→UPPER_SNAKE: brokers → BROKERS)
                BootstrapServers = settings.MessagingBrokers,
            };

            // Apply SASL if credentials are present (MESSAGING_USERNAME / MESSAGING_PASSWORD)
            if (!string.IsNullOrEmpty(settings.MessagingUsername))
            {
                config.SaslUsername = settings.MessagingUsername;
                config.SaslPassword = settings.MessagingPassword;
                config.SaslMechanism = Enum.TryParse<SaslMechanism>(settings.MessagingSaslMechanism, ignoreCase: true, out var mech)
                    ? mech
                    : SaslMechanism.Plain;
                config.SecurityProtocol = SecurityProtocol.SaslSsl;
            }

            return new ProducerBuilder<Null, string>(config).Build();
        });
        return services;
    }
}
