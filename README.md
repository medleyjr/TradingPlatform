# TradingPlatform

This is a Dotnet Windows Forms application to test a few trading strategies against the IG CFD broker platform. Note that this application was written in 2016 and the IG api might have changed since then. This is a playground project to test if it's possible to predict the market with a trading bot. This product was not fully tested. All the trading bot algorithms were more loss making than profitable.

IGTrading is the main executable. It contains all the window Forms. IGTradingLib is a dll that contains most of the business logic. IGTradingLib.TradePlan contains a few trading strategies, e.g. TradeplanMA.cs using a moving average to signal trades.

This application allows you to:
- Download historical price data.
- Start a live data stream and store price changes for selected instruments.
- Convert the data stream data to candle bar data.
- View price history in a Candle bar chart.
- Test trading strategies against historical data. 
- Use a trading bot to automatically  trade for a given algorithm.

To run the application, you need to copy the contents of the Run folder in your output folder. Update the config\ApiConfig.json with the correct Api keys and login details.


