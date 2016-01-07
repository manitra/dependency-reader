#!/bin/sh
cd $(readlink -f $(dirname $0)/..)
mono src/packages/NUnit.Console.3.0.1/tools/nunit3-console.exe DependencyReader.CLI.Tests/bin/Release/DependencyReader.CLI.Tests.dll --out bin/TestResult.xml