// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It is a library file and can keep its unused function.", Scope = "member", Target = "~M:GalsPassHolder.GalLib.BytesToString(System.Byte[])~System.String")]
[assembly: SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "This is done to reduce the time sensitive data is in memory.", Scope = "member", Target = "~M:GalsPassHolder.FrmMain.File_GetMainHash(System.Boolean)~System.Boolean")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>", Scope = "member", Target = "~M:GalsPassHolder.GalLib.GetHash512(System.String,System.String,System.Int32)~System.Byte[]")]
[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]

