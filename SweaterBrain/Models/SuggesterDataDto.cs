using System;
namespace SweaterBrain.Models
{
    public class SuggesterDataDto
    {
        public SuggesterDataDto(string temp, string feelsLike, string weight, string sweaterPath)
        {
            Temp = temp;
            FeelsLike = feelsLike;
            Weight = weight;
            SweaterPath = sweaterPath;
        }

        public string Temp { get; }
        public string FeelsLike { get; }
        public string Weight { get; }
        public string SweaterPath { get; }
    }
}
