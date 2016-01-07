#!/bin/sh
cd $(readlink -f $(dirname $0)/..)
mono src/packages/NUnit.Runners.2.6.3/tools/nunit-console.exe src/DependencyReader.CLI.Tests/bin/Release/DependencyReader.CLI.Tests.dll --output=bin/TestResult.xml