using System.Globalization;

namespace Assets.Infrastructure.CoreTools.Static.Strings
{
    public static class StringFormatHelper 
	{
		private static readonly CultureInfo UsCultureInfo = new CultureInfo("en-US");

		public static string FormatMoney(long moneyValue)
		{
			return "$" + moneyValue.ToString("N0", UsCultureInfo);
		}

		public static string FormatComma(double moneyValue)
		{
			return FormatComma((long)moneyValue);
		}

		public static string FormatComma(long moneyValue)
		{
			return moneyValue.ToString("N0", UsCultureInfo);
		}

		public static string FormatPriceDollar(float price, bool showText = false)
		{
			return FormatPriceDollar((double)price, showText);
		}

		public static string FormatPriceDollar(double price, bool showText = false)
		{
			var value = "$" + price.ToString("F");
			if (showText)
			{
				value += " USD";
			}
			return value;
		}

		public static string FormatShortMoney(double moneyValue)
		{
			return FormatShortMoney((long)moneyValue);
		}

		public static string FormatShortMoney(long moneyValue)
		{
			if (moneyValue >= 100000000000)
				return (moneyValue / 1000000000).ToString("#,0") + "B";
			if (moneyValue >= 10000000000)
				return (moneyValue / 1000000000D).ToString("0.#") + "B";
			if (moneyValue >= 100000000)
				return (moneyValue / 1000000D).ToString("0.00#") + "M";
			if (moneyValue >= 10000000)
				return (moneyValue / 1000000D).ToString("0.#") + "M";
			if (moneyValue >= 100000)
				return (moneyValue / 1000).ToString("#,0") + "K";
			if (moneyValue >= 10000)
				return (moneyValue / 1000D).ToString("0.#") + "K";
			return moneyValue.ToString("#,0");
		}

	    public static string FormatMoneyOverThreshold(long moneyValue, long threshold = 1000000000L)
	    {
	        if (moneyValue >= threshold)
	        {
	            return FormatShortMoney(moneyValue);
	        }

	        return FormatComma(moneyValue);
	    }
	}
}