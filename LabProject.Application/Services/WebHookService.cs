using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Services
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using global::LabProject.Application.Interfaces;
    using global::LabProject.Domain.Entities;
    using Microsoft.Extensions.Configuration;

    namespace LabProject.Application.Services
    {
        public class WebhookService(HttpClient httpClient, IConfiguration configuration) : IWebhookService
        {
            private readonly HttpClient _httpClient = httpClient;
            private readonly IConfiguration _configuration = configuration;

            public async Task NotifyAppointmentCreatedAsync(Appointment appointment)
            {
                var webhookUrl = _configuration["Webhooks:AppointmentCreated"];

                if (string.IsNullOrWhiteSpace(webhookUrl))
                    return;

                var payload = JsonSerializer.Serialize(new
                {
                    appointment.Id,
                    appointment.ClientId,
                    appointment.ProviderId,
                    appointment.Status
                });

                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                Console.WriteLine("Sending webhook to: " + webhookUrl);
                Console.WriteLine("Payload: " + payload);

                await _httpClient.PostAsync(webhookUrl, content);
            }
        }

    }
}
