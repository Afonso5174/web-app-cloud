using Azure;
using Azure.AI.OpenAI;
using Azure.Communication.Email;
using static System.Net.WebRequestMethods;

namespace web_app
{
    public class NotificationService
    {
        private readonly string _aiEndpoint = "https://a2240-mj1m72v4-swedencentral.services.ai.azure.com/";
        private readonly string _aiKey = "BLbOKzM6mkbYgYPUijmXC2OZ2fhonA83AbfCZ0q0hAQDec1ZyFC8JQQJ99BLACfhMk5XJ3w3AAAAACOGW9Ux";
        private readonly string _emailConnString = "endpoint=https://web-com5174.france.communication.azure.com/;accesskey=4UNPEPEvXm4IJpmIQXTjVSkAjRKRBI6cC48tRxlqqhPG6fIIGETeJQQJ99BLACULyCp9EKveAAAAAZCS6Du0";
        private readonly string _emailRemetente = "DoNotReply@1ec9dea3-c58e-4067-9e52-2f166fb2b124.azurecomm.net"; // O email que o Azure te deu

        // 1. Gerar Mensagem com IA
        public async Task<string> GerarMensagemAgradecimento(string nomeCliente)
        {
            var client = new OpenAIClient(new Uri(_aiEndpoint), new AzureKeyCredential(_aiKey));

            // Prompt pedido no enunciado
            string prompt = $"Gera-me uma mensagem de agradecimento de email para enviar a um cliente ({nomeCliente}) depois de enviar uma fatura."; 

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "gpt-4o", // O nome que deste ao deploy no Azure
                Messages =
                {
                    new ChatRequestSystemMessage("Tu és um assistente útil de faturação."),
                    new ChatRequestUserMessage(prompt)
                }
            };

            Response<ChatCompletions> response = await client.GetChatCompletionsAsync(chatCompletionsOptions);
            return response.Value.Choices[0].Message.Content;
        }

        // 2. Enviar Email
        public async Task EnviarEmail(string emailDestino, string assunto, string mensagem)
        {
            var emailClient = new EmailClient(_emailConnString);

            var emailContent = new EmailContent(assunto)
            {
                PlainText = mensagem
            };

            var emailMessage = new EmailMessage(
                senderAddress: _emailRemetente,
                content: emailContent,
                recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(emailDestino) }));

            await emailClient.SendAsync(WaitUntil.Completed, emailMessage);
        }
    }
}