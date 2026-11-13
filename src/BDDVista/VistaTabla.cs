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
    //El boton de editar canción
    public Button Editar {
      get;
      set;
    }
    //La ruta de la bdd para poder editarla
    private string rutaBDD;
    
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
    Editar = Button.NewWithLabel("Editar");
    Editar.Halign = Align.End;
    Editar.MarginTop = 10;
    Editar.MarginBottom = 10;
    Editar.MarginEnd = 13;
    Editar.OnClicked += (s, e) => {
      if (seleccionar.SelectedItem == null)
        return;
      var seleccionado = (FilaTabla)seleccionar.SelectedItem;
      var ventanaEdit = Window.New();
      ventanaEdit.Title = "Editar canción.";
      ventanaEdit.DefaultWidth = 320;
      var caja = Box.New(Orientation.Vertical, 10);
      caja.MarginTop = caja.MarginBottom = caja.MarginStart = caja.MarginEnd = 15;
      var textoT = Entry.New();
      textoT.Buffer.Text = seleccionado.Titulo;
      var textoF = Entry.New();
      textoF.Buffer.Text = seleccionado.Fecha;
      Button Guardar = Button.NewWithLabel("Guardar.");
      Guardar.OnClicked += async (s2, e2) => {
        Guardar.Sensitive = false;
        await ActualizarC(seleccionado.IdRola, textoT.Buffer.Text, textoF.Buffer.Text);
        ventanaEdit.Close();
        LlenarTabla(rutaBDD);
      };
      caja.Append(Label.New("Título de la pista:"));
      caja.Append(textoT);
      caja.Append(Label.New("Año:"));
      caja.Append(textoF);
      caja.Append(Guardar);
      ventanaEdit.SetChild(caja);
      ventanaEdit.Show();
      };
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
      rutaBDD = ruta;
		var nuevoContenido = await System.Threading.Tasks.Task.Run(() => {
          List<Temporal> lista = new List<Temporal>();
          try {
              string conexion = $"Data Source={ruta}";
              using var conexionV = new SqliteConnection(conexion);
              conexionV.Open();
              string llenar = @"SELECT r.id_rola, r.title, p.name, a.name, r.genre, r.year FROM rolas r JOIN performers p ON r.id_performer = p.id_performer JOIN albums a ON r.id_album = a.id_album";
              var comandoLlena = new SqliteCommand(llenar, conexionV);
              var leer = comandoLlena.ExecuteReader();
              while(leer.Read()) {
                  lista.Add(new Temporal {
                      IdRola = leer.GetInt32(0),
                      Titulo = leer.IsDBNull(1) ? "Desconocido" : leer.GetString(1),
                      Artista = leer.IsDBNull(2) ? "Desconocido" : leer.GetString(2),
                      Album = leer.IsDBNull(3) ? "Desconocido" : leer.GetString(3),
                      Genero = leer.IsDBNull(4) ? "Desconocido" : leer.GetString(4),
                      Fecha = leer.IsDBNull(5) ? "Desconocido" : leer.GetInt32(5).ToString()
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
          datos.Append(FilaTabla.Create(song.IdRola, song.Titulo, song.Artista, song.Album, song.Genero, song.Fecha));
        }
        return false;
      });
    }
    //Actualiza la bdd con los nuevos datos para las canciones
    public async System.Threading.Tasks.Task ActualizarC(int idRola, string tituloN, string fechaN) {
      string conexion = $"Data Source={rutaBDD}";
      await System.Threading.Tasks.Task.Run(() => {
        using var conexionA = new SqliteConnection(conexion);
        conexionA.Open();
        string actu = "UPDATE rolas SET title = @t, year = @y WHERE id_rola = @id";
        using var comandoActu = new SqliteCommand(actu, conexionA);
        comandoActu.Parameters.AddWithValue("@t", tituloN);
        comandoActu.Parameters.AddWithValue("@y", int.TryParse(fechaN, out int a) ? a : 0);
        comandoActu.Parameters.AddWithValue("@id", idRola);
        comandoActu.ExecuteNonQuery();
      });
    }
	//Clase que se usa para obtener datos de la bdd
	private class Temporal {
      public int IdRola {
        get;
        set;
      }
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
      public string Fecha {
        get;
        set;
      }
    }
}
