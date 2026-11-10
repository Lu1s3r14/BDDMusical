using System;
//using Gtk;

public class Prueba {
    public static void Main(String[] args) {
	//      var appl = Application.New("BddMusical.prueba", Gio.ApplicationFlags.FlagsNone);
	//appl.OnActivate += (sender, e) => {
	//  var window = ApplicationWindow.New((Application)sender);
	//  window.Title = "BddMusicalPrueba";
	//  window.DefaultWidth = 320;
	//  window.DefaultHeight = 220;
	//  var etq = Label.New("Hola mundo 2. ");
	//  window.Child = etq;
	//  window.Show();
	//
	//appl.Run(0, null);
	//}
	Console.WriteLine("Prueba minero.");
	string ruta;
	if (args.Length>0) {
	    ruta = args[0];
	} else {
	    Console.WriteLine("Uso: ./build/Prueba ruta/de/prueba");
	    return;
	}
	string bdd = "rolas.db";
	albumI album = new Album(bdd);
	performerI performer = new Performer(bdd);
	rolaI rola = new Rola(bdd);
	var minero = new Minero(album, performer, rola);
	Console.WriteLine($"Búsqueda en el directorio {ruta}\n");
	minero.buscarDirectorio(ruta, (mensaje) => {
	    Console.WriteLine(mensaje);
	});
	Console.WriteLine("Todos los álbums:");
	var todosA = album.mostrarTodo();
	foreach (var al in todosA) {
	    Console.WriteLine($"id del álb: {al.idAlbum} | nombre: {al.nombreAlbum} | fecha: {al.fechaAlbum}");
	}
	Console.WriteLine("----------------------------------");
	Console.WriteLine();
	Console.WriteLine("Todos los performers: ");
	var todosP = performer.mostrarTodoP();
	foreach (var per in todosP) {
	    Console.WriteLine($"id del perf: {per.idPerf} | tipo: {per.idTyp} | nombre: {per.nombrePerf}");
	}
	Console.WriteLine("-----------------------------------");
	Console.WriteLine();
	Console.WriteLine("Todas las rolas: ");
	var todosR = rola.mostrarTodoR();
	foreach (var rol in todosR) {
	    Console.WriteLine("--------------------------------");
	    Console.WriteLine($"id de la rola: {rol.idRola} | titulo: {rol.tituloRola} | numPista: {rol.pistaRola} | genero: {rol.generoRola}");
	    Console.WriteLine($"id del álbum al que pertenecen: {rol.idRalbum}. con id del performer: {rol.idRperf}");
	    Console.WriteLine($"archivo fisico leido: {rol.rutaRola}");
	    Console.WriteLine("--------------------------------");
	}
    }
}
