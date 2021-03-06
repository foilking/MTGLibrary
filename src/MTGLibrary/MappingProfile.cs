﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using MTGLibrary.Data;
using MTGLibrary.Data.Models;
using MTGLibrary.Models.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTGLibrary
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ImportSet, Set>();
            CreateMap<ImportCard, Card>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Rarity, opt => opt.Ignore());
            CreateMap<ImportRuling, Ruling>();

            CreateMap<Set, Models.Set>();
            CreateMap<Rarity, Models.Rarity>();
            CreateMap<Card, Models.Card>();
        }
    }
}
