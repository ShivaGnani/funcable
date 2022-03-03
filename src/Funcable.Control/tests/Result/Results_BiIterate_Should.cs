using FluentAssertions;
using Xunit;

using static Funcable.Control.Prelude;
using static Funcable.Control.Tests.FuncableTestFixture;

namespace Funcable.Control.Tests;

public class Results_BiIterate_Should
{
	[Fact]
	public void Reduce_T_In_IResult_When_IResult_Is_Ok_T() =>
		Ok<string, int>(HelloWorld).BiFold(
			string.Empty,
			(state, t) => state switch { { Length: 0 } => t, _ => $"{state} {t}" },
			(state, error) => state switch { { Length: 0 } => error.ToString(), _ => $"{state} {error}" }
		)
		.Should()
		.Be(HelloWorld);

	[Fact]
	public void Reduce_TError_In_IResult_When_IResult_Is_Error_TError() =>
		Error<string, int>(-1).BiFold(
			string.Empty,
			(state, t) => state switch { { Length: 0 } => t, _ => $"{state} {t}" },
			(state, error) => state switch { { Length: 0 } => error.ToString(), _ => $"{state} {error}" }
		)
		.Should()
		.Be("-1");

	[Fact]
	public void Reduce_Ts_And_TErrors_In_IResults() =>
		new[]
		{
				Ok<string, int>(HelloWorld),
				Error<string, int>(-1),
				Ok<string, int>(HolaMundo),
				Error<string, int>(-20),
		}
		.BiFold(
			string.Empty,
			(state, t) => state switch { { Length: 0 } => t, _ => $"{state} {t}" },
			(state, error) => state switch { { Length: 0 } => error.ToString(), _ => $"{state} {error}" }
		)
		.Should()
		.Be("Hello, World! -1 Hola, Mundo! -20");
}
