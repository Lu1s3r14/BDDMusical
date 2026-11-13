# Base de datos musical con Interfaz Gráfica.

Para poder ejecutar/compilar este programa es necesario contar con:

* Meson (_Para poder compilar este programa._)
* Gtk4 (_Principalmente para la interfaz_.)
* SDK .NET 8 (_Para compilar y usar las bibliotecas de lectura de etiquetas del .mp3_)

## Uso:
Compilar de la forma:

```
$ meson setup build
```
Después:
```
$ meson compile -C build
```
Todo esto desde la carpeta raíz del programa.

El programa se puede usar de dos formas:
* El minero de la base de datos:
```
$ ./build/Minero ruta/de/prueba
```
* La interfaz gráfica de la base de datos.
```
$ ./build/BDDMusical
```

Cada una es independiente, pero si se ejecuta el minero desde la terminal crea un .db donde guarda todo lo que encuentra de los .mp3.

La interfaz gráfica usa el mismo .db para guardar/buscar/editar la base de datos, además del minero para encontrar nuevos .mp3