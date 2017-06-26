using MetastockWrapper.MSFL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MetastockWrapper
{
    public class MetastockSecurity
    {
        public string Ticker { get; set; }

        public string Path { get; set; }

        public List<MsflPriceRecord> Daily { get; set; }

        public List<MsflPriceRecord> Weekly { get; set; }

        public List<MsflPriceRecord> Yearly { get; set; }

        public MetastockSecurity(string path, string tricker)
        {
            Ticker = tricker;
            this.Path = path;
        }

        public int Fill()
        {
            Daily = new List<MsflPriceRecord>();
            Weekly = new List<MsflPriceRecord>();
            Yearly = new List<MsflPriceRecord>();

            var returnValue = 0;
            var dirNumber = new byte();

            var priceRecordStore = new MsflPriceRecord();

            try
            {
                using (var conn = new DatabaseConnection(Environment.UserName.ToString(), "MetastockWrapper"))
                {

                    var lHSecurity = new IntPtr();
                    var lSSecurityId = new Msfl.MsflSecurityIdentifierStruct();
                    var lSSecurityInfo = new Msfl.MsflSecurityInfoStruct();
                    var lSPriceRecord = new Msfl.MsflPriceRecordStruct();

                    returnValue = Msfl.MSFL1_OpenDirectory(Path, ref dirNumber, Msfl.MsflDirAllowMultiOpen);
                    if (returnValue == (int)Msfl.MsflErr.MsflErrDirAlreadyOpen)
                    {
                        returnValue = Msfl.MSFL1_GetDirectoryNumber(Path, ref dirNumber);
                    }

                    if (returnValue == (int)(Msfl.MsflErr.MsflNoErr))
                    {
                        lSSecurityId = default(Msfl.MsflSecurityIdentifierStruct);
                        lSSecurityId.cDirNumber = dirNumber;
                        lSSecurityId.szSymbol = Ticker;
                        lSSecurityId.cPeriodicity = 68;

                        lSSecurityId.dwTotalSize = (uint)(Marshal.SizeOf(lSSecurityId));

                        returnValue = Msfl.MSFL1_GetSecurityHandle(ref lSSecurityId, ref lHSecurity);
                        returnValue = Msfl.MSFL1_GetSecurityInfo(lHSecurity, ref lSSecurityInfo);

                        returnValue = Msfl.MSFL1_LockSecurity(lHSecurity, Msfl.MsflLockPrevWriteLock);


                        while (returnValue == (int)(Msfl.MsflErr.MsflNoErr))
                        {
                            returnValue = Msfl.MSFL1_ReadDataRec(lHSecurity, ref lSPriceRecord);
                            if (returnValue == (int)(Msfl.MsflErr.MsflNoErr))
                            {
                                var dateString = Convert.ToString(lSPriceRecord.lDate);
                                var provider = CultureInfo.InvariantCulture;

                                var dateConverted = DateTime.ParseExact(dateString, "yyyyMMdd", provider);

                                lSPriceRecord.fOpen *= 100f;
                                lSPriceRecord.fHigh *= 100f;
                                lSPriceRecord.fLow *= 100f;
                                lSPriceRecord.fClose *= 100f;

                                var dailyRecord = new MsflPriceRecord
                                {
                                    Date = dateConverted,
                                    Open = lSPriceRecord.fOpen,
                                    High = lSPriceRecord.fHigh,
                                    Low = lSPriceRecord.fLow,
                                    Close = lSPriceRecord.fClose,
                                    Volume = lSPriceRecord.fVolume,
                                    OpenInt = lSPriceRecord.fOpenInt,
                                    DataAvailable = lSPriceRecord.wDataAvailable
                                };

                                Daily.Add(dailyRecord);

                                if (dateConverted.DayOfWeek < priceRecordStore.Date.DayOfWeek)
                                {
                                    Weekly.Add(new MsflPriceRecord
                                    {
                                        Date = priceRecordStore.Date,
                                        Open = priceRecordStore.Open,
                                        High = priceRecordStore.High,
                                        Low = priceRecordStore.Low,
                                        Close = priceRecordStore.Close,
                                        Volume = priceRecordStore.Volume,
                                        OpenInt = priceRecordStore.OpenInt,
                                        DataAvailable = priceRecordStore.DataAvailable
                                    });
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {

                // TODO Display Error
            }
            finally
            {
                Msfl.MSFL1_CloseDirectory(dirNumber);
            }


            return returnValue;
        }

        public List<float> ExtractFloat(List<MsflPriceRecord> priceRecord, char type)
        {
            var returnList = new List<float>();

            foreach (var price in priceRecord)
            {
                float floatResult = 0;
                if (type == 'C')
                {
                    floatResult = price.Close;
                }
                if (type == 'O')
                {
                    floatResult = price.Open;
                }
                if (type == 'H')
                {
                    floatResult = price.High;
                }
                if (type == 'L')
                {
                    floatResult = price.Low;
                }

                returnList.Add(floatResult);
            }

            return returnList;
        }

    }
}
