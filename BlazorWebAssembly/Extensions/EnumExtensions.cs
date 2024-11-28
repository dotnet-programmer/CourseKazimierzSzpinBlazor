using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BlazorWebAssembly.Extensions;

public static class EnumExtensions
{
	// pobranie z enuma wartości atrybutu "Display"
	public static string GetDisplayName(this Enum enumValue)
		=> enumValue
			.GetType()
			.GetMember(enumValue.ToString())
			.FirstOrDefault()?
			.GetCustomAttribute<DisplayAttribute>()?
			.GetName() ?? enumValue.ToString();
}