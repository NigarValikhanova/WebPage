namespace ProductAPI.DTO
{
    public class CardDTO
    {
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
