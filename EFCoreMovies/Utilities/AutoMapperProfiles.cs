﻿using AutoMapper;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCoreMovies.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Actor,ActorDTO>();

            CreateMap<Cinema,CinemaDTO>()
                .ForMember(dto => dto.longitude, ent => ent.MapFrom(p => p.Location.X))
                .ForMember(dto => dto.latitude, ent => ent.MapFrom(p => p.Location.Y));

            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreCreationDTO, Genre>();

            CreateMap<Movie, MovieDTO>()
                .ForMember(dto => dto.Genres, ent => ent.MapFrom
                (p => p.Genres.OrderByDescending(g => g.Name).Where(g => !g.Name.Contains("m"))))
                .ForMember(dto => dto.Cinemas, ent => ent.MapFrom
                (p => p.CinemaHalls.OrderByDescending(ch => ch.Cinema.Id).Select(c => c.Cinema)))
                .ForMember(dto => dto.Actors, ent => ent.MapFrom(p => p.MoviesActors.Select(m => m.Actor)));

            var geometryFactor = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CreateMap<CinemaCreationDTO,Cinema>()
                .ForMember(ent => ent.Location, dto => dto.MapFrom
                (prop => geometryFactor.CreatePoint(new Coordinate(prop.Longitue, prop.Latitue))));

            CreateMap<CinemaOfferCreationDTO, CinemaOffer>();
            CreateMap<CinemaHallCreationDTO,CinemaHall>();

            CreateMap<MovieCreationDTO, Movie>()
                .ForMember(ent => ent.Genres, 
                dto => dto.MapFrom(prop => prop.GenresIds.Select(id => new Genre() { Id = id})))
                .ForMember(ent => ent.CinemaHalls, 
                dto => dto.MapFrom(prop => prop.CinemaHallsIds.Select(id => new CinemaHall() { Id = id})));

            CreateMap<MovieActorCreationDTO, MovieActor>();

            CreateMap<ActorCreationDTO, Actor>();
        }
    }
}
