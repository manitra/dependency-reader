#!/bin/sh

REPO=$(readlink -f $(dirname $0)/..)

# Building c# and front
cd $REPO/src

if [ ! -f nuget.exe ]; then
    wget -O nuget.exe https://dist.nuget.org/win-x86-commandline/v2.8.6/nuget.exe
fi
mono nuget.exe restore DependencyReader.sln -NoCache -Source https://www.nuget.org/api/v2/
xbuild DependencyReader.sln /p:Configuration=Release
