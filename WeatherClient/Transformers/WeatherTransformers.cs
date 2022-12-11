using System;
using AutoMapper;
using WeatherClient.Entities;
using WeatherClient.Utils;

namespace WeatherClient.Transformers
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
                        .ForMember(
                            dest => dest.Summary,
                            opt => opt.MapFrom(src => WeatherUtils.GetWeatherSummary(src.CurrentWeather.Temperature))
                        )
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

