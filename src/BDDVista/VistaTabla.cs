using System;
using Gtk;
using Gio;
using GObject;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

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
    //Llena la tabla con la bdd en sql
    public async void LlenarTabla(string ruta) {
		var nuevoContenido = await System.Threading.Tasks.Task.Run(() => {
          List<Temporal> lista = new List<Temporal>();
          try {
              string conexion = $"Data Source={ruta}";
              using var conexionV = new SqliteConnection(conexion);
              conexionV.Open();
              string llenar = @"SELECT r.title, p.name, a.name, r.genre FROM rolas r JOIN performers p ON r.id_performer = p.id_performer JOIN albums a ON r.id_album = a.id_album";
              var comandoLlena = new SqliteCommand(llenar, conexionV);
              var leer = comandoLlena.ExecuteReader();
              while(leer.Read()) {
                  lista.Add(new Temporal {
                      Titulo = leer.IsDBNull(0) ? "Desconocido" : leer.GetString(0),
                      Artista = leer.IsDBNull(1) ? "Desconocido" : leer.GetString(1),
                      Album = leer.IsDBNull(2) ? "Desconocido" : leer.GetString(2),
                      Genero = leer.IsDBNull(3) ? "Desconocido" : leer.GetString(3)
                    });
            }
		} catch (Exception e) {
            Console.WriteLine("Error al leer");
        }
        return lista;
      });
      GLib.Functions.IdleAdd(0, () => {
        datos.RemoveAll();
        foreach(var song in nuevoContenido) {
          datos.Append(FilaTabla.Create(song.Titulo, song.Artista, song.Album, song.Genero));
        }
        return false;
      });
    }
	//Clase que se usa para obtener datos de la bdd
	private class Temporal {
      public string Titulo {
        get;
        set;
      }
      public string Artista {
        get;
        set;
      }
      public string Album {
        get;
        set;
      }
      public string Genero {
        get;
        set;
      }
    }
}
