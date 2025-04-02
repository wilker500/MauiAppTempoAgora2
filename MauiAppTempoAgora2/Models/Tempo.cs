using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppTempoAgora2.Models
{
    public class Tempo
    {
        public double? lon { get; set; }
        public double? lat { get; set; }
        public double? temp_min { get; set; }
        public double? temp_max { get; set; }
        public int? visibility { get; set; }      // Visibilidade
        public double? speed { get; set; }        // Velocidade do Vento
        public string? main { get; set; }
        public string? description { get; set; }  // Descrição do Clima
        public string? sunrise { get; set; }
        public string? sunset { get; set; }

    }

}
