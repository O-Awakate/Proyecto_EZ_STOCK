using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaPresentacion.Utilidades;


namespace CapaPresentacion.Utilities
{
    public class Ciudades
    {
        public Dictionary<string, string[]> CiudadesPorEstado { get; private set; }
        public Ciudades()
        {
            // Crear el diccionario para almacenar las ciudades por estado
            CiudadesPorEstado = new Dictionary<string, string[]>();

            // Agregar las ciudades al diccionario
            CiudadesPorEstado.Add("Amazonas", new string[] { "La Esmeralda", "San Fernando de Atabapo", "Puerto Ayacucho", "Isla Ratón", "San Juan de Manapiare", "Maroa", "San Carlos de Río Negro" });
            CiudadesPorEstado.Add("Anzoátegui", new string[] { "Anaco", "Aragua de Barcelona", "Barcelona", "Clarines", "Onoto", "Valle de Guanape", "Cantaura", "San José de Guanipa", "Guanta", "San Mateo", "El Chaparro", "Pariaguán", "Mapire", "Puerto Píritu", "Píritu", "Boca de Uchire", "Santa Ana", "El Tigre", "Puerto La Cruz", "Lechería", "Ciudad Orinoco" });
            CiudadesPorEstado.Add("Apure", new string[] { "Achaguas", "Biruaca", "San Juan de Payara", "Bruzual", "Guasdualito", "Elorza", "San Fernando de Apure" });
            CiudadesPorEstado.Add("Aragua", new string[] { "Santa Rita", "San Mateo", "Camatagua", "Maracay", "El Limón", "Santa Cruz de Aragua", "Palo Negro", "Turmero", "Las Tejerías", "Ocumare de la Costa", "El Consejo", "La Victoria", "San Casimiro", "San Sebastián de los Reyes", "Cagua", "Colonia Tovar", "Barbacoas", "Villa de Cura" });
            CiudadesPorEstado.Add("Barinas", new string[] { "Sabaneta", "El Cantón", "Socopó", "Arismendi", "Barinas", "Barinitas", "Barrancas", "Santa Bárbara", "Obispos", "Ciudad Bolivia", "Libertad", "Ciudad de Nutrias" });
            CiudadesPorEstado.Add("Bolívar", new string[] { "Ciudad Piar", "Ciudad Bolívar", "Ciudad Guayana", "Caicara del Orinoco", "El Palmar", "El Callao", "Santa Elena de Uairén", "Upata", "Guasipati", "Maripa", "Tumeremo", "El Dorado" });
            CiudadesPorEstado.Add("Carabobo", new string[] { "Bejuma", "Güigüe", "Mariara", "Guacara", "Morón", "Tocuyito", "Los Guayos", "Miranda", "Montalbán", "Naguanagua", "Puerto Cabello", "San Diego", "San Joaquín", "Valencia" });
            CiudadesPorEstado.Add("Cojedes", new string[] { "Cojedes", "San Carlos", "El Baúl", "Macapo", "El Pao", "Libertad", "Las Vegas", "Tinaco", "Tinaquillo" });
            CiudadesPorEstado.Add("Delta Amacuro", new string[] { "Curiapo", "Sierra Imataca", "Pedernales", "Tucupita" });
            CiudadesPorEstado.Add("Distrito Capital", new string[] { "Caracas" });
            CiudadesPorEstado.Add("Falcón", new string[] { "San Juan de los Cayos", "San Luis", "Capatárida", "Punto Fijo", "La Vela de Coro", "Dabajuro", "Pedregal", "Pueblo Nuevo", "Churuguara", "Chichiriviche", "Jacura", "Santa Cruz de Los Taques", "Yaracal", "Mene de Mauroa", "Santa Ana de Coro", "Palmasola", "Píritu", "Mirimire", "La Cruz de Taratara", "Tucacas", "Tocópero", "Santa Cruz de Bucaral", "Urumaco", "Puerto Cumarebo", "Cabura" });
            CiudadesPorEstado.Add("Guárico", new string[] { "Camaguán", "Chaguaramas", "El Socorro", "Calabozo", "Tucupido", "Altagracia de Orituco", "Las Mercedes", "El Sombrero", "Valle de La Pascua", "Ortiz", "Guayabal", "San José de Guaribe", "Santa María de Ipire", "Zaraza", "San Juan de los Morros" });
            CiudadesPorEstado.Add("Lara", new string[] { "Sanare", "Duaca", "Barquisimeto", "Quibor", "El Tocuyo", "Cabudare", "Sarare", "Carora", "Siquisique" });
            CiudadesPorEstado.Add("La Guaira", new string[] { "La Guaira" });
            CiudadesPorEstado.Add("Mérida", new string[] { "El Vigía", "La Azulita", "Santa Cruz de Mora", "Aricagua", "Canaguá", "Ejido", "Tucaní", "Santo Domingo", "Guaraque", "Arapuey", "Mérida", "Timotes", "Santa Elena de Arenales", "Santa María de Caparo", "Pueblo Llano", "Mucuchíes", "Bailadores", "Tabay", "Zea", "Lagunillas", "Nueva Bolivia", "Torondoy", "Tovar" });
            CiudadesPorEstado.Add("Miranda", new string[] { "Caucagua", "San José de Barlovento", "Baruta", "San Francisco de Yare", "Higuerote", "Mamporal", "Carrizal", "Chacao", "Charallave", "El Hatillo", "Los Teques", "Cúpira", "Santa Teresa del Tuy", "Ocumare del Tuy", "San Antonio de los Altos", "Páez", "Río Chico", "Santa Lucía", "Guarenas", "Petare", "San Diego", "Cúa", "Guatire" });
            CiudadesPorEstado.Add("Monagas", new string[] { "San Antonio de Capayacuar", "Aguasay", "Caripito", "Caripe", "Caicara de Maturín", "Temblador", "Maturín", "Aragua de Maturín", "Quiriquire", "Santa Bárbara", "Barrancas del Orinoco", "Uracoa", "Punta de Mata" });
            CiudadesPorEstado.Add("Nueva Esparta", new string[] { "Paraguachí", "San Juan Bautista", "La Asunción", "El Valle", "Santa Ana", "Boca de Río", "Pampatar", "Juan Griego", "Porlamar", "Punta de Piedras", "San Pedro de Coche" });
            CiudadesPorEstado.Add("Portuguesa", new string[] { "Agua Blanca", "Araure", "Píritu", "Guanare", "Guanarito", "Chabasquén", "Ospino", "Acarigua", "Papelón", "Boconoíto", "San Rafael de Onoto", "El Playón", "Biscucuy", "Villa Bruzual" });
            CiudadesPorEstado.Add("Sucre", new string[] { "Casanay", "San José de Areocuar", "Río Caribe", "El Pilar", "Carúpano", "Marigüitar", "Yaguaraparo", "Araya", "Tunapuy", "Irapa", "San Antonio del Golfo", "Cumanacoa", "La Cruz de Taratara", "Guiria", "Cariaco", "Cumaná" });
            CiudadesPorEstado.Add("Táchira", new string[] { "Cordero", "Las Mesas", "Colón", "San Antonio del Táchira", "Táriba", "Santa Ana de Táchira", "San Rafael del Piñal", "San José de Bolívar", "La Fría", "Palmira", "Capacho Nuevo", "La Grita", "El Cobre", "Rubio", "Capacho Viejo", "Abejales", "Lobatera", "Michelena", "Coloncito", "Ureña", "Delicias", "La Tendida", "San Simón", "Queniquea", "San Josecito", "Pregonero", "San Cristóbal", "Umuquena" });
            CiudadesPorEstado.Add("Trujillo", new string[] { "Santa Isabel", "Boconó", "Sabana Grande", "Chejendé", "Carache", "Carvajal", "Escuque", "Campo Elías", "Santa Apolonia", "El Paradero", "El Dividive", "Monte Carmelo", "Motatán", "Pampán", "Pampanito", "Betijoque", "Sabana de Mendoza", "Trujillo", "La Quebrada", "Valera" });
            CiudadesPorEstado.Add("Yaracuy", new string[] { "San Pablo", "Aroa", "Chivacoa", "Cocorote", "Independencia", "Sabana de Parra", "Boraure", "Yumare", "Nirgua", "Yaritagua", "San Felipe", "Guama", "Urachiche", "Farriar" });
            CiudadesPorEstado.Add("Zulia", new string[] { "El Toro", "San Timoteo", "Cabimas", "Encontrados", "San Carlos del Zulia", "Pueblo Nuevo-El Chivo", "Sinamaica", "La Concepción", "Casigua El Cubo", "Concepción", "Ciudad Ojeda", "Machiques", "San Rafael del Moján", "Maracaibo", "Los Puertos de Altagracia", "La Villa del Rosario", "San Francisco", "Santa Rita", "Tía Juana", "Bobures", "Bachaquero" });
           
            // Ejemplo de cómo acceder a las ciudades de un estado específico
            string[] ciudadesAnzoategui = CiudadesPorEstado["Anzoátegui"];

            // Imprimir las ciudades de Anzoátegui
            Console.WriteLine("Ciudades de Anzoátegui:");
            foreach (var ciudad in ciudadesAnzoategui)
            {
                Console.WriteLine(ciudad);
            }
        }
    }
}
