using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace JWT.Model
{
    public class Mdl_Currency
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string CrossRateName { get; set; }
        public double ForexBuying { get; set; }
        public double ForexSelling { get; set; }
        public double BanknoteBuying { get; set; }
        public double BanknoteSelling { get; set; }

  
    }

    /// <summary>
    /// Parasal Kur bilgileri ile ilgili işlemlerde bu class üzerinden yapılır.
    /// </summary>
   

        public enum ExchangeType
        {
            ForexBuying, ForexSelling,
            BanknoteBuying, BanknoteSelling
        }

        public enum CurrencyCode
        {
            USD, AUD, DKK,
            EUR, GBP, CHF,
            SEK, CAD, KWD,
            NOK, SAR, JPY,
            BGN, RON, RUB,
            IRR, CNY, PKR,
            TRY
        }

        public static class CurrenciesExchange
        {


            public static Dictionary<string, Mdl_Currency> GetAllCurrenciesTodaysExchangeRates()
            {
                try
                {
                    return GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/today.xml");
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            /// <summary>
            /// Günlük döviz kurları bilgilerini getirir.
            /// </summary>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public static List<Mdl_Currency> GetDataTableAllCurrenciesTodaysExchangeRates()
            {
                List<Mdl_Currency> DovizKurlari = new List<Mdl_Currency>();


                try
                {
                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/today.xml");

                    foreach (string item in CurrencyRates.Keys)
                    {

                        Mdl_Currency Para = new Mdl_Currency();
                        Para.Name = CurrencyRates[item].Name;
                        Para.Code = CurrencyRates[item].Code;
                        Para.CrossRateName = CurrencyRates[item].CrossRateName;
                        Para.ForexBuying = CurrencyRates[item].ForexBuying;
                        Para.ForexSelling = CurrencyRates[item].ForexSelling;
                        Para.BanknoteBuying = CurrencyRates[item].BanknoteBuying;
                        Para.BanknoteSelling = CurrencyRates[item].BanknoteSelling;

                        DovizKurlari.Add(Para);
                    }

                    return DovizKurlari;
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static Dictionary<string, Mdl_Currency> GetAllCurrenciesHistoricalExchangeRates(int Year, int Month, int Day)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", Year);
                    string SMonth = String.Format("{0:00}", Month);
                    string SDay = String.Format("{0:00}", Day);

                    return GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static DataTable GetDataTableAllCurrenciesHistoricalExchangeRates(int Year, int Month, int Day)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", Year);
                    string SMonth = String.Format("{0:00}", Month);
                    string SDay = String.Format("{0:00}", Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Code", typeof(string));
                    dt.Columns.Add("CrossRateName", typeof(string));
                    dt.Columns.Add("ForexBuying", typeof(double));
                    dt.Columns.Add("ForexSelling", typeof(double));
                    dt.Columns.Add("BanknoteBuying", typeof(double));
                    dt.Columns.Add("BanknoteSelling", typeof(double));

                    foreach (string item in CurrencyRates.Keys)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Name"] = CurrencyRates[item].Name;
                        dr["Code"] = CurrencyRates[item].Code;
                        dr["CrossRateName"] = CurrencyRates[item].CrossRateName;
                        dr["ForexBuying"] = CurrencyRates[item].ForexBuying;
                        dr["ForexSelling"] = CurrencyRates[item].ForexSelling;
                        dr["BanknoteBuying"] = CurrencyRates[item].BanknoteBuying;
                        dr["BanknoteSelling"] = CurrencyRates[item].BanknoteSelling;
                        dt.Rows.Add(dr);
                    }

                    return dt;

                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static Dictionary<string, Mdl_Currency> GetAllCurrenciesHistoricalExchangeRates(DateTime date)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", date.Year);
                    string SMonth = String.Format("{0:00}", date.Month);
                    string SDay = String.Format("{0:00}", date.Day);

                    return GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static DataTable GetDataTableAllCurrenciesHistoricalExchangeRates(DateTime date)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", date.Year);
                    string SMonth = String.Format("{0:00}", date.Month);
                    string SDay = String.Format("{0:00}", date.Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Code", typeof(string));
                    dt.Columns.Add("CrossRateName", typeof(string));
                    dt.Columns.Add("ForexBuying", typeof(double));
                    dt.Columns.Add("ForexSelling", typeof(double));
                    dt.Columns.Add("BanknoteBuying", typeof(double));
                    dt.Columns.Add("BanknoteSelling", typeof(double));

                    foreach (string item in CurrencyRates.Keys)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Name"] = CurrencyRates[item].Name;
                        dr["Code"] = CurrencyRates[item].Code;
                        dr["CrossRateName"] = CurrencyRates[item].CrossRateName;
                        dr["ForexBuying"] = CurrencyRates[item].ForexBuying;
                        dr["ForexSelling"] = CurrencyRates[item].ForexSelling;
                        dr["BanknoteBuying"] = CurrencyRates[item].BanknoteBuying;
                        dr["BanknoteSelling"] = CurrencyRates[item].BanknoteSelling;
                        dt.Rows.Add(dr);
                    }

                    return dt;

                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static Mdl_Currency GetTodaysExchangeRates(CurrencyCode Currency)
            {
                try
                {
                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/today.xml");

                    if (CurrencyRates.Keys.Contains(Currency.ToString()))
                    {
                        return CurrencyRates[Currency.ToString()];
                    }
                    else
                    {
                        throw new Exception("The specified currency(" + Currency.ToString() + ") was not found!");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static Mdl_Currency GetHistoricalExchangeRates(CurrencyCode Currency, int Year, int Month, int Day)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", Year);
                    string SMonth = String.Format("{0:00}", Month);
                    string SDay = String.Format("{0:00}", Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    if (CurrencyRates.Keys.Contains(Currency.ToString()))
                    {
                        return CurrencyRates[Currency.ToString()];
                    }
                    else
                    {
                        throw new Exception("The specified currency(" + Currency.ToString() + ") was not found!");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static Mdl_Currency GetHistoricalExchangeRates(CurrencyCode Currency, DateTime date)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", date.Year);
                    string SMonth = String.Format("{0:00}", date.Month);
                    string SDay = String.Format("{0:00}", date.Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    if (CurrencyRates.Keys.Contains(Currency.ToString()))
                    {
                        return CurrencyRates[Currency.ToString()];
                    }
                    else
                    {
                        throw new Exception("The specified currency(" + Currency.ToString() + ") was not found!");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            //public static Mdl_Currency GetTodaysCrossRates(CurrencyCode ToCurrencyCode, CurrencyCode FromCurrencyCode)
            //{
            //    try
            //    {
            //        Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/today.xml");

            //        if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
            //        {
            //            throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
            //        }
            //        else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
            //        {
            //            throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
            //        }
            //        else
            //        {
            //            Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
            //            Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

            //            return new Mdl_Currency(
            //                OtherCurrency.Name,
            //                OtherCurrency.Code,
            //                OtherCurrency.Code + "/" + MainCurrency.Code,
            //                (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4),
            //                (OtherCurrency.ForexSelling == 0 || MainCurrency.ForexSelling == 0) ? 0 : Math.Round((OtherCurrency.ForexSelling / MainCurrency.ForexSelling), 4),
            //                (OtherCurrency.BanknoteBuying == 0 || MainCurrency.BanknoteBuying == 0) ? 0 : Math.Round((OtherCurrency.BanknoteBuying / MainCurrency.BanknoteBuying), 4),
            //                (OtherCurrency.BanknoteSelling == 0 || MainCurrency.BanknoteSelling == 0) ? 0 : Math.Round((OtherCurrency.BanknoteSelling / MainCurrency.BanknoteSelling), 4)
            //            );
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("The date specified may be a weekend or a public holiday!");
            //    }
            //}

            public static double GetTodaysCrossRate(CurrencyCode ToCurrencyCode, CurrencyCode FromCurrencyCode)
            {
                try
                {
                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/today.xml");

                    if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
                    }
                    else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
                    }
                    else
                    {
                        Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
                        Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

                        return (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            //public static Mdl_Currency GetHistoricalCrossRates(CurrencyCode ToCurrencyCode, CurrencyCode FromCurrencyCode, DateTime date)
            //{
            //    try
            //    {
            //        string SYear = String.Format("{0:0000}", date.Year);
            //        string SMonth = String.Format("{0:00}", date.Month);
            //        string SDay = String.Format("{0:00}", date.Day);

            //        Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

            //        if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
            //        {
            //            throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
            //        }
            //        else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
            //        {
            //            throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
            //        }
            //        else
            //        {
            //            Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
            //            Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

            //            return new Mdl_Currency(
            //                OtherCurrency.Name,
            //                OtherCurrency.Code,
            //                OtherCurrency.Code + "/" + MainCurrency.Code,
            //                (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4),
            //                (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4),
            //                (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4),
            //                (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4)
            //            );
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("The date specified may be a weekend or a public holiday!");
            //    }
            //}

            public static double GetHistoricalCrossRate(CurrencyCode ToCurrencyCode, CurrencyCode FromCurrencyCode, DateTime date)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", date.Year);
                    string SMonth = String.Format("{0:00}", date.Month);
                    string SDay = String.Format("{0:00}", date.Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
                    }
                    else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
                    }
                    else
                    {
                        Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
                        Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

                        return (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            //public static Mdl_Currency GetHistoricalCrossRates(CurrencyCode ToCurrencyCode, CurrencyCode FromCurrencyCode, int Year, int Month, int Day)
            //{
            //    try
            //    {
            //        string SYear = String.Format("{0:0000}", Year);
            //        string SMonth = String.Format("{0:00}", Month);
            //        string SDay = String.Format("{0:00}", Day);

            //        Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

            //        if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
            //        {
            //            throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
            //        }
            //        else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
            //        {
            //            throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
            //        }
            //        else
            //        {
            //            Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
            //            Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

            //            return new Mdl_Currency(
            //                OtherCurrency.Name,
            //                OtherCurrency.Code,
            //                OtherCurrency.Code + "/" + MainCurrency.Code,
            //                (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4),
            //                (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4),
            //                (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4),
            //                (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4)
            //            );
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("The date specified may be a weekend or a public holiday!");
            //    }
            //}

            public static double GetHistoricalCrossRate(CurrencyCode ToCurrencyCode, CurrencyCode FromCurrencyCode, int Year, int Month, int Day)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", Year);
                    string SMonth = String.Format("{0:00}", Month);
                    string SDay = String.Format("{0:00}", Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
                    }
                    else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
                    }
                    else
                    {
                        Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
                        Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

                        return (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round((OtherCurrency.ForexBuying / MainCurrency.ForexBuying), 4);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static double CalculateTodaysExchange(double Amount, CurrencyCode FromCurrencyCode, CurrencyCode ToCurrencyCode)
            {
                try
                {
                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/today.xml");

                    if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
                    }
                    else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
                    }
                    else
                    {
                        Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
                        Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

                        return (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round(Amount * (MainCurrency.ForexBuying / OtherCurrency.ForexBuying), 4);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static double CalculateTodaysExchange(double Amount, CurrencyCode FromCurrencyCode, CurrencyCode ToCurrencyCode, ExchangeType exchangeType)
            {
                try
                {
                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/today.xml");

                    if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
                    }
                    else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
                    }
                    else
                    {
                        Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
                        Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

                        switch (exchangeType)
                        {
                            case ExchangeType.ForexBuying:
                                return (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round(Amount * (MainCurrency.ForexBuying / OtherCurrency.ForexBuying), 4);
                            case ExchangeType.ForexSelling:
                                return (OtherCurrency.ForexSelling == 0 || MainCurrency.ForexSelling == 0) ? 0 : Math.Round(Amount * (MainCurrency.ForexSelling / OtherCurrency.ForexSelling), 4);
                            case ExchangeType.BanknoteBuying:
                                return (OtherCurrency.BanknoteBuying == 0 || MainCurrency.BanknoteBuying == 0) ? 0 : Math.Round(Amount * (MainCurrency.BanknoteBuying / OtherCurrency.BanknoteBuying), 4);
                            case ExchangeType.BanknoteSelling:
                                return (OtherCurrency.BanknoteSelling == 0 || MainCurrency.BanknoteSelling == 0) ? 0 : Math.Round(Amount * (MainCurrency.BanknoteSelling / OtherCurrency.BanknoteSelling), 4);
                            default:
                                return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static double CalculateHistoricalExchange(double Amount, CurrencyCode FromCurrencyCode, CurrencyCode ToCurrencyCode, DateTime date)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", date.Year);
                    string SMonth = String.Format("{0:00}", date.Month);
                    string SDay = String.Format("{0:00}", date.Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
                    }
                    else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
                    }
                    else
                    {
                        Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
                        Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

                        return (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round(Amount * (MainCurrency.ForexBuying / OtherCurrency.ForexBuying), 4);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static double CalculateHistoricalExchange(double Amount, CurrencyCode FromCurrencyCode, CurrencyCode ToCurrencyCode, ExchangeType exchangeType, DateTime date)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", date.Year);
                    string SMonth = String.Format("{0:00}", date.Month);
                    string SDay = String.Format("{0:00}", date.Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
                    }
                    else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
                    }
                    else
                    {
                        Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
                        Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

                        switch (exchangeType)
                        {
                            case ExchangeType.ForexBuying:
                                return (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round(Amount * (MainCurrency.ForexBuying / OtherCurrency.ForexBuying), 4);
                            case ExchangeType.ForexSelling:
                                return (OtherCurrency.ForexSelling == 0 || MainCurrency.ForexSelling == 0) ? 0 : Math.Round(Amount * (MainCurrency.ForexSelling / OtherCurrency.ForexSelling), 4);
                            case ExchangeType.BanknoteBuying:
                                return (OtherCurrency.BanknoteBuying == 0 || MainCurrency.BanknoteBuying == 0) ? 0 : Math.Round(Amount * (MainCurrency.BanknoteBuying / OtherCurrency.BanknoteBuying), 4);
                            case ExchangeType.BanknoteSelling:
                                return (OtherCurrency.BanknoteSelling == 0 || MainCurrency.BanknoteSelling == 0) ? 0 : Math.Round(Amount * (MainCurrency.BanknoteSelling / OtherCurrency.BanknoteSelling), 4);
                            default:
                                return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static double CalculateHistoricalExchange(double Amount, CurrencyCode FromCurrencyCode, CurrencyCode ToCurrencyCode, int Year, int Month, int Day)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", Year);
                    string SMonth = String.Format("{0:00}", Month);
                    string SDay = String.Format("{0:00}", Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
                    }
                    else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
                    }
                    else
                    {
                        Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
                        Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

                        return (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round(Amount * (MainCurrency.ForexBuying / OtherCurrency.ForexBuying), 4);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            public static double CalculateHistoricalExchange(double Amount, CurrencyCode FromCurrencyCode, CurrencyCode ToCurrencyCode, ExchangeType exchangeType, int Year, int Month, int Day)
            {
                try
                {
                    string SYear = String.Format("{0:0000}", Year);
                    string SMonth = String.Format("{0:00}", Month);
                    string SDay = String.Format("{0:00}", Day);

                    Dictionary<string, Mdl_Currency> CurrencyRates = GetCurrencyRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");

                    if (!CurrencyRates.Keys.Contains(FromCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + FromCurrencyCode.ToString() + ") was not found!");
                    }
                    else if (!CurrencyRates.Keys.Contains(ToCurrencyCode.ToString()))
                    {
                        throw new Exception("The specified currency(" + ToCurrencyCode.ToString() + ") was not found!");
                    }
                    else
                    {
                        Mdl_Currency MainCurrency = CurrencyRates[FromCurrencyCode.ToString()];
                        Mdl_Currency OtherCurrency = CurrencyRates[ToCurrencyCode.ToString()];

                        switch (exchangeType)
                        {
                            case ExchangeType.ForexBuying:
                                return (OtherCurrency.ForexBuying == 0 || MainCurrency.ForexBuying == 0) ? 0 : Math.Round(Amount * (MainCurrency.ForexBuying / OtherCurrency.ForexBuying), 4);
                            case ExchangeType.ForexSelling:
                                return (OtherCurrency.ForexSelling == 0 || MainCurrency.ForexSelling == 0) ? 0 : Math.Round(Amount * (MainCurrency.ForexSelling / OtherCurrency.ForexSelling), 4);
                            case ExchangeType.BanknoteBuying:
                                return (OtherCurrency.BanknoteBuying == 0 || MainCurrency.BanknoteBuying == 0) ? 0 : Math.Round(Amount * (MainCurrency.BanknoteBuying / OtherCurrency.BanknoteBuying), 4);
                            case ExchangeType.BanknoteSelling:
                                return (OtherCurrency.BanknoteSelling == 0 || MainCurrency.BanknoteSelling == 0) ? 0 : Math.Round(Amount * (MainCurrency.BanknoteSelling / OtherCurrency.BanknoteSelling), 4);
                            default:
                                return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("The date specified may be a weekend or a public holiday!");
                }
            }

            private static Dictionary<string, Mdl_Currency> GetCurrencyRates(string Link)
            {
                try
                {
                    XmlTextReader rdr = new XmlTextReader(Link);
                    // XmlTextReader nesnesini yaratıyoruz ve parametre olarak xml dokümanın urlsini veriyoruz
                    // XmlTextReader urlsi belirtilen xml dokümanlarına hızlı ve forward-only giriş imkanı sağlar.
                    XmlDocument myxml = new XmlDocument();
                    // XmlDocument nesnesini yaratıyoruz.
                    myxml.Load(rdr);
                    // Load metodu ile xml yüklüyoruz
                    XmlNode tarih = myxml.SelectSingleNode("/Tarih_Date/@Tarih");
                    XmlNodeList mylist = myxml.SelectNodes("/Tarih_Date/Mdl_Currency");
                    XmlNodeList adi = myxml.SelectNodes("/Tarih_Date/Mdl_Currency/Isim");
                    XmlNodeList kod = myxml.SelectNodes("/Tarih_Date/Mdl_Currency/@Kod");
                    XmlNodeList doviz_alis = myxml.SelectNodes("/Tarih_Date/Mdl_Currency/ForexBuying");
                    XmlNodeList doviz_satis = myxml.SelectNodes("/Tarih_Date/Mdl_Currency/ForexSelling");
                    XmlNodeList efektif_alis = myxml.SelectNodes("/Tarih_Date/Mdl_Currency/BanknoteBuying");
                    XmlNodeList efektif_satis = myxml.SelectNodes("/Tarih_Date/Mdl_Currency/BanknoteSelling");

                    Dictionary<string, Mdl_Currency> ExchangeRates = new Dictionary<string, Mdl_Currency>();

                    Mdl_Currency Para1 = new Mdl_Currency();
                    Para1.Name = "Türk Lirası";
                    Para1.Code = "TRY";
                    Para1.CrossRateName = "TRY/TRY";
                    Para1.ForexBuying = 1;
                    Para1.ForexSelling = 1;
                    Para1.BanknoteBuying = 1;

                    ExchangeRates.Add("TRY", Para1);

                    for (int i = 0; i < adi.Count; i++)
                    {

                    Mdl_Currency Para = new Mdl_Currency();
                      
                    Para.Name = adi.Item(i).InnerText.ToString();
                    Para.Code = kod.Item(i).InnerText.ToString();
                    Para.CrossRateName = kod.Item(i).InnerText.ToString() + "/TRY";
                    Para.ForexBuying = (String.IsNullOrWhiteSpace(doviz_alis.Item(i).InnerText.ToString())) ? 0 : Convert.ToDouble(doviz_alis.Item(i).InnerText.ToString().Replace(".", ","));
                    Para.ForexSelling = (String.IsNullOrWhiteSpace(doviz_satis.Item(i).InnerText.ToString())) ? 0 : Convert.ToDouble(doviz_satis.Item(i).InnerText.ToString().Replace(".", ","));
                    Para.BanknoteBuying = (String.IsNullOrWhiteSpace(efektif_alis.Item(i).InnerText.ToString())) ? 0 : Convert.ToDouble(efektif_alis.Item(i).InnerText.ToString().Replace(".", ","));
                    Para.BanknoteSelling = (String.IsNullOrWhiteSpace(efektif_satis.Item(i).InnerText.ToString())) ? 0 : Convert.ToDouble(efektif_satis.Item(i).InnerText.ToString().Replace(".", ","));


                    ExchangeRates.Add(kod.Item(i).InnerText.ToString(), Para);
                    }

                    return ExchangeRates;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


   
}
