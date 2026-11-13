using System;
using Gtk;

// Clase prueba de interfaz

public class BDDMusicalV {
    public static void Main(String[] args) {
	var appl = Application.New("BDDMusical.Plus", Gio.ApplicationFlags.FlagsNone);
	appl.OnActivate += (sender, e) => {
	    var window = ApplicationWindow.New((Application) sender);
	    window.Title = "BDDMusical";
	    window.SetDefaultSize(1200, 700);
	    string rolasdb = IniciaBDD.Inicia();
	    var tamanio = Box.New(Orientation.Vertical, 0);
	    var botonMinero = new VistaMinero(window, rolasdb);
	    var contenido = new VistaTabla();
		contenido.LlenarTabla(rolasdb);
		botonMinero.terminar = () => {
			contenido.LlenarTabla(rolasdb);
		};
	    tamanio.Append(botonMinero.VentanaMin);
		var cajaPrinc = Box.New(Orientation.Horizontal, 0);
		cajaPrinc.Hexpand = true;
		cajaPrinc.Vexpand = true;
		cajaPrinc.Append(contenido.Imagen);
		cajaPrinc.Append(contenido.barraVista);
	    tamanio.Append(cajaPrinc);
		tamanio.Append(contenido.Botones);
	    window.SetChild(tamanio);
	    window.Present();
	};
	appl.Run(args);
    }
}
