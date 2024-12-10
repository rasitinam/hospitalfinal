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
    public class AccountController : ControllerBase
    {
        private readonly HospitalContext _context;
        public AccountController(HospitalContext context)
        {
            _context = context;
        }
        [HttpGet("getPatientProfile")]
        public async Task<IActionResult> GetPatientProfile()
        {
            try
            {
                int? hastaId = HttpContext.Session.GetInt32("HastaId");
                var patient = await _context.Hastalars
                                             .FirstOrDefaultAsync(h => h.HastaId == hastaId);
                if (patient == null)
                    return NotFound(new { message = "Hasta bulunamadı." });

                var randevular = await _context.Randevulars
                                               .Where(r => r.HastaId == patient.HastaId)
                                               .ToListAsync();
                return Ok(new
                {
                    Patient = new
                    {
                        hastaAd = patient.HastaAd,
                        hastaSoyad = patient.HastaSoyad,
                        tc = patient.Tc,
                        DogumTarihi = patient.DogumTarihi,
                        email = patient.Email,
                        telefon = patient.Telefon
                    },
                    Randevular = _context.Randevulars
             .Where(r => r.HastaId == patient.HastaId)
             .Select(r => new { r.RandevuTarihi, r.RandevuSaati })
             .ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Bir hata oluştu.", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var patient = await _context.Hastalars.FirstOrDefaultAsync(h => h.Email == model.Email && h.Sifre == model.Password);
                var randevu = await _context.Randevulars.Where(r => r.HastaId == patient.HastaId).ToListAsync();
                HttpContext.Session.SetInt32("HastaId", patient.HastaId);
                return Ok(patient.HastaId);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Email veya Şifre yanlış.", errors = ModelState });
                throw;
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Geçersiz veri.", errors = ModelState });
            }

            // Kullanıcı tipine göre işlem
            switch (model.UserType.ToLower())
            {
                case "patient":
                    var hasta = new Hastalar
                    {
                        HastaAd = model.Name,
                        HastaSoyad = model.SurName,
                        Tc = model.TC,
                        Telefon = model.Phone,
                        Email = model.Email,
                        Sifre = model.Password,
                        Adres = model.Adress,
                    };
                    await _context.Hastalars.AddAsync(hasta);
                    break;

                case "doctor":
                    var doktor = new Doktorlar
                    {
                        DoktorAd = model.Name,
                        DoktorTc = model.TC,
                        Telefon = model.Phone,
                        Email = model.Email,
                        Sifre = model.Password
                    };
                    await _context.Doktorlars.AddAsync(doktor);
                    break;

                case "secretary":
                    var sekreter = new Sekreterler
                    {
                        SekreterAd = model.Name,
                        SekreterTc = model.TC,
                        Telefon = model.Phone,
                        Email = model.Email,
                        Sifre = model.Password
                    };
                    await _context.Sekreterlers.AddAsync(sekreter);
                    break;

                default:
                    return BadRequest(new { message = "Geçersiz kullanıcı tipi." });
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Kayıt başarılı." });
        }
    }
}


