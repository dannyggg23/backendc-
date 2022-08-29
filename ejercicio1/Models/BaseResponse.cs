namespace ejercicio1.Models
{


    public class Retorno
    {
        /// <summary>
        /// Si el proceso es correcto posee un valor de True de lo contrario False y en la variable retorno posee la descripción del error
        /// </summary>
        public bool procesoCorrecto { get; set; }

        /// <summary>
        /// Retorna los objetos dependiendo del servicio y en caso de haber error retona el texto del mismo
        /// </summary>
        public object retorno { get; set; }

        public Retorno()
        {
            //msm = "";
            procesoCorrecto = false;
        }


    }
}
    
