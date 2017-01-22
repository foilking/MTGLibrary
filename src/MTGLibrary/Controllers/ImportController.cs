using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using MTGLibrary.Models.Import;
using MTGLibrary.Data;
using AutoMapper;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MTGLibrary.Controllers
{
    public class ImportController : Controller
    {
        private string _connectionString;
        private readonly IMapper _mapper;

        public ImportController(IConfiguration config, IMapper mapper)
        {
            _connectionString = config.GetConnectionString("CardDB");
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            // Import data from MTG JSON
            var url = @"https://mtgjson.com/json/AllSetsArray-x.json";
            using (var httpClient = new HttpClient())
            {
                var rawData = await httpClient.GetStringAsync(url);
                ImportSet[] importSets = Newtonsoft.Json.JsonConvert.DeserializeObject<ImportSet[]>(rawData);

                ImportSets(importSets);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BySet(string code)
        {
            var url = string.Format("https://mtgjson.com/json/{0}-x.json", code);
            using (var httpClient = new HttpClient())
            {
                var rawData = await httpClient.GetStringAsync(url);
                ImportSet importSet = Newtonsoft.Json.JsonConvert.DeserializeObject<ImportSet>(rawData);

                ImportSets(new ImportSet[] { importSet });
            }
            return View();
        }

        private void ImportSets(ImportSet[] importSets)
        {
            // Add Data to the Database
            using (var worker = new UnitOfWork(_connectionString))
            {
                var rarities = worker.RarityRepository.GetAll();
                foreach (var importSet in importSets)
                {
                    // Import the set first
                    var mappedSet = _mapper.Map<Data.Models.Set>(importSet);
                    var set = worker.SetRepository.Find(mappedSet.Code);
                    if (set == null)
                    {
                        worker.SetRepository.Add(mappedSet);
                    }
                    else
                    {
                        mappedSet.Id = set.Id;
                        worker.SetRepository.Update(mappedSet);
                    }
                    worker.Commit();
                    set = worker.SetRepository.Find(mappedSet.Code);

                    // Then import all cards in the set
                    foreach (var importCard in importSet.cards)
                    {
                        var mappedCard = _mapper.Map<Data.Models.Card>(importCard);
                        mappedCard.Rarity = rarities.FirstOrDefault(r => r.Name == importCard.rarity);
                        mappedCard.Set = set;
                        Data.Models.Card card = null;
                        if (mappedCard.MultiverseId > 0)
                        {
                            card = worker.CardRepository.Find(multiverseId: mappedCard.MultiverseId).FirstOrDefault();
                        }
                        else
                        {
                            card = worker.CardRepository.Find(name: mappedCard.Name, setId: mappedCard.Set.Id).FirstOrDefault();
                        }
                        if (card == null)
                        {
                            worker.CardRepository.Add(mappedCard);
                        }
                        else
                        {
                            mappedCard.Id = card.Id;
                            worker.CardRepository.Update(mappedCard);
                        }
                        worker.Commit();

                    }
                }
            }
        }
    }
}
