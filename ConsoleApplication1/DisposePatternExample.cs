using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class DisposePatternExample : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool _disposed = false;
        private IntPtr _umResource;
        private const string InstanceName = "DisposePatternExample";

        public DisposePatternExample()
        {
            _umResource = Marshal.StringToCoTaskMemAuto(InstanceName);
        }
 

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //TODO://вопрос на засыпку, наши managed сlass`es типа Stream и тд которые 
        //TODO://инкапсулируют unmanaged ресурсы и имплементят IDispose() в какой блок кода надо поместить ? :)
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                
            }

            // Free any unmanaged objects here.
            //

            if (_umResource != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(_umResource);
                Console.WriteLine("[{0}] Unmanaged memory freed at {1:x16}",
                    InstanceName, _umResource.ToInt64());
                _umResource = IntPtr.Zero;
            }

            _disposed = true;
        }

        ~DisposePatternExample()
        {
            Dispose(false);
        }
    }
}
