using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.DAL;
using ProductAPI.DTO;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CardController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var data = _appDbContext.PaymentCards.ToList();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCard(PaymentCard paymentCard)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            PaymentCard dbModel = await _appDbContext.PaymentCards.FirstOrDefaultAsync(x => x.CardNumber == paymentCard.CardNumber);
            if (dbModel != null)
            {
                return Ok($"bele bir kart artiq movcuddur {dbModel.CardNumber}");
            }

            await _appDbContext.PaymentCards.AddAsync(paymentCard);
            await _appDbContext.SaveChangesAsync();
            return Ok("kart elave edildi");
        }

        //[HttpPut(Name = "Edit Card Details All")]
        //public async Task<IActionResult> EditCard(PaymentCard paymentCard)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return NotFound();
        //    }
        //    PaymentCard dbModel = await _appDbContext.PaymentCards.FirstOrDefaultAsync(x => x.Id == paymentCard.Id);
        //    if (dbModel == null)
        //    {
        //        return Ok("Kart editlenmedi");
        //    }
        //    dbModel.CardNumber = paymentCard.CardNumber;
        //    dbModel.Balance = paymentCard.Balance;
        //    dbModel.Cvc = paymentCard.Cvc;
        //    dbModel.Date = paymentCard.Date;

        //    await _appDbContext.SaveChangesAsync();
        //    return Ok("kart duzelish edildi");
        //}

        [HttpPut]
        public async Task<IActionResult> EditCardBalance(CardDTO card)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            PaymentCard dbModel = await _appDbContext.PaymentCards.FirstOrDefaultAsync(x => x.CardNumber == card.CardNumber && x.Cvc == card.Cvc && x.Date == card.Date);
            if (dbModel == null)
            {
                return Ok("Kart editlenmedi");
            }

            dbModel.Balance -= card.Price;
            await _appDbContext.SaveChangesAsync();
            return Ok("Odenish ugurla kechdi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCard(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            PaymentCard dbModel = await _appDbContext.PaymentCards.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.PaymentCards.Remove(dbModel);
            await _appDbContext.SaveChangesAsync();
            return Ok("Kart silindi");
        }
    }
}
