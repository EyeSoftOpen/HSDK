// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Error List, point to "Suppress Message(s)", and click
// "In Project Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
	"Microsoft.Portability",
	"CA1903:UseOnlyApiFromTargetedFramework",
	MessageId = "System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
	Justification = "It's ok.")]

[assembly: SuppressMessage(
	"Microsoft.Design",
	"CA1007:UseGenericsWhereAppropriate",
	Scope = "member",
	Target = "EFCachingProvider.Caching.ICache.#GetItem(System.String,System.Object&)",
	Justification = "It's ok.")]

[assembly: SuppressMessage(
	"Microsoft.Design",
	"CA1021:AvoidOutParameters",
	MessageId = "1#",
	Scope = "member",
	Target = "EFCachingProvider.Caching.ICache.#GetItem(System.String,System.Object&)",
	Justification = "It's ok.")]

[assembly: SuppressMessage(
	"Microsoft.Design",
	"CA1021:AvoidOutParameters",
	MessageId = "2#",
	Scope = "member",
	Target = "EFCachingProvider.Caching.CachingPolicy.#GetCacheableRows" +
	"(EFCachingProvider.CachingCommandDefinition,System.Int32&,System.Int32&)",
	Justification = "It's ok.")]

[assembly: SuppressMessage(
	"Microsoft.Design",
	"CA1021:AvoidOutParameters",
	MessageId = "1#",
	Scope = "member",
	Target = "EFCachingProvider.Caching.CachingPolicy." +
	"#GetCacheableRows(EFCachingProvider.CachingCommandDefinition,System.Int32&,System.Int32&)",
	Justification = "It's ok.")]

[assembly: SuppressMessage(
	"Microsoft.Design",
	"CA1021:AvoidOutParameters",
	MessageId = "1#",
	Scope = "member",
	Target = "EFCachingProvider.Caching.CachingPolicy." +
	"#GetExpirationTimeout(EFCachingProvider.CachingCommandDefinition,System.TimeSpan&,System.DateTime&)",
	Justification = "It's ok.")]

[assembly: SuppressMessage(
	"Microsoft.Design",
	"CA1021:AvoidOutParameters",
	MessageId = "2#",
	Scope = "member",
	Target = "EFCachingProvider.Caching.CachingPolicy." +
	"#GetExpirationTimeout(EFCachingProvider.CachingCommandDefinition,System.TimeSpan&,System.DateTime&)",
	Justification = "It's ok.")]

[assembly: SuppressMessage(
	"Microsoft.Design",
	"CA1031:DoNotCatchGeneralExceptionTypes",
	Scope = "member",
	Target = "EFCachingProvider.Caching.AspNetCache." +
	"#PutItem(System.String,System.Object,System.Collections.Generic" +
	".IEnumerable`1<System.String>,System.TimeSpan,System.DateTime)",
	Justification = "It's ok.")]

[assembly: SuppressMessage(
	"Microsoft.Design",
	"CA1031:DoNotCatchGeneralExceptionTypes",
	Scope = "member",
	Target = "EFCachingProvider.Caching.AspNetCache.#EnsureEntryExists(System.String)",
	Justification = "It's ok.")]