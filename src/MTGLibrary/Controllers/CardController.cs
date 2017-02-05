using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MTGLibrary.Data;
using MTGLibrary.ViewModels;
using MTGLibrary.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MTGLibrary.Controllers
{
    public class CardController : Controller
    {
        private string _connectionString;
        private readonly IMapper _mapper;

        public CardController(IConfiguration config, IMapper mapper)
        {
            _connectionString = config.GetConnectionString("CardDB");
            _mapper = mapper;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(string code, string number)
        {
            using (var worker = new UnitOfWork(_connectionString))
            {
                var cards = worker.CardRepository.Find(code: code, number: number);
                if (cards.Any() && cards.Count() == 1)
                {
                    var card = cards.FirstOrDefault();
                    var viewModel = new CardDetailsViewModel();
                    viewModel.Card = _mapper.Map<Card>(card);
                    return View(viewModel);
                }
                else
                {
                    var searchQuery = string.Format("set:{0} number={1}", code, number);
                    return RedirectToAction("Search", new { searchQuery = searchQuery });
                }
            }
        }

        public IActionResult Search(string searchQuery)
        {

            return View();
        }
    }
}
