using hospitalfinal.Models;
using hospitalfinal.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hospitalfinal.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandevuController : ControllerBase
    {
        private readonly HospitalContext _context;
        public RandevuController(HospitalContext context)
        {
            _context = context;
        }
        [HttpGet("getBrans")]
        public async Task<IActionResult> GetBrans()
        {
            var brans = await _context.Branslars.ToListAsync();
            return Ok(brans);

        }
        [HttpGet("getBrans/{bransId}")]
        public async Task<IActionResult> GetDoktorsByBrans(int bransId)
        {
            var doktorlar = await _context.Doktorlars
                .Where(d => d.BransId == bransId)
                .Select(d => new { d.DoktorId, d.DoktorAd, d.DoktorSoyad })
                .ToListAsync();

            return Ok(doktorlar); // Doktorlar listesini döndür
        }
        [HttpGet("getAvailableDates/{doktorId}")]
        public async Task<IActionResult> GetAvailableDates(int doktorId)
        {
            var availableDates = await _context.RandevuMusaits
                .Where(r => r.DoktorId == doktorId && r.Durum == 0) // Uygun olan randevular
                .Select(r => r.MusaitTarih.ToString("yyyy-MM-dd")) // Sadece tarihleri al
                .Distinct() // Tarihleri tekrarsız hale getir
                .ToListAsync();

            return Ok(availableDates); // Tarih listesini döndür
        }
        [HttpGet("getAvailableTimes/{date}")]
        public async Task<IActionResult> GetAvailableTimes(DateTime date)
        {
            var availableTimes = await _context.RandevuMusaits
                .Where(r => r.MusaitTarih.Date == date.Date && r.Durum == 0) // Tarih ve uygunluk kontrolü
                .Select(r => r.MusaitSaat) // Saatleri al
                .ToListAsync();

            return Ok(availableTimes); // Saat listesini döndür
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAppointment([FromBody] RandevuViewModel appointment)
        {
            if (appointment == null || string.IsNullOrEmpty(appointment.Tarih.ToString()) || string.IsNullOrEmpty(appointment.Saat.ToString()))
            {
                return BadRequest("Eksik veya geçersiz veri.");
            }
            int? hastaId = HttpContext.Session.GetInt32("HastaId");
            // Yeni randevu nesnesi oluştur
            var yeniRandevu = new Randevular
            {
                DoktorId = appointment.DoktorId,
                RandevuTarihi = appointment.Tarih,
                RandevuSaati = appointment.Saat,
                HastaId = (int)hastaId,// Hasta bilgisi login'den alınabilir
            };
            DateTime selectedDate = appointment.Tarih.Date;
            var musait = _context.RandevuMusaits
    .FirstOrDefault(r => r.MusaitTarih.Date == appointment.Tarih.Date &&
                         r.MusaitSaat == appointment.Saat);


            Console.WriteLine(appointment.Tarih.Date + " " + appointment.Saat + selectedDate);
            musait.Durum = 1;
            // Veritabanına ekle
            _context.Randevulars.Add(yeniRandevu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Veritabanı güncelleme hatası: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }


            return Ok(new { Success = true, Message = "Randevu başarıyla oluşturuldu." });
        }


    }
}
