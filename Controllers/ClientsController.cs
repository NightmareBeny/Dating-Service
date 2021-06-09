using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatingService.Data;
using DatingService.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DatingService.Controllers
{
    public class ClientsController : Controller
    {
        private readonly DatingServiceContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        public ClientsController(DatingServiceContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Clients
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            ViewBag.Id = id;
            var partner = await _context.Partner.FirstAsync(l => l.LoginClient == id);
            if (partner.Sign != null)
                partner.Sign = _context.Zodiac.FindAsync(partner.Sign).Result.Name;
            else partner.Sign = "Любой";

            var sign = await _context.Zodiac.OrderBy(n => n.Name).Select(z => z.Name).ToListAsync();
            sign.Insert(0, "Любой");
            ViewBag.Sign = new SelectList(sign);

            var clients = await _context.Client.Where(c=>c.Login!=id).ToListAsync();
            if (partner.Adress != null)
            {
                partner.Adress = partner.Adress.Trim();
                clients = clients.Where(c => c.Adress!=null && c.Adress.Contains(partner.Adress)).ToList();
            }

            if (partner.Education != null)
            {
                partner.Education = partner.Education.Trim();
                clients = clients.Where(c =>c.Education!=null && c.Education.Contains(partner.Education)).ToList();
            }

            if (partner.Hobbies != null)
            {
                partner.Hobbies = partner.Hobbies.Trim();
                clients = clients.Where(c => c.Interests!=null && c.Interests.Contains(partner.Hobbies)).ToList();
            }

            if (partner.Age != null)
                clients = clients.Where(c => c.Age >= partner.Age).ToList();

            if (partner.Height != null)
                clients = clients.Where(c => Convert.ToInt32(c.Height) >= Convert.ToInt32(partner.Height)).ToList();

            if (partner.Weight != null)
                clients = clients.Where(c => Convert.ToInt32(c.Weight) <= Convert.ToInt32(partner.Weight)).ToList();

            switch (partner.Gender)
            {
                case 'Ж':
                    clients = clients.Where(c => c.Gender == 'Ж').ToList();
                    break;
                case 'М':
                    clients = clients.Where(c => c.Gender == 'М').ToList();
                    break;
            }

            switch (partner.Children)
            {
                case true:
                    clients = clients.Where(c => c.Children).ToList();
                    break;
                case false:
                    clients = clients.Where(c => !c.Children).ToList();
                    break;
            }

            if (partner.Sign != "Любой")
                clients = clients.Where(c => c.Sign == partner.Sign).ToList();

            foreach (var client in clients)
            {
                client.Sign = _context.Zodiac.FindAsync(client.Sign).Result.Name;
            }
            ViewBag.Partner = partner;
            return View(clients);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string id, [Bind("CodePartner,LoginClient,Sign,Gender,Age,Adress,Education,Height,Weight,Children,Hobbies")] Partner partnerInfo)
        {
            ViewBag.Id = id;

            if (partnerInfo.Sign == "Любой") partnerInfo.Sign = null;
            else partnerInfo.Sign = _context.Zodiac.FirstAsync(z => z.Name == partnerInfo.Sign).Result.IDZodiac;

            if (partnerInfo.Adress != null)
                partnerInfo.Adress = partnerInfo.Adress.Trim();
            if (partnerInfo.Education != null)
                partnerInfo.Education = partnerInfo.Education.Trim();
            if (partnerInfo.Hobbies != null)
                partnerInfo.Hobbies = partnerInfo.Hobbies.Trim();

            ViewBag.Partner = partnerInfo;
            _context.Update(partnerInfo);
            await _context.SaveChangesAsync();

            var sign = await _context.Zodiac.OrderBy(n => n.Name).Select(z => z.Name).ToListAsync();
            sign.Insert(0, "Любой");
            ViewBag.Sign = new SelectList(sign);

            var clients = await _context.Client.Where(c => c.Login != id).ToListAsync();
            if (partnerInfo.Adress != null)
                clients = clients.Where(c => c.Adress != null && c.Adress.Contains(partnerInfo.Adress)).ToList();

            if (partnerInfo.Education != null)
                clients = clients.Where(c => c.Education != null && c.Education.Contains(partnerInfo.Education)).ToList();

            if (partnerInfo.Hobbies != null)
                clients = clients.Where(c => c.Interests != null && c.Interests.Contains(partnerInfo.Hobbies)).ToList();

            if (partnerInfo.Age != null)
                clients = clients.Where(c => c.Age >= partnerInfo.Age).ToList();

            if (partnerInfo.Height != null)
                clients = clients.Where(c => Convert.ToInt32(c.Height) >= Convert.ToInt32(partnerInfo.Height)).ToList();

            if (partnerInfo.Weight != null)
                clients = clients.Where(c => Convert.ToInt32(c.Weight) <= Convert.ToInt32(partnerInfo.Weight)).ToList();

            switch (partnerInfo.Gender)
            {
                case 'Ж':
                    clients=clients.Where(c => c.Gender == 'Ж').ToList();
                    break;
                case 'М':
                    clients=clients.Where(c => c.Gender == 'М').ToList();
                    break;
            }

            switch (partnerInfo.Children)
            {
                case true:
                    clients=clients.Where(c => c.Children).ToList();
                    break;
                case false:
                    clients = clients.Where(c => !c.Children).ToList();
                    break;
            }

            if (partnerInfo.Sign != null)
                clients = clients.Where(c => c.Sign == partnerInfo.Sign).ToList();

            foreach (var client in clients)
            {
                client.Sign = _context.Zodiac.FindAsync(client.Sign).Result.Name;
            }

            if (partnerInfo.Sign != null)
                partnerInfo.Sign = _context.Zodiac.FindAsync(partnerInfo.Sign).Result.Name;
            else partnerInfo.Sign = "Любой";

            return View(clients);
        }

        // GET: Clients/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(string id, string idClient)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Login == id);
            if (client == null)
            {
                return NotFound();
            }

            client.Sign = _context.Zodiac.FindAsync(client.Sign).Result.Name;

            ViewBag.Children = (client.Children == true) ? "Да" : "Нет";

            ViewBag.Id = idClient;

            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Login == id);
            if (client == null)
            {
                return NotFound();
            }

            client.Sign = _context.Zodiac.FindAsync(client.Sign).Result.Name;

            ViewBag.Children = (client.Children == true) ? "Да" : "Нет";

            return View(client);
        }

        // GET: Clients/Create
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Login = false;
            ViewBag.Gender = false;
            ViewBag.Img = false;
            ViewBag.Sign = new SelectList(await _context.Zodiac.OrderBy(n => n.Name).Select(z => z.Name).ToListAsync());
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Login,Sign,Password,Image,FIO,Gender,Birthday,Adress,Contacts,Education,Height,Weight,Children,Interests,Note")] Client client, IFormFile uploadedFile)
        {
            ViewBag.Login = false;
            ViewBag.Img = false;
            ViewBag.Gender = false;
            if (client.Gender == null)
                ViewBag.Gender = true;
            else
            {
                if (uploadedFile == null)
                    ViewBag.Img = true;
                else
                {
                    if (ModelState.IsValid)
                    {
                        ViewBag.Login = true;
                        if (await _context.Client.FindAsync(client.Login) == null)
                        {
                            client.Sign = _context.Zodiac.FirstAsync(n => n.Name == client.Sign).Result.IDZodiac;

                            // путь к папке с фотографиями
                            string path = "/Images/" + uploadedFile.FileName;
                            // сохраняем файл в папку Images
                            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                            {
                                await uploadedFile.CopyToAsync(fileStream);
                            }
                            //сохраняем ссылку на фото
                            client.Image = uploadedFile.FileName;

                            client.Age = DateTime.Now.Year - client.Birthday.Year;

                            _context.Add(client);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("LogIn", "Home");
                        }
                    }
                }
            }
            ViewBag.Sign = new SelectList(await _context.Zodiac.OrderBy(n => n.Name).Select(z => z.Name).ToListAsync());
            return View(client);
        }

        // GET: Clients/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            ViewBag.Sign = new SelectList(await _context.Zodiac.OrderBy(n=>n.Name).Select(z=>z.Name).ToListAsync());
            client.Sign = _context.Zodiac.FindAsync(client.Sign).Result.Name;
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Login,Sign,Password,FIO,Image,Gender,Birthday,Adress,Contacts,Education,Height,Weight,Children,Interests,Note")] Client client, IFormFile uploadedFile)
        {
            if (id != client.Login)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    client.Sign = _context.Zodiac.FirstAsync(n => n.Name == client.Sign).Result.IDZodiac;

                    if (uploadedFile != null)
                    {
                        // путь к папке с фотографиями
                        string path = "/Images/" + uploadedFile.FileName;
                        // сохраняем файл в папку Images
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        //сохраняем ссылку на фото
                        client.Image = uploadedFile.FileName;
                    }

                    client.Age = DateTime.Now.Year - client.Birthday.Year;

                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Login))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Profile", new { id = client.Login });
            }

            ViewBag.Sign = new SelectList(await _context.Zodiac.OrderBy(n => n.Name).Select(z => z.Name).ToListAsync());
            return View(client);
        }

        private bool ClientExists(string id)
        {
            return _context.Client.Any(e => e.Login == id);
        }
    }
}
