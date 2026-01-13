namespace WebApp
{
    public class Fatura
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.Now;
        public decimal ValorComIva { get; set; }
        public decimal ValorSemIva { get; set; }
        public string? UrlDocumento { get; set; }
        public string EmailCliente { get; set; } = string.Empty;
    }
}