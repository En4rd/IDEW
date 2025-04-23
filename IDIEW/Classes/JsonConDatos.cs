using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDIEW.Classes
{
    public class JsonConDatos
    {
        public string Archivo { get; set; }
        public Classes.HDSData Datos { get; set; }
        public string ImagenPictograma { get; set; } // ruta del pictograma seleccionad
        public List<string> PictogramasSeleccionados { get; set; }  // Agregado para almacenar los pictogramas seleccionados
    }
}
