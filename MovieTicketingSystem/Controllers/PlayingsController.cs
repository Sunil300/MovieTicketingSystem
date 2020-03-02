using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTicketingSystem.Data;
using MovieTicketingSystem.Models;
using MovieTicketingSystem.Models.ViewModels;
namespace MovieTicketingSystem.Controllers
{
    public class PlayingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Playings
        public async Task<IActionResult> Index(int? id, DateTime searchDate)
        {
            //var applicationDbContext = _context.Playing.Include(p => p.Movie).Include(p => p.Room).Include(p => p.TimeSlot);

            IQueryable<Playing> playings = from p in _context.Playing.Include(p => p.Movie).Include(p => p.Room).Include(p => p.TimeSlot) orderby p.PlayingDate select p ;

            if (id != null)
            {
                playings = playings.Where(s => s.MovieId.Equals(id));
            }
            if (searchDate != DateTime.MinValue)
            {
                playings = playings.Where(x => x.PlayingDate.Equals(searchDate));
            }
            var playingsVM = new PlayingsViewModel
            {
                Playings = await playings.ToListAsync(),
                SearchDate = DateTime.Today
            };

            return View(playingsVM);
        }

        // GET: Playings/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playing = await _context.Playing
                .Include(p => p.Movie)
                .Include(p => p.Room)
                .Include(p => p.TimeSlot)
                .FirstOrDefaultAsync(m => m.PlayingId == id);
            if (playing == null)
            {
                return NotFound();
            }

            var tickets = from t in _context.Ticket select t;

            if (id != null)
            {
                tickets = tickets.Where(s => s.PlayingId.Equals(id));
            }

            List<System.String> UnionList = new List<string>();
            String[] seats = null;
            for (int i = 0; i < tickets.Count(); i++)
            {
                seats = tickets.ToList<Ticket>().ElementAt(i).SeatNo.Split(" ");
                List<System.String> lists = new List<System.String>(seats);
                UnionList.AddRange(lists);
            }
            var playingSeatsVM = new PlayingSeatsViewModel
            {
                Playing = playing,
                Seats = UnionList
            };
            return View(playingSeatsVM);
        }

        // GET: Playings/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "Title");
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomNo");
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlot, "TimeSlotId", "TimeFrom");
            return View();
        }

        // POST: Playings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayingId,TimeSlotId,PlayingDate,MovieId,RoomId")] Playing playing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "MovieId", playing.MovieId);
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", playing.RoomId);
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlot, "TimeSlotId", "TimeSlotId", playing.TimeSlotId);
            return View(playing);
        }
        // GET: Playings/Create
        public IActionResult AutoCreate()
        {
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "Title");
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomNo");
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlot, "TimeSlotId", "TimeFrom");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AutoCreate([Bind("PlayingDateFrom,PlayingDateTo,MovieIds")] AutoPlayingsViewModel autoPlayingsViewModel)
        {
            foreach(int item in autoPlayingsViewModel.MovieIds)
            {
                if (ModelState.IsValid)
                {
                    Playing playing = new Playing
                    {
                        MovieId = item,
                        PlayingDate = autoPlayingsViewModel.PlayingDateFrom,
                        RoomId = _context.Room.ToList().ElementAt(item).RoomId,
                        TimeSlotId = _context.TimeSlot.ToList().ElementAt(item).TimeSlotId
                    
                    };
                    _context.Add(playing);
                    await _context.SaveChangesAsync();

                }
            }

            //ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "MovieId", playing.MovieId);
            //ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", playing.RoomId);
            //ViewData["TimeSlotId"] = new SelectList(_context.TimeSlot, "TimeSlotId", "TimeSlotId", playing.TimeSlotId);
            return RedirectToAction(nameof(Index));
        }
        // GET: Playings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playing = await _context.Playing.FindAsync(id);
            if (playing == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "Title", playing.MovieId);
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomNo", playing.RoomId);
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlot, "TimeSlotId", "TimeFrom", playing.TimeSlotId);
            return View(playing);
        }

        // POST: Playings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayingId,TimeSlotId,PlayingDate,MovieId,RoomId")] Playing playing)
        {
            if (id != playing.PlayingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayingExists(playing.PlayingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "MovieId", playing.MovieId);
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", playing.RoomId);
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlot, "TimeSlotId", "TimeSlotId", playing.TimeSlotId);
            return View(playing);
        }

        // GET: Playings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playing = await _context.Playing
                .Include(p => p.Movie)
                .Include(p => p.Room)
                .Include(p => p.TimeSlot)
                .FirstOrDefaultAsync(m => m.PlayingId == id);
            if (playing == null)
            {
                return NotFound();
            }

            return View(playing);
        }

        // POST: Playings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playing = await _context.Playing.FindAsync(id);
            _context.Playing.Remove(playing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayingExists(int id)
        {
            return _context.Playing.Any(e => e.PlayingId == id);
        }
    }
}
