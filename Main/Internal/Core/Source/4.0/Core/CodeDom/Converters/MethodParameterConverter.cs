namespace EyeSoft.CodeDom.Converters
{
	internal class MethodParameterConverter
		: IToStringConverter<MethodParameter>
	{
		public string ConvertToString(MethodParameter parameter)
		{
			return
				string.Format("{0} {1}", parameter.Type.FriendlyName(), parameter.Name);
		}
	}
}