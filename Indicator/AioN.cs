#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion



namespace CustomEnumSpace
{
	public enum DisplayMode
	{
		Difference,
		DataSeries,
		ALL
	}
}

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
    /// AioN - Percent Difference Indicator
    /// </summary>
    [Description("AioN - Percent Difference Indicator")]
    public class AioN : Indicator
    {
		private double start1 = -1;
		private double start2 = -1;
		private bool isStartPointFound = false;
		
		private string secondDataSeries = "MNST";
		private CustomEnumSpace.DisplayMode currentMode = CustomEnumSpace.DisplayMode.Difference;
		
		
        protected override void Initialize()
        {
			Add(secondDataSeries, BarsPeriod.Id, BarsPeriod.Value);
			
            Add(new Plot(Color.Blue, PlotStyle.Line, "Plot1 - DataSeries 1"));
			Add(new Plot(Color.Green, PlotStyle.Line, "Plot2 - DataSeries 2"));
			Add(new Plot(Color.Red, PlotStyle.Line, "Plot3 - Difference"));
			
            Overlay	= true;
        }
		
		protected override void OnStartUp()
		{
			
		}
		
        protected override void OnBarUpdate()
        {
	
			if (isStartPointFound == false &&
				CurrentBars[0] > -1 && CurrentBars[1] > -1)
			{
				//common point is found
				//indicator's plot pre-start
				start1 = BarsArray[0][0];
				start2 = BarsArray[1][0];
				
				isStartPointFound = true;
				Log(start1.ToString() + " , "+start2.ToString(), LogLevel.Information);
			}
			
	//		if (CurrentBars[0] < 2 || CurrentBars[1] < 2)
	//		{
	//			return; //do not print first entry
	//		}
			
			if (!isStartPointFound) return;
			
			switch (currentMode) //mode switch
			{
				case CustomEnumSpace.DisplayMode.ALL:
				{
					Plot1[0] = BarsArray[0][0] / start1;
					Plot2[0] = BarsArray[1][0] /start2;	
					Plot3[0] = Plot1[0] - Plot2[0] + 1;
					break;
				}
				case CustomEnumSpace.DisplayMode.DataSeries:
				{
					Plot1[0] = BarsArray[0][0] / start1;					
					Plot2[0] = BarsArray[1][0] /start2;
					break;
				}
				case CustomEnumSpace.DisplayMode.Difference:
				{
					double a = BarsArray[0][0] / start1;
					double b = BarsArray[1][0] /start2;
					Plot3[0] = a - b;
					break;
				}
			};
			
			return;
			
			/*
			// ensure both series have at least one bar
			if (CurrentBars[0] < 1 || CurrentBars[1] < 1)
				return;
			
			// when the 5 minute series is processing set the secondary plot to the sma with the secondary series input
			if (BarsInProgress == 1)
				SMASecondary[0] = sma2[0];
			
			SMAPrimary[0] = sma1[0];
			//when the primary series is processing set the primary plot to the sma with the primary series input
			if (BarsInProgress == 0)
			{
				SMAPrimary[0] = sma1[0];
				
				// if the secondary 5 minute series did not close, set the current bar's value to the previous bar's value to prevent gaps 
				if (!SMASecondary.ContainsValue(0))
					SMASecondary[0] = SMASecondary[1];
			}*/
        }
		
		
		
		#region Properties
		
		[Description("Chart Display Mode")]
		[GridCategory("Parameters")]
		public CustomEnumSpace.DisplayMode DisplayMode
		{
			get { return this.currentMode; }
			set { this.currentMode = value; }
		}
		
		[Description("The Second DataSeries")]
		[GridCategory("Parameters")]
		public string SecondDataSeries
		{
			get { return secondDataSeries; }
			set { secondDataSeries = value; }
		}
		
		#endregion
		
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Plot1
        {
            get { return Values[0]; }
        }
	
		
		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Plot2
        {
            get { return Values[1]; }
        }
		
		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Plot3
        {
            get { return Values[2]; }
        }
    }
}

#region NinjaScript generated code. Neither change nor remove.
// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    public partial class Indicator : IndicatorBase
    {
        private AioN[] cacheAioN = null;

        private static AioN checkAioN = new AioN();

        /// <summary>
        /// AioN - Percent Difference Indicator
        /// </summary>
        /// <returns></returns>
        public AioN AioN(CustomEnumSpace.DisplayMode displayMode, string secondDataSeries)
        {
            return AioN(Input, displayMode, secondDataSeries);
        }

        /// <summary>
        /// AioN - Percent Difference Indicator
        /// </summary>
        /// <returns></returns>
        public AioN AioN(Data.IDataSeries input, CustomEnumSpace.DisplayMode displayMode, string secondDataSeries)
        {
            if (cacheAioN != null)
                for (int idx = 0; idx < cacheAioN.Length; idx++)
                    if (cacheAioN[idx].DisplayMode == displayMode && cacheAioN[idx].SecondDataSeries == secondDataSeries && cacheAioN[idx].EqualsInput(input))
                        return cacheAioN[idx];

            lock (checkAioN)
            {
                checkAioN.DisplayMode = displayMode;
                displayMode = checkAioN.DisplayMode;
                checkAioN.SecondDataSeries = secondDataSeries;
                secondDataSeries = checkAioN.SecondDataSeries;

                if (cacheAioN != null)
                    for (int idx = 0; idx < cacheAioN.Length; idx++)
                        if (cacheAioN[idx].DisplayMode == displayMode && cacheAioN[idx].SecondDataSeries == secondDataSeries && cacheAioN[idx].EqualsInput(input))
                            return cacheAioN[idx];

                AioN indicator = new AioN();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.DisplayMode = displayMode;
                indicator.SecondDataSeries = secondDataSeries;
                Indicators.Add(indicator);
                indicator.SetUp();

                AioN[] tmp = new AioN[cacheAioN == null ? 1 : cacheAioN.Length + 1];
                if (cacheAioN != null)
                    cacheAioN.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheAioN = tmp;
                return indicator;
            }
        }
    }
}

// This namespace holds all market analyzer column definitions and is required. Do not change it.
namespace NinjaTrader.MarketAnalyzer
{
    public partial class Column : ColumnBase
    {
        /// <summary>
        /// AioN - Percent Difference Indicator
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.AioN AioN(CustomEnumSpace.DisplayMode displayMode, string secondDataSeries)
        {
            return _indicator.AioN(Input, displayMode, secondDataSeries);
        }

        /// <summary>
        /// AioN - Percent Difference Indicator
        /// </summary>
        /// <returns></returns>
        public Indicator.AioN AioN(Data.IDataSeries input, CustomEnumSpace.DisplayMode displayMode, string secondDataSeries)
        {
            return _indicator.AioN(input, displayMode, secondDataSeries);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// AioN - Percent Difference Indicator
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.AioN AioN(CustomEnumSpace.DisplayMode displayMode, string secondDataSeries)
        {
            return _indicator.AioN(Input, displayMode, secondDataSeries);
        }

        /// <summary>
        /// AioN - Percent Difference Indicator
        /// </summary>
        /// <returns></returns>
        public Indicator.AioN AioN(Data.IDataSeries input, CustomEnumSpace.DisplayMode displayMode, string secondDataSeries)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.AioN(input, displayMode, secondDataSeries);
        }
    }
}
#endregion
