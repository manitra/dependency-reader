# dependency-reader [![Build Status](http://build.manitra.net/job/dependency-reader/badge/icon)](http://build.manitra.net/job/dependency-reader/)
[dep.exe](http://build.manitra.net/job/dependency-reader/lastSuccessfulBuild/artifact/bin/Release/dep.exe) is a command line tool which dumps all the dependencies of .NET assemblies (*.dll or *.exe files) to the standard output in a parsable way.
It shows direct dependencies and indirect one.
If A depends on B and B depends on C, [dep.exe](http://build.manitra.net/job/dependency-reader/lastSuccessfulBuild/artifact/bin/Release/dep.exe) will output

- A depends on B with a distance of 1
- B depends on C with a distance of 1
- A depends on C with a distance of 2

[dep.exe](http://build.manitra.net/job/dependency-reader/lastSuccessfulBuild/artifact/bin/Release/dep.exe) has been designed to be focused on a single task an combinable with other command line tools like `grep` or `awk`.
Have a look at the examples below.


The latest ready to use binary is available here: [dep.exe](http://build.manitra.net/job/dependency-reader/lastSuccessfulBuild/artifact/bin/Release/dep.exe)

# Usage

Download [dep.exe](http://build.manitra.net/job/dependency-reader/lastSuccessfulBuild/artifact/bin/Release/dep.exe) first.


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

dep-1.0.0.0 mscorlib-4.0.0.0 1
dep-1.0.0.0 System-4.0.0.0 1
vshost32-12.0.0.0 mscorlib-4.0.0.0 1
vshost32-12.0.0.0 Microsoft.VisualStudio.HostingProcess.Utilities.Sync-12.0.0.0 1
vshost32-12.0.0.0 mscorlib-2.0.0.0 1
```

Combine it with other tools
```
> dep myproject/bin | grep -i reactive

myproject-1.0.0.0 Reactive.Core-2.2.5.0 2
myproject-1.0.0.0 Reactive.Linq-2.2.5.0 1
Reactive.Linq-2.2.5.0 Reactive.Core-2.2.5.0 1
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

- use multiple threads
- handle multiple version of the same assembly
- [DONE 0.2.0] show indirect dependency (and add a third column for the path of the dependency)
