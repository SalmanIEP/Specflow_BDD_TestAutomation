﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Selenium.Intilaizer;

namespace Selenium.Support.Extensions
{
    public static class SeleniumWebElementExtensions
    {

        /// <summary>
        ///     Gets the javascript executor.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        /// 
        
       

        /// <summary>
        ///     Gets the type of the element attributes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">The element.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static T GetAttributeAsType<T>(this IWebElement element, string attributeName)
        {
            var value = element.GetAttribute(attributeName) ?? string.Empty;
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
        }

       
        /// <summary>
        ///     Hovers over and then clicks the element.
        /// </summary>
        /// <param name="element">The element.</param>
       

        /// <summary>
        ///     Determines if the element is displayed.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>true if the element is displayed, otherwise false</returns>
        public static bool Displayed(this IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        ///     Gets the specified elements class attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The class attribute value</returns>
        public static string ClassName(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("class");
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets the specified elements name attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The name attribute value</returns>
        public static string GetName(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("name");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets the specified elements id attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The id attribute value</returns>
        public static string GetId(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("id");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets the specified elements style attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The style attribute value</returns>
        public static string GetStyle(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("style");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets the specified elements value attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The value attribute</returns>
        public static string GetValue(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("value");
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets the specified elements type attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The type attribute value</returns>
        public static string GetTypeValue(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("type");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets the specified elements title attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The title attribute value</returns>
        public static string GetTitle(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("title");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets the specified elements parent element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The parent element</returns>
        public static IWebElement GetParent(this IWebElement element)
        {
            return element.FindElement(By.XPath("./parent::*"));
        }

        /// <summary>
        ///     Gets the specified elements child element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The child element</returns>
        public static IWebElement GetChild(this IWebElement element)
        {
            return element.FindElement(By.XPath("./child::*"));
        }

        /// <summary>
        ///     Gets the preceding elements sibling.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static IWebElement GetPreviousSibling(this IWebElement element)
        {
            return element.FindElement(By.XPath("./preceding-sibling::*"));
        }

        /// <summary>
        ///     Gets the following elements sibling.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static IWebElement GetNextSibling(this IWebElement element)
        {
            return element.FindElement(By.XPath("./following-sibling::*"));
        }

       

        /// <summary>
        ///     Gets the children elements of the specified type.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="elementType">Type of the element.</param>
        /// <returns></returns>
        /// <summary>
        ///     Gets the children elements by.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> GetElementsBy(this IWebElement element, By locator)
        {
            return element.FindElements(locator);
        }

        /// <summary>
        ///     Waits until the element is visible.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="timespan">The timeout timespan.</param>
        public static void WaitUntilVisible(this IWebElement element, TimeSpan timespan)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var checking = false;
            while (stopwatch.IsRunning && stopwatch.Elapsed < timespan)
            {
                if (!checking)
                {
                    checking = true;
                    if (element.Displayed)
                    {
                        stopwatch.Stop();
                    }
                    else
                    {
                        checking = false;
                    }
                }
            }
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
        }

        /// <summary>
        ///     Waits until the element exists.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="timespan">The timeout timespan.</param>
        public static void WaitUntilExists(this IWebElement element, TimeSpan timespan)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var checking = false;
            while (stopwatch.IsRunning && stopwatch.Elapsed < timespan)
            {
                if (!checking)
                {
                    checking = true;
                    if (element != null)
                    {
                        stopwatch.Stop();
                    }
                    else
                    {
                        checking = false;
                    }
                }
            }
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
        }

      


        
        public static bool Contains(this IWebElement element, string value)
        {
            if (string.IsNullOrWhiteSpace(element.Text))
            {
                return false;
            }
            var result = element.Text.Contains(value);
            return result;
        }

        public static bool TagIs(this IWebElement element, string tag)
        {
            var result = element.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static IWebElement ClearText(this IWebElement element)
        {
            element.Clear();
            return element;
        }

        public static void SetTextForControl(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }
    }
}
