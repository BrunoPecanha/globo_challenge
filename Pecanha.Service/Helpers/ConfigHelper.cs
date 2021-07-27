using Microsoft.Extensions.Configuration;
using System;

namespace Pecanha.Service.Helpers {
    public class ConfigHelper {
        private static IConfigurationRoot configuracaoGeral;
        public class Config {
            public string Host { get; set; }
            public int Port { get; set; }
            public bool EnableSsl { get; set; }
            public string FromAddress { get; set; }
            public string Password { get; set; }
            public string AddressTo { get; set; }
        }

        private static IConfigurationRoot RecuperaConfiguracao() {
            if (configuracaoGeral == null) {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
                configuracaoGeral = builder.Build();
            }
            return configuracaoGeral;
        }

        public static Config GetEmailSendConfiguration() {
            var config = new Config() {
                Host = RecuperaConfiguracao()["Smtp:Server"],
                Port = Convert.ToInt32(RecuperaConfiguracao()["Smtp:Port"]),
                EnableSsl = Convert.ToBoolean(RecuperaConfiguracao()["Smtp:EnableSSL"]),
                FromAddress = RecuperaConfiguracao()["Smtp:FromAddress"],
                Password = RecuperaConfiguracao()["Smtp:Password"],
                AddressTo = RecuperaConfiguracao()["Smtp:AddressTo"]
            };
            return config;
        }
    }
}

