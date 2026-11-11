using System;
using Gtk;

// Clase prueba de interfaz

public class BDDMusicalV {
    public static void Main(String[] args) {
	var appl = Application.New("BDDMusical.prueba", Gio.ApplicationFlags.FlagsNone);
	appl.OnActivate += (sender, e) => {
	    var window = ApplicationWindow.New((Application) sender);
	    window.Title = "BDDMusical";
	    window.SetDefaultSize(500, 500);
	    var etq = Label.New("Hola mundo.");
	    window.Child = etq;
	    window.Show();
	};
	appl.Run(0, null);
    }
}
