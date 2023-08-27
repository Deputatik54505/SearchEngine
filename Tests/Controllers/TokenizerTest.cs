using System.Diagnostics;
using SearchEngine.Controllers;
using SearchEngine.Models;
using Xunit;

namespace SearchEngine.Tests.Controllers;

public class TokenizerTest
{
	[Fact]
	public void ParseText_Casual_StringResponse()
	{
		var html =
			"<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <title>Coffee Page</title>\r\n    <script>\r\n        function showAlert() {\r\n            alert(\"Coffee is a brewed drink prepared from roasted coffee beans.\");\r\n        }\r\n    </script>\r\n</head>\r\n<body>\r\n    <h1>Welcome to the Coffee Page</h1>\r\n    <p>Coffee is a popular beverage enjoyed by people around the world.</p>\r\n    \r\n    <button onclick=\"showAlert()\">Click me to learn more about coffee!</button>\r\n</body>\r\n</html>\r\n";
		var page = new Page("https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices", html);
		var expectedText =
			"    Welcome to the Coffee Page Coffee is a popular beverage enjoyed by people around the world. Click me to learn more about coffee!   "; 
		var text = Tokenizer.ParseText(page);

		Debug.WriteLine(text);
		Debug.WriteLine(expectedText);

		Assert.Equal(text, expectedText);
	}


}