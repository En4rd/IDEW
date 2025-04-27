using Newtonsoft.Json;
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
        public Datos Datos { get; set; }
        public List<string> PictogramasSeleccionados { get; set; } = new List<string>();
        public string ImagenPictograma { get; set; }
    }

    public class Datos
    {
        [JsonProperty("Fabricante")]
        public string Fabricante { get; set; }

        [JsonProperty("Nombre del producto")]
        public string NombreDelProducto { get; set; }

        [JsonProperty("Uso")]
        public string Uso { get; set; }

        [JsonProperty("Fecha de actualización de HDS")]
        public string FechaDeActualizacionDeHDS { get; set; }

        [JsonProperty("HDS en español")]
        public string HDSEnEspanol { get; set; }

        [JsonProperty("Temperatura de inflamación")]
        public string TemperaturaDeInflamacion { get; set; }

        [JsonProperty("Descripción")]
        public string Descripcion { get; set; }

        [JsonProperty("Equipo de protección personal")]
        public string EquipoDeProteccionPersonal { get; set; }

        [JsonProperty("Incompatibilidad")]
        public string Incompatibilidad { get; set; }

        [JsonProperty("Condiciones a evitar")]
        public string CondicionesAEvitar { get; set; }

        [JsonProperty("No. CAS")]
        public string NoCAS { get; set; }

        [JsonProperty("Limite de exposicion")]
        public string LimiteDeExposicion { get; set; }

        [JsonProperty("Características físico-químicas")]
        public string CaracteristicasFisicoQuimicas { get; set; }

        [JsonProperty("Medidas sanitarias")]
        public string MedidasSanitarias { get; set; }

        [JsonProperty("Síntomas")]
        public string Sintomas { get; set; }

        [JsonProperty("Primeros auxilios")]
        public string PrimerosAuxilios { get; set; }

        [JsonProperty("Órganos afectados")]
        public string OrganosAfectados { get; set; }

        // Extras
        public string NoHDS { get; set; }
        public string Area { get; set; } = "General";
        public string Cantidad { get; set; } = "N/A";

    }
}
