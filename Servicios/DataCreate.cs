using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Servicios
{
  public class DataCreate
    {

        public DataCreate()
        {
                
        }

        public static string  CrearDocumento()
        {
            string path = @"C:\Users\Casa\Documents\Cursos\ProyectosIng\reporte.txt";
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (FileStream oFS = File.Create(path)) 
                {
                    Byte[] cuerpoInfo = new UTF8Encoding(true).GetBytes(Verificador());
                    oFS.Write(cuerpoInfo, 0, cuerpoInfo.Length);
                    return path;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string Verificador()
        {
            return "llega acá";
        }
    }
}
