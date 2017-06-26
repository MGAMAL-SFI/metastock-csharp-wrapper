using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MetastockWrapper.MSFL
{
    public static class Msfl
    {
        /*----------------------------  Version Info  --------------------------------*/
        public const int MsflDllInterfaceVersion = 9100;         // DLL interface version

        /*----------------------------  Open directory flags  ------------------------*/
        public const int MsflDirNoFlags = 0x00;                  // Standard open
        public const int MsflDirForceUserIn = 0x04;             // Force user in directory
        public const int MsflDirMergeDupSecs = 0x08;            // Merge duplicate securities
        public const int MsflDirRemoveEmptyFiles = 0x10;        // On close, remove empty files
        public const int MsflDirCreateDopFiles = 0x20;          // Create & maintain DOP files
        public const int MsflDirAllowMultiOpen = 0x40;          // Allow opening a dir # times

        /*----------------------------  Name & Symbol  -------------------------------*/
        public const int MsflMaxNameLength = 45;                 // Max length of security name
        public const int MsflMaxSymbolLength = 14;               // Max length of tickerID symbol
        public const int MsflMaxUserNameLength = 30;            // Max len of user name no null
        public const int MsflMaxAppNameLength = 30;             // Max len of app name no null
        public const int MsflMaxUserIdLength = (MsflMaxAppNameLength + MsflMaxUserNameLength + 1);

        /*----------------------------  Lock Types  ----------------------------------*/
        public const int MsflLockPrevWriteLock = 0x10;          // Prevent write lock
        public const int MsflLockWriteLock = 0x20;               // Write lock
        public const int MsflLockFullLock = 0x80;                // Full lock

        /*----------------------------  Limitations  ---------------------------------*/
        public const int MsflMaxOpenDirectories = 125;           // Max num of open directories
        public const int MsflMaxNumOfSecurities = 2000;         // Max num of securities in dir
        public const int MsflMaxDataRecords = 65500;             // Max num of data records
        public const int MsflOldMaxDataRecords = 32766;         // Max num of data recs <= v6.5
        public const int MsflMaxLockedSecurities = 24;           // Max num of locked securities
        public const int MsflMaxErrMsgLength = 100;             // Max length of error message
        public const int MsflMaxPath = 261;                       // Max path length + NULL
        public const int MsflMaxReadWriteRecords = 65500;       // Max records to read/write
        public const int MsflMinDate = 18000101;                  // Min date  1/01/1800
        public const int MsflMaxDate = 22001231;                  // Max date 12/31/2200

        /*----------------------------  Data Fields (DO NOT REORDER)  ----------------*/
        public const int MsflDataDate = 0x0001;                   // Date field used
        public const int MsflDataClose = 0x0002;                  // Close field used
        public const int MsflDataVolume = 0x0004;                 // Volume field used
        public const int MsflDataHigh = 0x0008;                   // High field used
        public const int MsflDataLow = 0x0010;                    // Low field used
        public const int MsflDataOpen = 0x0020;                   // Open field used
        public const int MsflDataOpenint = 0x0040;                // Open interest field used
        public const int MsflDataTime = 0x0080;                   // Time field used

        /*----------------------------  Find Modes  ----------------------------------*/
        public enum MsflFind
        {
            MsflFindClosestPrev,                                 // If not exact match, use prev
            MsflFindClosestNext,                                 // If not exact match, use next
            MsflFindExactMatch,                                  // Must match exactly

            MsflFindUseCurrentPos                               // Use current pos (read/write)
        }

        /*----------------------------  MSFL Messages  -------------------------------*/
        public enum MsflMessages
        {
            MsflNoMsg = 0,                                        //   0| No message
            MsflMsgNotAMetastockDir,                           //   1| Not a MetaStock data directory
            MsflMsgCreatedDir,                                   //   2| Created directory
            MsflMsgBuiltMetastockDir,                           //   3| Built MetaStock directory
            MsflMsgCreatedNBuiltDir,                           //   4| Created and built MS directory

            MsflMsgFirstSecurityInDir,                         //   5| First security in directory
            MsflMsgLastSecurityInDir,                          //   6| Last security in directory

            MsflMsgNotAnExactMatch = 25,                       //  25| Find was not an exact match

            MsflMsgOverwroteRecords = 50,                        //  50| Write overwrote current records
            MsflMsgLessRecordsDel,                              //  51| Less rec were del than asked
            MsflMsgLessRecordsRead,                             //  52| Less rec were read than asked
            MsflMsgMoreRecordsInRange                          //  53| More recs to read in date range
        }

        /*----------------------------  MSFL Errors  ---------------------------------*/
        public enum MsflErr
        {
            MsflErrNotInitialized = -400,                //-400| MSFL hasn't been initialized
            MsflErrAlreadyInitialized,                   //-399| MSFL has been initialized
            MsflErrMsflCorrupt,                          //-398| MSFL is corrupt - shutdown!
            MsflErrOsVerNotSupported,                  //-397| Windows version below NT 3.1
            MsflErrShareNotLoaded,                      //-396| Share not loaded, load SHARE
            MsflErrInsufficientFiles,                    //-395| Insufficient file handles
            MsflErrInsufficientMem,                      //-394| Insufficient memory
            MsflErrInvalidUserId,                       //-393| Invalid user id
            MsflErrInvalidTempDir,                      //-392| Invalid TEMP directory
            MsflErrDllIncompatible,                      //-391| DLL interface is incompatible

            MsflErrInvalidDrive = -375,                  //-375| Drive is an invalid drive
            MsflErrInvalidDir,                           //-374| Directory is invalid
            MsflErrDirDoesNotExist,                    //-373| Directory does not exist
            MsflErrUnableToCreateDir,                  //-372| Unable to create the directory
            MsflErrDirAlreadyOpen,                      //-371| Directory was already opened
            MsflErrDirNotOpen,                          //-370| Directory has not been opened
            MsflErrTooManyDirsOpen,                    //-369| Too many directories opened
            MsflErrAlreadyAMsDir,                      //-368| Already a MetaStock directory
            MsflErrNotAMsDir,                          //-367| Not a MetaStock data directory
            MsflErrDirIsBusy,                           //-366| Directory is busy
            MsflErrUserIdAlreadyInDir,                //-365| Duplicate user id in directory
            MsflErrTooManyUsersInDir,                 //-364| Too many users can't open
            MsflErrInvalidUser,                          //-363| No longer a valid user in dir
            MsflErrNonMsflUserInDir,                  //-362| Non-MSFL user accessing dir
            MsflErrDirIsReadOnly,                      //-361| Directory is read-only
            MsflErrMaxFilesInTempDir,                 //-360| Too many MSFL files in temp dir

            MsflErrInvalidXmasterFile = -355,           //-355| XMaster file has an invalid id
            MsflErrInvalidIndexFile,                    //-354| Index file has an invalid id
            MsflErrInvalidLockFile,                     //-353| Lock file had an invalid id
            MsflErrInvalidSecurityFile,                 //-352| Security file has an invalid id
            MsflErrInvalidUsersFile,                    //-351| User file has an invalid id

            MsflErrCrcError = -350,                      //-350| CRC Error while reading
            MsflErrDriveNotReady,                       //-349| Drive is not ready
            MsflErrGeneralFailure,                       //-348| General failure on disk
            MsflErrMiscDiskError,                       //-347| General disk error
            MsflErrSectorNotFound,                      //-346| Sector not found
            MsflErrSeekError,                            //-345| Error seeking in file
            MsflErrUnknownMedia,                         //-344| Unknown media type
            MsflErrWriteProtected,                       //-343| Disk is write protected
            MsflErrDiskIsFull,                          //-342| Disk is full unable to write
            MsflErrNotSameDevice,                       //-341| The device is not the same
            MsflErrNetworkError,                         //-340| There was a network error

            MsflErrLockViolation = -325,                 //-325| File locking violation
            MsflErrInvalidLockType,                     //-324| Lock type is invalid
            MsflErrFileLocked,                           //-323| File locked by another user
            MsflErrTooManySecLocked,                   //-322| Too many securities locked
            MsflErrSecurityLocked,                       //-321| Security locked by another user
            MsflErrSecurityNotLocked,                   //-320| Security not locked
            MsflErrImproperLockType,                    //-319| Improper lock for operation

            MsflErrEndOfFile = -300,                    //-300| End of the file
            MsflErrErrorOpeningFile,                    //-299| Unable to open file
            MsflErrErrorReadingFile,                    //-298| Error reading the file
            MsflErrErrorWritingFile,                    //-297| Error writing the file
            MsflErrFileDoesntExist,                     //-296| File doesn't exist
            MsflErrInvalidFileHandle,                   //-295| The file handle is invalid
            MsflErrPermissionDenied,                     //-294| Access to file denied
            MsflErrSeekPastEof,                         //-293| Seek went past the end of file
            MsflErrMiscFileError,                       //-292| Misc file error

            MsflErrUnableToReadAll = -275,             //-275| Unable to read all records
            MsflErrUnableToWriteAll,                   //-274| Unable to write all records

            MsflErrAllSymbNotLoaded = -250,            //-250| Not all symbols were loaded
            MsflErrUnableToResynch,                     //-249| Unable to resynch masters
            MsflErrFilesInDirChanged,                  //-248| Files in dir changed
            MsflErrUnrecognizedVersion,                  //-247| Files are not recognized ver.

            MsflErrInvalidCompSymbol = -225,            //-225| Composite symbol is invalid
            MsflErrInvalidSymbol,                        //-224| Symbol is invalid

            MsflErrDifferentDataFormats = -200,         //-200| Securities with different data
            MsflErrDuplicateSecurities,                  //-199| Duplicate securities in dir
            MsflErrDuplicateSecurity,                    //-198| Add/change would duplicate
            MsflErrPrimarySecNotFound,                 //-197| Primary security not found
            MsflErrSecondarySecNotFound,               //-196| Secondary security not found
            MsflErrSecurityHasComposites,               //-195| Can't del, security has comp.
            MsflErrSecurityHasNoData,                  //-194| The security data file is empty
            MsflErrSecurityIsAComposite,               //-193| Security is a composite
            MsflErrSecurityNotComposite,                //-192| Security is not a composite
            MsflErrSecurityNotFound,                    //-191| Security not found
            MsflErrTooManySecurities,                   //-190| Attempted to add too many
            MsflErrTooManyComposites,                   //-189| Too many composites in directory
            MsflErrSecuritiesAreTheSame,               //-188| Securities are same security

            MsflErrInvalidDate = -175,                   //-175| Invalid date
            MsflErrInvalidTime,                          //-174| Invalid time
            MsflErrInvalidInterval,                      //-173| Invalid interval
            MsflErrInvalidPeriodicity,                   //-172| Invalid periodicity
            MsflErrInvalidOperator,                      //-171| Invalid composite operator
            MsflErrInvalidFieldOrder,                   //-170| Invalid combination of fields
            MsflErrInvalidRecords,                       //-169| Invalid records for operation
            MsflErrInvalidDisplayUnits,                 //-168| Invalid display units
            MsflErrInvalidSecurityHandle,               //-167| Invalid security handle

            MsflErrAddingWouldOverflow = -150,          //-150| Adding recs would overflow file
            MsflErrDataFileIsFull,                     //-149| The data file is full can't add
            MsflErrDataRecordNotFound,                 //-148| Date was not found for data rec
            MsflErrDataNotSorted,                       //-147| Data is not in date/time order
            MsflErrDateAfterLastRec,                   //-146| Date requested is after last
            MsflErrDateBeforeFirstRec,                 //-145| Date requested is before first
            MsflErrRecordIsADuplicate,                 //-144| Record is a duplicate
            MsflErrRecordOutOfRange,                   //-143| Record number is out of range
            MsflErrRecordNotFound,                      //-142| Record not found

            MsflErrBufferNotAttached = -125,            //-125| Buffer !attached to security
            MsflErrInvalidFuncParms,                    //-124| Invalid function parms
            MsflErrUnknownFieldsReq,                    //-123| Unknown fields requested

            MsflErrLastError,                            // -99| Last MSFL error    

            MsflNoErr = 0                                 //   0| Operation completed normally
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MsflSecurityInfoStruct
        {
            public uint dwTotalSize;                         // Structure size
            public IntPtr hSecurity;                           // Security handle
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MsflMaxNameLength + 1)]
            public String szName;      // Security name
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MsflMaxSymbolLength + 1)]
            public String szSymbol;  // tickerID symbol
            public byte cPeriodicity;                        // Periodicity (D, W, M, I)
            public ushort wInterval;                           // Intraday time interval
            public bool bComposite;                          // 0 = FALSE, otherwise TRUE
            public bool bFlagged;                            // 0 = FALSE, otherwise TRUE
            public byte ucDisplayUnits;                      // Units decimal/fraction
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MsflMaxSymbolLength + 1)]
            public String szCompSymbol; // 2nd symbol in comp.
            public byte cCompOperator;                       // Composite Op (+, -, *, or /)
            public float fCompFactor1;                        // Multiply factor for 1st sec.
            public float fCompFactor2;                        // Multiply factor for 2nd sec.
            public int lFirstDate;                          // Date in first data record
            public int lLastDate;                           // Date in last data record
            public int lFirstTime;                          // Time in the first record
            public int lLastTime;                           // Time in the last record
            public int lStartTime;                          // Intraday start trade time
            public int lEndTime;                            // Intraday end trade time
            public int lCollectionDate;                     // Date to collect data for
            public int lMostRecentAdjDate;                  // Most recent adjustment date
            public float fMostRecentAdjRatio;                 // Ratio of the most recent adj
            public ushort wDataAvailable;                      // Data fields available
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MsflPriceRecordStruct
        {
            public int lDate;                                   // Date of price record
            public int lTime;                                   // Time of price record
            public float fOpen;                                   // Open price
            public float fHigh;                                   // High price
            public float fLow;                                    // Low price
            public float fClose;                                  // Close price
            public float fVolume;                                 // Volume value
            public float fOpenInt;                                // Open interest value
            public ushort wDataAvailable;                          // Data fields available

        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MsflDirectoryStatus
        {
            public uint dwTotalSize;                            // Structure size
            public bool bExists;                                // Directory exists
            public bool bInUse;                                 // In use by one or more users
            public bool bMetaStockDir;                          // Contains MetaStock files
            public ushort wDriveType;                           // Drive type

            public bool bOpen;                                  // Directory is open
            public bool bReadOnly;                              // Directory is read-only
            public bool bUserInvalid;                           // User is invalid in dir
            public byte cDirNumber;                             // Directory number
            public uint dwNumOfSecurities;                      // Number of securities
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MsflSecurityInfo
        {
            public uint dwTotalSize;                            // Structure size
            public IntPtr hSecurity;                            // Security handle
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MsflMaxNameLength + 1)]
            public String szName;                               // Security name
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MsflMaxSymbolLength + 1)]
            public String szSymbol;                             // tickerID symbol
            public Char cPeriodicity;                           // Periodicity (D, W, M, I)
            public ushort wInterval;                            // Intraday time interval
            public bool bComposite;                             // 0 = FALSE, otherwise TRUE
            public bool bFlagged;                               // 0 = FALSE, otherwise TRUE
            public byte ucDisplayUnits;                         // Units decimal/fraction
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MsflMaxSymbolLength + 1)]
            public String szCompSymbol;                         // 2nd symbol in comp.
            public byte cCompOperator;                          // Composite Op (+, -, *, or /)
            public float fCompFactor1;                          // Multiply factor for 1st sec.
            public float fCompFactor2;                          // Multiply factor for 2nd sec.
            public int lFirstDate;                              // Date in first data record
            public int lLastDate;                               // Date in last data record
            public int lFirstTime;                              // Time in the first record
            public int lLastTime;                               // Time in the last record
            public int lStartTime;                              // Intraday start trade time
            public int lEndTime;                                // Intraday end trade time
            public int lCollectionDate;                         // Date to collect data for
            public int lMostRecentAdjDate;                      // Most recent adjustment date
            public float fMostRecentAdjRatio;                   // Ratio of the most recent adj
            public ushort wDataAvailable;                       // Data fields available
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MsflSecurityIdentifierStruct
        {
            public uint dwTotalSize;
            public byte cDirNumber;                              // Directory number
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MsflMaxSymbolLength + 1)]
            public String szSymbol;      // tickerID symbol
            public byte cPeriodicity;                            // Periodicity (D, W, M, or I)
            public ushort wInterval;                               // Intraday interval
            public bool bComposite;                              // Is security a composite
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MsflMaxSymbolLength + 1)]
            public String szCompSymbol;  // Secondary composite symbol
            public byte cCompOperator;                           // Composite Op (+, -, *, or /)

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MsflPriceRecord
        {
            public int lDate;                                   // Date of price record
            public int lTime;                                   // Time of price record
            public float fOpen;                                 // Open price
            public float fHigh;                                 // High price
            public float fLow;                                  // Low price
            public float fClose;                                // Close price
            public float fVolume;                               // Volume value
            public float fOpenInt;                              // Open interest value
            public ushort wDataAvailable;                       // Data fields available
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DateTime
        {
            public int lDate;
            public int lTime;
        }

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_AddSecurity();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_BuildMetaStockDirectory();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_CloseDirectory(byte cDirNumber);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_DeleteDataRec();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_DeleteSecurity();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_FindDataDate();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_FindDataRec();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_FormatDate(StringBuilder pszDateString, ushort wStringSize, int lDate);

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_FormatTime(StringBuilder pszTimeString, ushort wStringSize, int lTime, bool bIncludeTicks);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetCurrentDataPos();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetDataPath();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_GetDataRecordCount(IntPtr hSecurity, ref ushort pwNumOfDataRecs);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetDayMonthYear();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetDirectoryNumber();
        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_GetDirectoryNumber(string pszPath, ref byte pcDirNumber);

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_GetDirectoryStatus(byte cDirNumber, string pszDirectory, ref MsflDirectoryStatus psDirStatus);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetDirNumberFromHandle();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr MSFL1_GetErrorMessage(int iErr, StringBuilder pszErrorMessage, ushort wMaxMsgLength);

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_GetFirstSecurityInfo(byte cDirNumber, ref MsflSecurityInfo psSecurityInfo);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetHourMinTicks();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetLastFailedLockInfo();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetLastFailedOpenDirInfo();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetLastSecurityInfo();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetMSFLState();
        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_GetMSFLState();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_GetNextSecurityInfo(IntPtr hSecurity, ref MsflSecurityInfo psSecurityInfo);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetPrevSecurityInfo();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetRecordCountForDateRange();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetSecurityCount();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetSecurityHandle(MSFLSecurityIdentifier_struct psSecurityID, ref IntPtr phSecurity);

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_GetSecurityHandle(ref MsflSecurityIdentifierStruct psSecurityId, ref IntPtr phSecurity);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetSecurityID();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetSecurityInfo();
        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_GetSecurityInfo(IntPtr hSecurity, ref MsflSecurityInfoStruct psSecurityInfo);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_GetSecurityLockedStatus();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_Initialize(string pszAppName, string pszUserName, int iInterfaceVersion);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_InsertDataRec();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_LockSecurity(IntPtr hSecurity, int uiLockType);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_MakeMSFLDate();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_MakeMSFLTime();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int MSFL1_OpenDirectory(string pszDirectory, ref byte pcDirNumber, int iDirOpenFlags);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_ParseDateString();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_ParseTimeString();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_ReadDataRec();
        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_ReadDataRec(IntPtr hSecurity, ref MsflPriceRecordStruct psPriceRec);

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_SeekBeginData(IntPtr hSecurity);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_SeekEndData();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_Shutdown();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL1_UnlockSecurity(IntPtr hSecurity);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_UpdateSecurity();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL1_WriteDataRec();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_AdjustData();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_ChangeFieldsInData();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_CopySecurity();

        // [DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_DeleteMultipleRecs();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_DeleteMultipleRecsByDates();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_GetSecurityHandles();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_InsertMultipleRecs();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_MergeData();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_ReadBackMultipleRecs();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_ReadDataRec();

        [DllImport("MSFL91.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSFL2_ReadMultipleRecs(IntPtr hSecurity, [In, Out] MsflPriceRecord[] pasPriceRec, ref DateTime psFirstRecDate, ref ushort pwReadCount, int iFindMode);

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_ReadMultipleRecsByDates();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_RemoveDuplicateDataRecs();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_SortData();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_SortSecurities();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_WriteDataRec();

        //[DllImport( "MSFL91.dll", CallingConvention = CallingConvention.StdCall )]
        //private static extern int MSFL2_WriteMultipleRecs();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iErr"></param>
        public static void DisplayMsflError(int iErr)
        {
            // If there is an error or message
            if (iErr != (int)MsflErr.MsflNoErr)
            {
                var szErrMsg = new StringBuilder(Msfl.MsflMaxErrMsgLength);

                // Get the error/message text from MSFL
                var ptrStrInfo = Msfl.MSFL1_GetErrorMessage(iErr, szErrMsg, (ushort)szErrMsg.Capacity);
                //String strInfo = Marshal.PtrToStringAuto( ptrStrInfo );

                // If this is a information message, display the text in a message box
                //if (iErr > (int)MSFL_ERR.MSFL_NO_ERR)
                //    MessageBox.Show(szErrMsg.ToString(), "MSFL Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // If there is an error, display the text in a message box
                //else
                //    MessageBox.Show(szErrMsg.ToString(), "MSFL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
