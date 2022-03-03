using FluentAssertions;
using Xunit;

using static Funcable.Control.Prelude;
using static Funcable.Control.Tests.FuncableTestFixture;

namespace Funcable.Control.Tests;

public class Results_Fold_Should
{
	[Fact]
	public void Reduce_T_In_IResult_T_When_IResult_Is_Ok() =>
		Ok<string, int>(HelloWorld).Fold(
			string.Empty,
			(state, t) => state switch { { Length: 0 } => t, _ => $"{state} {t}" }
		)
		.Should()
		.Be(HelloWorld);

	[Fact]
	public void Reduce_Ts_In_IResult_T_When_IResult_Is_Ok() =>
		new[]
		{
				Ok<string, int>(HelloWorld),
				Error<string, int>(-1),
				Ok<string, int>(HolaMundo)
		}
		.Fold(
			string.Empty,
			(state, t) => state switch { { Length: 0 } => t, _ => $"{state} {t}" }
		)
		.Should()
		.Be("Hello, World! Hola, Mundo!");
}
