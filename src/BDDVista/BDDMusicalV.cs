using System;
using Gtk;

// Clase prueba de interfaz

public class BDDMusicalV {
    public static void Main(String[] args) {
	var appl = Application.New("BDDMusical.prueba", Gio.ApplicationFlags.FlagsNone);
	appl.OnActivate += (sender, e) => {
	    var window = ApplicationWindow.New((Application) sender);
	    window.Title = "BDDMusical";
	    window.SetDefaultSize(1000, 650);
	    var tamanio = Box.New(Orientation.Vertical, 0);
	    var botonMinero = new VistaMinero(window);
	    var contenido = new VistaTabla();
	    //botonMinero.VentanaMin.WidthRequest = 300;
	    tamanio.Append(botonMinero.VentanaMin);
	    tamanio.Append(Separator.New(Orientation.Horizontal));
	    tamanio.Append(contenido.barraVista);
	    window.SetChild(tamanio);
	    window.Present();
	};
	appl.Run(args);
    }
}
