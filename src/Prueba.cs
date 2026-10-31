using System;
using Gtk;

public class Prueba {
    public static void Main(String[] args) {
        var appl = Application.New("BddMusical.prueba", Gio.ApplicationFlags.FlagsNone);
	appl.OnActivate += (sender, e) => {
	    var window = ApplicationWindow.New((Application)sender);
	    window.Title = "BddMusicalPrueba";
	    window.DefaultWidth = 320;
	    window.DefaultHeight = 220;
	    var etq = Label.New("Hola mundo 2. ");
	    window.Child = etq;
	    window.Show();
	};
	appl.Run(0, null);
    }
}
