using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDIEW.Classes
{
    public class PuntoManagercs
    {
        public List<Tuple<PointF, int>> Puntos { get; private set; } = new List<Tuple<PointF, int>>();

        public void AgregarPunto(PointF punto)
        {
            Puntos.Add(Tuple.Create(punto, Puntos.Count + 1));
        }

        public void MoverPunto(int index, PointF nuevoPunto)
        {
            if (index >= 0 && index < Puntos.Count)
            {
                var actual = Puntos[index];
                Puntos[index] = Tuple.Create(nuevoPunto, actual.Item2);
            }
        }

        public bool EliminarPuntoCercano(PointF punto, float radio)
        {
            int index = BuscarIndiceCercano(punto, radio);
            if (index >= 0)
            {
                Puntos.RemoveAt(index);
                Renumerar();
                return true;
            }
            return false;
        }

        public bool EditarNumeroCercano(PointF punto, float radio, int nuevoNumero)
        {
            int index = BuscarIndiceCercano(punto, radio);
            if (index >= 0)
            {
                var actual = Puntos[index];
                Puntos[index] = Tuple.Create(actual.Item1, nuevoNumero);
                return true;
            }
            return false;
        }

        public int BuscarIndiceCercano(PointF punto, float radio)
        {
            for (int i = 0; i < Puntos.Count; i++)
            {
                var pt = Puntos[i].Item1;
                float dx = pt.X - punto.X;
                float dy = pt.Y - punto.Y;
                if (Math.Sqrt(dx * dx + dy * dy) <= radio)
                    return i;
            }
            return -1;
        }

        public void Renumerar()
        {
            for (int i = 0; i < Puntos.Count; i++)
            {
                var punto = Puntos[i].Item1;
                Puntos[i] = Tuple.Create(punto, i + 1);
            }
        }

        public void Limpiar() => Puntos.Clear();
    }
}


