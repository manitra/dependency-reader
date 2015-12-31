# dependency-reader
A command line tool which dumps all the dependencies of .NET assemblies (*.dll or *.exe)

# Download

The latest binary is available here:
http://build.manitra.net/job/dependency-reader/lastSuccessfulBuild/bin/Release/dep.exe

# Usage

## Syntax

```
dep [ file-path | dir-path ]
```

This will recursively search for assemblies in the given path and output all the parent-child relation of assemblies.
If the argument is ommited, the tool will start at the current folder `.` .

## Example

```
dep
```

> dep-1.0.0.0 mscorlib-4.0.0.0
> dep-1.0.0.0 System-4.0.0.0
> vshost32-12.0.0.0 mscorlib-4.0.0.0
> vshost32-12.0.0.0 Microsoft.VisualStudio.HostingProcess.Utilities.Sync-12.0.0.0
> vshost32-12.0.0.0 mscorlib-2.0.0.0

# Build is yourself

Just get the source and build the solution

## Windows (Requires Visual Studio)

```bash
git clone https://github.com/manitra/dependency-reader.git
cd dependency-reader
msbuild src/DependencyReader.sln
```

## Linux or MacOS (Requires mono-complete)

```bash
git clone https://github.com/manitra/dependency-reader.git
cd dependency-reader
xbuild src/DependencyReader.sln
```


The output is the 'bin/[ Configuration ]' folder
