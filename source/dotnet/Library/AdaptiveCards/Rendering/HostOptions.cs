﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdaptiveCards.Rendering
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class HostOptions
    {
        public HostOptions() { }

        /// <summary>
        /// Font family for the card (can be comma delimited for fallback)
        /// </summary>
        public string FontFamily { get; set; } = "Calibri";

        /// <summary>
        /// FontSize
        /// </summary>
        public FontSizeOptions FontSizes { get; set; } = new FontSizeOptions();


        /// <summary>
        /// Toggles whether or not to render inputs and actions
        /// </summary>
        public bool SupportsInteractivity { get; set; } = true;

        /// <summary>
        /// The types of Actions that you support(null for no actions)
        /// </summary>
        public string[] SupportedActionTypes { get; set; } = new string[]
        {
            OpenUrlAction.TYPE,
            SubmitAction.TYPE,
            HttpAction.TYPE,
            ShowCardAction.TYPE
        };

        public ImageSizeOptions ImageSizes { get; set; } = new ImageSizeOptions();

        /// <summary>
        /// Max number of actions to support on your Cards(e.g., 3)
        /// </summary>
        public int MaxActions { get; set; } = 5;

        /// <summary>
        /// Separation settings 
        /// </summary>
        public SeparationOptions Separation { get; set; } = new SeparationOptions();


        //  ------ AdaptiveCard -------
        public AdaptiveCardOptions AdaptiveCard { get; set; } = new AdaptiveCardOptions();

        /// <summary>
        /// Color settings for the TextBlock
        /// </summary>
        public ColorOptions Colors { get; set; } = new ColorOptions();

        // ------ Containers ------
        public ImageSetOptions ImageSet { get; set; } = new ImageSetOptions();

        public FactSetOptions FactSet { get; set; } = new FactSetOptions();

        public ColumnOptions Column { get; set; } = new ColumnOptions();

        // ------ Actions------
        public ActionOptions Actions { get; set; } = new ActionOptions();
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class BoundaryOptions
    {
        public BoundaryOptions() { }

        public BoundaryOptions(int allMargin)
        {
            Left = Right = Top = Bottom = allMargin;
        }
        public BoundaryOptions(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }


        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class AdaptiveCardOptions
    {
        public AdaptiveCardOptions() { }

        /// <summary>
        ///  Padding for the card
        /// </summary>
        public BoundaryOptions Padding { get; set; } = new BoundaryOptions(8);

        /// <summary>
        /// Background color for card 
        /// </summary>
        public string BackgroundColor { get; set; } = "#FFFFFF";
    }

    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum ActionsOrientation
    {
        /// <summary>
        /// actions should be laid out horizontally
        /// </summary>
        Horizontal,

        /// <summary>
        /// Actions should be laid out vertically
        /// </summary>
        Vertical
    }


    /// <summary>
    /// Properties which control spacing and visual between elements
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SeparationOptions
    {
        public SeparationOptions() { }

        /// <summary>
        /// Separation settings when Separation:default
        /// </summary>
        public SeparationOption Default { get; set; } = new SeparationOption() { Spacing = 10 };

        /// <summary>
        /// Separation settings when Separation:Strong
        /// </summary>
        public SeparationOption Strong { get; set; } = new SeparationOption() { Spacing = 20, LineThickness = 1, LineColor = "#FF707070" };
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SeparationOption
    {
        public SeparationOption() { }

        /// <summary>
        /// How much space between this element and previous should be used
        /// </summary>
        public int Spacing { get; set; }

        /// <summary>
        /// If there is a visible line, how thick should the line be
        /// </summary>
        public int LineThickness { get; set; }

        /// <summary>
        /// If there is a visible color, what color to use
        /// </summary>
        public string LineColor { get; set; }

    }

    /// <summary>
    /// Properties which control rendering of TextBlock 
    /// </summary>
    //public class TextBlockOptions : CardElementOptions
    //{
    //    public TextBlockOptions() { }

    //}

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class FontSizeOptions
    {
        public FontSizeOptions() { }

        public int Small { get; set; } = 10;

        public int Normal { get; set; } = 12;

        public int Medium { get; set; } = 14;

        public int Large { get; set; } = 17;

        public int ExtraLarge { get; set; } = 20;

    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ColorOptions
    {
        public ColorOptions() { }

        public ColorOption Default { get; set; } = new ColorOption("#FF000000");

        public ColorOption Accent { get; set; } = new ColorOption("#FF0000FF");

        public ColorOption Dark { get; set; } = new ColorOption("#FF101010");

        public ColorOption Light { get; set; } = new ColorOption("#FFFFFFFF");

        public ColorOption Good { get; set; } = new ColorOption("#FF008000");

        public ColorOption Warning { get; set; } = new ColorOption("#FFFFD700");

        public ColorOption Attention { get; set; } = new ColorOption("#FF8B0000");
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ColorOption
    {
        public ColorOption(string normal, string subtle = null)
        {
            this.Normal = normal;
            if (subtle == null)
            {
                var opacity = (byte)(Convert.ToByte(normal.Substring(1, 2), 16) * .7);
                this.Subtle = $"#{opacity.ToString("x")}{normal.Substring(3)}";
            }
            else
                this.Subtle = subtle;
        }

        /// <summary>
        /// Color in #RRGGBB format
        /// </summary>
        public string Normal { get; set; }

        public string Subtle { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ImageSizeOptions

    {
        public ImageSizeOptions() { }

        public int Small { get; set; } = 60;

        public int Medium { get; set; } = 120;

        public int Large { get; set; } = 180;
    }

    /// <summary>
    /// Properties which control rendering of actions
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ActionOptions
    {
        public ActionOptions() { }

        public ShowCardOptions ShowCard { get; set; } = new ShowCardOptions();

        /// <summary>
        /// Arrange actions horizontal or vertical
        /// </summary>
        public ActionsOrientation ActionsOrientation { get; set; } = ActionsOrientation.Horizontal;

        /// <summary>
        /// should they be aligned Left, Center or Right
        /// </summary>
        public HorizontalAlignment ActionAlignment { get; set; } = HorizontalAlignment.Center;


        public string BackgroundColor { get; set; } = "#FF5098FF";

        public string BorderColor { get; set; } = "#FF000000";

        public string TextColor { get; set; } = "#FFFFFFFF";

        public int BorderThickness { get; set; } = 1;

        public int FontWeight { get; set; } = 400;

        public int FontSize { get; set; } = 12;

        /// <summary>
        /// Space between actions
        /// </summary>
        public int Spacing { get; set; } = 8;

        /// <summary>
        /// space between title and button edge
        /// </summary>

        public BoundaryOptions Padding { get; set; } = new BoundaryOptions(4);

    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ShowCardOptions
    {
        public ShowCardOptions() { }

        public ShowCardActionMode ActionMode { get; set; } = ShowCardActionMode.Inline;

        /// <summary>
        /// Background color for showcard area
        /// </summary>
        public string BackgroundColor { get; set; } = "#FFF8F8F8";

        /// <summary>
        /// If actionMode is inline and AutoPadding is on then the background will extend to the edges of the parent card.
        /// </summary>
        public bool AutoPadding { get; set; } = true;
    }

    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum ShowCardActionMode
    {
        Inline,
        Popup
    }


    public class ImageSetOptions 
    {
        public ImageSetOptions() { }

        public ImageSize ImageSize { get; set; } = ImageSize.Medium;
    }

    public class FactSetOptions 
    {
        public FactSetOptions() { }

        /// <summary>
        /// TextBlock to use for Titles in factsets
        /// </summary>
        public TextOptions Title { get; set; } = new TextOptions() { Size = TextSize.Normal, Color = TextColor.Default, IsSubtle = false, Weight = TextWeight.Bolder };

        /// <summary>
        /// TextBlock to use for Values in fact sets
        /// </summary>
        public TextOptions Value { get; set; } = new TextOptions();

        /// <summary>
        /// Spacing between facts and values
        /// </summary>
        public int Spacing { get; set; } = 20;
    }

    public class ColumnOptions
    {
        public ColumnOptions() { }

        /// <summary>
        /// Separation settings between columns
        /// </summary>
        public SeparationOptions Separation { get; set; } = new SeparationOptions();

    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class TextOptions
    {
        public TextOptions()
        { }

        /// <summary>
        ///     The size of the text
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TextSize Size { get; set; } = TextSize.Normal;

        /// <summary>
        ///     The weight of the text
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TextWeight Weight { get; set; } = TextWeight.Normal;

        /// <summary>
        ///     The color of the text
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TextColor Color { get; set; } = TextColor.Default;

        /// <summary>
        ///     Should it be subtle?
        /// </summary>
        public bool IsSubtle { get; set; } = false;

        public bool ShouldSerializeSize()
        {
            return Size != TextSize.Normal;
        }

        public bool ShouldSerializeColor()
        {
            return Color != TextColor.Default;
        }


        public bool ShouldSerializeWeight()
        {
            return Weight != TextWeight.Normal;
        }

        public bool ShouldSerializeIsSubtle()
        {
            return IsSubtle;
        }
    }
}
