using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatingService.Data;
using DatingService.Models;

namespace DatingService.Controllers
{
    public class CouplesController : Controller
    {
        private readonly DatingServiceContext _context;

        public CouplesController(DatingServiceContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Couples
        public async Task<IActionResult> Index(string id)
        {
            ViewBag.Id = id;
            var clients = await _context.Client.FromSqlRaw($"exec PartnerList @loginClient='{id}'").ToListAsync();
            foreach(var client in clients)
            {
                client.Sign = _context.Zodiac.FindAsync(client.Sign).Result.Name;
            }
            return View(clients);
        }

        [HttpGet]
        // GET: Couples/Details/5
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

        // GET: Couples/Create
        [HttpGet]
        public async Task<IActionResult> Create(string idClient, string idPartner)
        {
            if (await _context.Couple.FirstOrDefaultAsync(par => par.LoginClient == idClient && par.LoginPartner == idPartner)==null)
            {
                var couple = new Couple();
                couple.LoginClient = idClient;
                couple.LoginPartner = idPartner;
                _context.Add(couple);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Clients", new { id = idClient });
        }

        // POST: Couples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDCouple,LoginClient,LoginPartner,Note")] Couple couple)
        {
            if (ModelState.IsValid)
            {
                _context.Add(couple);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(couple);
        }

        private bool CoupleExists(int id)
        {
            return _context.Couple.Any(e => e.IDCouple == id);
        }
    }
}
