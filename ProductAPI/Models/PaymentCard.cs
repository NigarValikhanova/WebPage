namespace ProductAPI.Models
{
    public class PaymentCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
    }
}
