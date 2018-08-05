Overview

This project demonstrates that the multitargeting in FluentScheduler is not being done correctly.

ConsoleFullFramework - A console application targeting 4.7.2 full ConsoleFullFramework
NetStandardLibSerilog - A NetStandard library targeting 2.0
NetStandardLibFluentAssertions - A NetStandard library targeting 2.0

The reason for the two NetStandard libraries is to demonstrate that Serilog is doing multitargeting correctly
while FluentScheduler is not.

## NetStandardLibSerilog ##
This library references Serilog v2.7.1 which contains multitargeting for the following
    * net45
    * net46
    * netstandard1.0
    * netstandard1.3

## NetStandardLibFluentAssertions ##
This library contains FluentAssertions v5.3 which contains multitargeting for the following
    * net40
    * netstandard1.4

# Building Fullframework targeting NetStandard projects #
When building a full framework target such as a windows service or console application, when you reference NetStandard
projects in the same solution, any transitive dependencies get resolved to the compatible version of the full framework version.

NOTE: You must make sure you are building using package references and not package.config otherwise you will not have 
any transitive references copied at all.

(https://www.hanselman.com/blog/ReferencingNETStandardAssembliesFromBothNETCoreAndNETFramework.aspx)

This means that after building ConsoleFullFramework we find in the bin/debug folder the following assemblies
* Serilog.dll (net46)
* FluentScheduler.dll (net40)

Running the application will result in the following exception

System.IO.FileLoadException: Could not load file or assembly 'FluentScheduler, Version=5.3.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)
File name: 'FluentScheduler, Version=5.3.0.0, Culture=neutral, PublicKeyToken=null'
   at NetStandardLibFluentScheduler.FluentSchedulerConfig.Main()

Which means that the expected manifest for FluentAssertion is not the same as what the NetStandardLibFluentAssertions class library was compiled against (which was netstandard1.4)

Since neither Serilog or FluentAssertions are being called directly from the Console application, a quick test with Serilog demonstrates that all the multitargeted assemblies have the same manifest, as you can copy any version into the bin/debug 
folder and run the application and it still works fine.

If you copy the FluentScheduler.dll from netstandard1.4 the application will also run without exceptions.

This means that in the FluentScheduler build process for multitargeting there is a flaw, as the assemblies are not being
built with the same manifest definitions, but they should be to allow the scenarios of using a NetStandard library from
a full framework application.

