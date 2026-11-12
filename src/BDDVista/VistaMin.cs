using System;
using Gtk;
using Gio;
using System.Threading.Tasks;
using GLib;

//Clase que usa al minero para que
//en la vista principal se pueda
//llamar.

public class VistaMinero {
    //La vista del minero
    public Box VentanaMin {
	get;
	set;
    }
    //Dice cuando el minero termina
    public System.Action terminar {
		get;
		set;
	}
    //Crea la vista del minero en la bdd
    public VistaMinero(Window ventana) {
	VentanaMin = Box.New(Orientation.Vertical, 15);
	VentanaMin.MarginTop = VentanaMin.MarginBottom = 25;
	VentanaMin.MarginStart = VentanaMin.MarginEnd = 25;
	Box caja = Box.New(Orientation.Horizontal, 10);
	var ruta = Entry.New();
	ruta.Buffer.Text = "Aqui va una ruta a un directorio.";
	ruta.Hexpand = true;
	Button buscar = Button.NewWithLabel("Examinar....");
	buscar.OnClicked += async (s, args) => {
	    var dialogo = FileDialog.New();
	    dialogo.Title = "Selecciona un directorio.";
	    try {
		var directorio = await dialogo.SelectFolderAsync(ventana);
		if (directorio != null) {
		    ruta.Buffer.Text = directorio.GetPath();
		}
	    } catch {
		dialogo.Title = "Se debe seleccionar un directorio para buscar y llenar la BDD.";
	    }
	};
	caja.Append(ruta);
	caja.Append(buscar);
	var estado = Label.New("Buscando..");
	var minar = Button.NewWithLabel("Buscar musica.");
	minar.OnClicked += async (s, args) => {
	    minar.Sensitive = false;
	    buscar.Sensitive = false;
	    string rutaFinal = ruta.Buffer.Text;
	    estado.SetText($"Buscando en: {rutaFinal}");
	    await System.Threading.Tasks.Task.Run(() => {
		albumI album = new Album("rolas.db");
		performerI performer = new Performer("rolas.db");
		rolaI rola = new Rola("rolas.db");
		var minero = new Minero(album, performer, rola);
		minero.buscarDirectorio(rutaFinal, (mensaje) => {
		    GLib.Functions.IdleAdd(0, () => {
			estado.SetText(mensaje);
			return false;
		    });
		});
	    });
		terminar?.Invoke();
	    estado.SetText("Completado!");
	    minar.Sensitive = true;
	    buscar.Sensitive = true;
	};
	VentanaMin.Append(caja);
	VentanaMin.Append(estado);
	VentanaMin.Append(minar);
    }
}
