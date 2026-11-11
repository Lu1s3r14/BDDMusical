using System;
using Gtk;

// Clase prueba de interfaz

public class BDDMusicalV {
    public static void Main(String[] args) {
	var appl = Application.New("BDDMusical.prueba", Gio.ApplicationFlags.FlagsNone);
	appl.OnActivate += (sender, e) => {
	    var window = ApplicationWindow.New((Application) sender);
	    window.Title = "BDDMusical";
	    window.SetDefaultSize(600, 500);
		var botonMinero = new VistaMinero(window);
		window.SetChild(botonMinero.VentanaMin);
		window.Present();
	};
	appl.Run(0, null);
    }
}
