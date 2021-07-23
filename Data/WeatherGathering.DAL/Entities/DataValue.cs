using System;
using WeatherGathering.DAL.Entities.Base;

namespace WeatherGathering.DAL
{
    // Класс, представляющий данные, получаемые из источника данных
    public class DataValue : Entity
    {
        // Данные времени в формате UTC
        public DateTimeOffset Time { get; set; } = DateTime.Now; 

        public string Value { get; set; }

        // Ссылка, откуда получены данные (источник)
        public DataSource Source { get; set; }

        // Флаг, показывающий исправность или неисправность ссылки
        public bool IsFaulty { get; set; } 
    }
}
