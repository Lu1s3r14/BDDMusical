project('Proyecto 2', version: '1.0')
dotnet = find_program('dotnet', required: true)
custom_target('compilar_Min',
  build_by_default: true,
  input: 'src/Minerosrc/Minero.csproj',
  output: 'Minero',
  command: [
    dotnet, 'publish',
    '@INPUT@',
    '-c', 'Release',
    '-r', 'linux-x64',
    '-o', meson.current_build_dir()
])

custom_target('compilar_BDDM',
  build_by_default: true,
  input: 'src/BDDVista/BDDMusical.csproj',
  output: 'BDDMusical',
  command: [
    dotnet, 'publish',
    '@INPUT@',
    '-c', 'Release',
    '-r', 'linux-x64',
    '-o', meson.current_build_dir()
])
