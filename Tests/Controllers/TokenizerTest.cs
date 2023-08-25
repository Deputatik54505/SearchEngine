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
			"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Coffee Lovers' Haven</title>\r\n    <link rel=\"stylesheet\" href=\"styles.css\">\r\n</head>\r\n<body>\r\n    <header>\r\n        <h1>Welcome to Coffee Haven</h1>\r\n        <p>Your one-stop destination for everything coffee!</p>\r\n    </header>\r\n    <div class=\"container\">\r\n        <h2>About Us</h2>\r\n        <p>We are passionate about coffee and dedicated to sharing our love for this magical beverage with the world.</p>\r\n\r\n        <h2>Our Coffee Selection</h2>\r\n        <p>Explore our diverse range of coffee beans sourced from around the globe. From rich and bold to smooth and mellow, we have something for every palate.</p>\r\n\r\n        <h2>Brewing Tips</h2>\r\n        <p>Discover the art of brewing the perfect cup. Our experts have curated tips and techniques to elevate your coffee-making skills.</p>\r\n\r\n        <h2>Coffee Recipes</h2>\r\n        <p>Indulge in the wonderful world of coffee-infused recipes. From classic espresso drinks to delightful coffee desserts, you're in for a treat!</p>\r\n    </div>\r\n    <footer>\r\n        <p>&copy; 2023 Coffee Haven. All rights reserved.</p>\r\n    </footer>\r\n</body>\r\n</html>\r\n";
		var page = new Page("https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices", html);
		var expectedText = "\r\n    \r\n    \r\n    Coffee Lovers' Haven\r\n    \r\n\r\n\r\n    \r\n        Welcome to Coffee Haven\r\n        Your one-stop destination for everything coffee!\r\n    \r\n    \r\n        About Us\r\n        We are passionate about coffee and dedicated to sharing our love for this magical beverage with the world.\r\n\r\n        Our Coffee Selection\r\n        Explore our diverse range of coffee beans sourced from around the globe. From rich and bold to smooth and mellow, we have something for every palate.\r\n\r\n        Brewing Tips\r\n        Discover the art of brewing the perfect cup. Our experts have curated tips and techniques to elevate your coffee-making skills.\r\n\r\n        Coffee Recipes\r\n        Indulge in the wonderful world of coffee-infused recipes. From classic espresso drinks to delightful coffee desserts, you're in for a treat!\r\n    \r\n    \r\n        © 2023 Coffee Haven. All rights reserved.\r\n    \r\n\r\n\r\n";

		var text = Tokenizer.ParseText(page);
		Debug.WriteLine(text);
		Debug.WriteLine(expectedText);

		Assert.Equal(text,expectedText);
	}
}