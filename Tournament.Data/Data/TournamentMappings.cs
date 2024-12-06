using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Dto;

namespace Tournament.Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings() 
        {
            CreateMap<TournamentDetails, TournamentIdDto>();
            CreateMap<TournamentUpdateDto, TournamentDetails>();
            CreateMap<TournamentCreateDto, TournamentDetails>();
            CreateMap<TournamentIdDto, TournamentUpdateDto>();
            CreateMap<TournamentIdDto, TournamentDetails>();

            CreateMap<Game, GameIdDto>();
            CreateMap<GameUpdateDto, Game>();
            CreateMap<GameCreateDto, Game>();
            CreateMap<GameIdDto, GameUpdateDto>();
            CreateMap<GameIdDto, Game>();

        }
    }
}
