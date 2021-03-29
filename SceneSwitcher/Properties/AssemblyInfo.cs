using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SceneSwitcher")]
[assembly: AssemblyDescription("This program will watch the area of the screen specified. The area watched is intended to be the lower right corner of the screen on the secondary monitor of the Kingdom Hall, where the JW logo appears when showing the yeartext. While being watched, if the logo dissapears, a new window named \"Scene2\" is opened briefly (300ms), long enough for the ODBC feature \"Automatic Scene Switcher\" to detect it and change accordingly. When the watcher sees the JW logo come back, it will repeat the process, but the window opened will be named \"Scene1\". ")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("SceneSwitcher")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("8f1a17c8-3968-4348-ad2e-88ec10a0c7a5")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
