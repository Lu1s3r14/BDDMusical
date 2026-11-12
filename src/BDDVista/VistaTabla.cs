using System;
using Gtk;
using Gio;
using GObject;

//Clase que dibuja las tablas de la bdd
//cuando ya se llenan

public class VistaTabla {
    //La barra para desplazar la tabla
    public ScrolledWindow barraVista {
	get;
	set;
    }
    //El contenido de la tabla
    public ColumnView contenidoTabla {
	get;
	set;
    }
    //Los datos que llenan la tabla.
    public Gio.ListStore datos {
	get;
	set;
    }
    

    //Crea la tabla con su contenido
    public VistaTabla() {
	barraVista = ScrolledWindow.New();
	barraVista.Hexpand = true;
	barraVista.Vexpand = true;
	barraVista.HasFrame = true;
	datos = Gio.ListStore.New(FilaTabla.GetGType());
	var seleccionar = SingleSelection.New(datos);
	contenidoTabla = ColumnView.New(seleccionar);
	contenidoTabla.AppendColumn(nuevaColumna("Título", r => r.Titulo));
	contenidoTabla.AppendColumn(nuevaColumna("Artista", r => r.Performer));
        contenidoTabla.AppendColumn(nuevaColumna("Álbum", r => r.Album));
        contenidoTabla.AppendColumn(nuevaColumna("Género", r => r.Genero));
	barraVista.SetChild(contenidoTabla);
    }

    //Crea una nueva columna para la tabla de la bdd
    public ColumnViewColumn nuevaColumna(string encabezado, Func<FilaTabla, string> elegir) {
	var nuevo = SignalListItemFactory.New();
	nuevo.OnSetup += (sender, args) => {
	    var item = (ListItem)args.Object;
	    var etq = Label.New("");
	    etq.Halign = Align.Start;
	    etq.MarginStart = 10;
	    item.SetChild(etq);
	};
	nuevo.OnBind += (sender, args) => {
	    var item = (ListItem)args.Object;
	    var etq = (Label)item.GetChild();
	    var fila = (FilaTabla)item.GetItem();
	    etq.SetText(elegir(fila));
	};
	var columna = ColumnViewColumn.New(encabezado, nuevo);
	columna.Expand = true;
	return columna;
    }
}
