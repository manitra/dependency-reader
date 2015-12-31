# dependency-reader
A command line tool which dumps all the dependencies of .NET assemblies (*.dll or *.exe)

# Download

The latest binary is available here:
http://build.manitra.net/job/dependency-reader/lastSuccessfulBuild/bin/Release/dep.exe

# Usage

```
dep [ file-path | dir-path ]
```

This will recursively search for assemblies in the given path and output all the parent-child
relation of assemblies.

# Build is your self

Just get the source and build the solution

```
git clone https://github.com/manitra/dependency-reader.git
cd dependency-reader
msbuild src/DependencyReader.sln
```

The output is the 'bin/[ Configuration ]' folder
