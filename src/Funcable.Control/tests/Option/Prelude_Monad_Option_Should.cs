using FluentAssertions;
using Funcable.Core;
using Xunit;

using static Funcable.Control.Prelude;
using static Funcable.Control.Tests.FuncableTestFixture;

namespace Funcable.Control.Tests;

public class Prelude_Monad_Option_Should
{
	[Fact]
	public void Unwrap_T_In_IOption_Of_T_And_Invoke_Binder_And_Return_Result_In_IOption_Of_U() =>
		Bind(Some(HelloWorld), s => Some(ToInt(s)))
			.Should<IOption<int>>()
			.Be(Some(FortyTwo));

	[Fact]
	public void Not_Unwrap_T_In_IOption_Of_T_But_Return_An_IOption_Of_U_When_IOption_Of_T_Is_None() =>
		Bind(None<string>(), s => Some(ToInt(s)))
			.Should()
			.BeOfType<None<int>>();

	[Fact]
	public void Throw_PatternNotHandledException_When_IOption_T_Pattern_Not_Handled() =>
		new Action(() => Bind(new TestSome<string>(), s => Some(ToInt(s))))
			.Should()
			.Throw<InvalidPatternException>()
			.WithMessage("IOption`1.Match: TestSome`1");
}
