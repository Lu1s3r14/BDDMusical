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
	var minero = new Minero(album);
	Console.WriteLine($"Búsqueda en el directorio {ruta}\n");
	minero.buscarDirectorio(ruta, (mensaje) => {
	    Console.WriteLine(mensaje);
	});
	Console.WriteLine("Total de albums en la bdd: ");
	var todo = album.mostrarTodo();
	foreach (var alb in todo) {
	    Console.WriteLine($"ID: {alb.idAlbum} , {alb.nombreAlbum} , {alb.fechaAlbum} | Ruta: {alb.rutaAlbum}"); 
	}	
    }
}
