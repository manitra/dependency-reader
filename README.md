# dependency-reader [![Build Status](http://build.manitra.net/job/dependency-reader/badge/icon)](http://build.manitra.net/job/dependency-reader/)
A command line tool which dumps all the dependencies of .NET assemblies (*.dll or *.exe).
The latest ready to use binary is available here: [dep.exe](http://build.manitra.net/job/dependency-reader/lastSuccessfulBuild/artifact/bin/Release/dep.exe)

# Usage

## Syntax

```
dep [ file-path | dir-path ]
```

This will recursively search for assemblies in the given path and output all the parent-child relation of assemblies.
If the argument is ommited, the tool will start at the current folder `.` .

## Example

Hello world example
```
> dep

dep-1.0.0.0 mscorlib-4.0.0.0
dep-1.0.0.0 System-4.0.0.0
vshost32-12.0.0.0 mscorlib-4.0.0.0
vshost32-12.0.0.0 Microsoft.VisualStudio.HostingProcess.Utilities.Sync-12.0.0.0
vshost32-12.0.0.0 mscorlib-2.0.0.0
```

Combine it with other tools
```
> dep myproject/bin | grep -i reactive

myproject-1.0.0.0 Reactive.Core-2.2.5.0
myproject-1.0.0.0 Reactive.Linq-2.2.5.0
Reactive.Linq-2.2.5.0 Reactive.Core-2.2.5.0
```


# Build it yourself

Just get the source and build the solution

## Windows (Requires Visual Studio)

```bash
git clone https://github.com/manitra/dependency-reader.git
cd dependency-reader
nuget restore src/DependencyReader.sln
msbuild src/DependencyReader.sln
```

## Linux or MacOS (Requires mono-complete)

```bash
git clone https://github.com/manitra/dependency-reader.git
cd dependency-reader
mono nuget restore src/DependencyReader.sln
xbuild src/DependencyReader.sln
```


The output is the 'bin/[ Configuration ]' folder

# Next steps

Here some feature/bugfix to implement

- handle multiple version of the same assembly
- show indirect dependency (and add a third column for the path of the dependency)
