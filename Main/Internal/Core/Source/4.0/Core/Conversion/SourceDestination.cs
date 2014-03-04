namespace EyeSoft
{
	using System;

	internal class SourceDestination
	{
		internal readonly Type sourceType;
		internal readonly Type destinationType;

		protected SourceDestination(Type source, Type destination)
		{
			sourceType = source;
			destinationType = destination;
		}
	}
}