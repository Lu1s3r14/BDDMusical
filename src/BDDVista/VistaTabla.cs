using System;
using Gtk;
using Gio;

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

    //Crea la tabla con su contenido
    public VistaTabla() {
	barraVista = ScrolledWindow.New();
	barraVista.Hexpand = true;
	barraVista.Vexpand = true;
	barraVista.HasFrame = true;
	contenidoTabla = ColumnView.New(null);
	contenidoTabla.AppendColumn(nuevaColumna("Título"));
	contenidoTabla.AppendColumn(nuevaColumna("Artista"));
        contenidoTabla.AppendColumn(nuevaColumna("Álbum"));
        contenidoTabla.AppendColumn(nuevaColumna("Género"));
	barraVista.SetChild(contenidoTabla);
    }

    //Crea una nueva columna para la tabla de la bdd
    public ColumnViewColumn nuevaColumna(string encabezado) {
	var nuevo = SignalListItemFactory.New();
	nuevo.OnSetup += (sender, args) => {
	    var item = (ListItem)args.Object;
	    var etq = Label.New("");
	    etq.Halign = Align.Start;
	    etq.MarginStart = 10;
	    item.SetChild(etq);
	};
	var columna = ColumnViewColumn.New(encabezado, nuevo);
	columna.Expand = true;
	return columna;
    }
}
