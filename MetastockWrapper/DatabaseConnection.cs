using MetastockWrapper.MSFL;
using System;

namespace MetastockWrapper
{
    public class DatabaseConnection : IDisposable
    {
        public string ApplicationName { get; set; }

        public string UserName { get; set; }

        public DatabaseConnection(string application, string user)
        {
            ApplicationName = application;
            UserName = user;

            this.Open();
        }

        ~DatabaseConnection()
        {
            if (Msfl.MSFL1_GetMSFLState() == (int)(Msfl.MsflErr.MsflErrAlreadyInitialized))
            {
                Msfl.MSFL1_Shutdown();
            }
        }

        public int Open()
        {
            return Msfl.MSFL1_GetMSFLState() != (int)(Msfl.MsflErr.MsflErrAlreadyInitialized) ? Msfl.MSFL1_Initialize(ApplicationName, UserName, Msfl.MsflDllInterfaceVersion) : Msfl.MSFL1_GetMSFLState();
        }

        public int Close()
        {
            return Msfl.MSFL1_GetMSFLState() == (int)(Msfl.MsflErr.MsflErrAlreadyInitialized) ? Msfl.MSFL1_Shutdown() : Msfl.MSFL1_GetMSFLState();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Close();
        }

        #endregion
    }
}
