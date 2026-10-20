project('Proyecto 2', version: '1.0')
dotnet = find_program('dotnet', required: true)
custom_target('compilar_BDD',
  build_by_default: true,
  input: 'src/BDDMusical.csproj',
  output: 'Prueba',
  command: [
    dotnet, 'publish',
    '@INPUT@',
    '-c', 'Release',
    '-r', 'linux-x64',
    '-o', @OUTDIR@'
])