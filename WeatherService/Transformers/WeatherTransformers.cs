using System;
using AutoMapper;
using WeatherService.Entities;

namespace WeatherService.Transformers
{
    public class WeatherTransformers
    {
        private readonly IMapper _mapper;

        public WeatherTransformers()
        {
            var config = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<CurrentWeatherResponse, WeatherDto>();
                    cfg.CreateMap<WeatherResponse, WeatherDto>()
                        .IncludeMembers(src => src.CurrentWeather);
                }
            );

            _mapper = new Mapper(config);
        }

        public WeatherDto TransformWeather(WeatherResponse? weatherResponse)
        {
            return _mapper.Map<WeatherDto>(weatherResponse);
        }
    }
}

